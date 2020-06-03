using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.DataSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using AgToolkit.Core.Helper;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using TheFantasticIsland.Instance;
using UnityEngine;
using UnityEngine.UI;

namespace TheFantasticIsland.Manager
{
    public class BuildingManager : Singleton<BuildingManager>, IBackup
    {
        [SerializeField]
        private List<Building> _Buildings = new List<Building>();
        [SerializeField]
        private string _BundleName = "";
        [SerializeField]
        private ScrollableInterface _Ui = null;
        [SerializeField]
        private GameObject _UiContent = null;

        private Dictionary<Building, BuildingInstance> _BuildingInstances = new Dictionary<Building, BuildingInstance>();

        private void Start()
        {
            CoroutineManager.Instance.StartCoroutine(Load());
        }

        private void SetupInterface()
        {
            Debug.Assert(_Ui != null);
            Debug.Assert(_UiContent != null);

            foreach (Building b in _Buildings)
            {
                GameObject element = GameObject.Instantiate(_UiContent);
                element.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = $"Buy {b.Cost} {ResourceManager.GetResourceText(b.Resource)}";
                element.GetComponentInChildren<Button>().onClick.AddListener((() =>
                {
                    if (BuyBuilding(b))
                    {
                        element.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = $"Increase Production to level {_BuildingInstances[b].ProductionLevel + 1} \n {b.Cost} {ResourceManager.GetResourceText(b.Resource)}";
                    };
                }));
                element.GetComponentsInChildren<Text>()[1].text = b.Description;
                _Ui.AddContent(element);   
            }
        }

        private void CreateBuilding(Building b, int production = 0, int size = 0)
        {
            BuildingInstance instance = Instantiate(b.BuildingPrefab).GetComponent<BuildingInstance>();
            instance.Init(b);
            //todo: set position
            instance.gameObject.SetActive(true);
            _BuildingInstances.Add(b, instance);
        }

        public IEnumerator Save()
        {
            foreach (Building b in _BuildingInstances.Keys)
            {
                BuildingInstance instance = _BuildingInstances[b];
                DataSystem.SaveGameInBinary("Building", new BuildingDataSerializable(b.Id, instance.SizeLevel, instance.ProductionLevel), b.Id);
                yield return null;
            }
        }

        public IEnumerator Load()
        {
            //Load buildings list from bundle
            _Buildings = BundleDataManager.Instance.GetBundleData<Building>(_BundleName);
            yield return null;

            // Load save
            DataSystem.LoadAllDataFromBinary<BuildingDataSerializable>("Building").ForEach((data =>
            {
                Building b = _Buildings.Find((building => building.Id == data.BuildingId));
                Debug.Assert(b != null, $"There is no building {data.BuildingId}.");
                CreateBuilding(b, data.ProductionLevel, data.SizeLevel);
            }));

            //SetupInterface
            SetupInterface();
        }

        public bool BuyBuilding(Building b)
        {
            if (_BuildingInstances.ContainsKey(b))
            {
                return IncreaseBuildingProduction(b);
            }

            if (!ResourceManager.Instance.ChangeAmount(b.Resource, ResourceModificatorType.Cost, b.Cost)) return false;

            CreateBuilding(b);
            return true;
        }

        public bool IncreaseBuildingProduction(Building b)
        {
            if (!_BuildingInstances.ContainsKey(b)) return false;

            if (_BuildingInstances[b].Cost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {_BuildingInstances[b].Cost.Type} (ResourceModificatorType)");
                return false;
            }

            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(b);
            float productionCostBonus = bonuses.ContainsKey(BuildingPropertiesType.ProductivityCost) ? bonuses[BuildingPropertiesType.ProductivityCost] : 0f;

            // adjust amount
            _BuildingInstances[b].Cost.AdjustAmount(_BuildingInstances[b].ProductionLevel);
            float amount = _BuildingInstances[b].Cost.Amount;
            amount += _BuildingInstances[b].Cost.Amount * productionCostBonus;

            if (!ResourceManager.Instance.ChangeAmount(_BuildingInstances[b].Cost.Resource, _BuildingInstances[b].Cost.Type,Mathf.FloorToInt(amount))) return false;

            _BuildingInstances[b].IncreaseProduction();

            return true;
        }

        public bool IncreaseBuildingSize(Building b)
        {
            if (_BuildingInstances[b].IncreaseSizeCost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {_BuildingInstances[b].Cost.Type} (ResourceModificatorType)");
                return false;
            }

            if (!_BuildingInstances.ContainsKey(b)) return false;

            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(b);
            float sizeCostBonus = bonuses.ContainsKey(BuildingPropertiesType.SizeCost) ? bonuses[BuildingPropertiesType.SizeCost] : 0f;

            // adjust amount
            _BuildingInstances[b].IncreaseSizeCost.AdjustAmount(_BuildingInstances[b].SizeLevel);
            float amount = _BuildingInstances[b].IncreaseSizeCost.Amount;
            amount += _BuildingInstances[b].IncreaseSizeCost.Amount * sizeCostBonus;

            if (!ResourceManager.Instance.ChangeAmount(_BuildingInstances[b].IncreaseSizeCost.Resource, _BuildingInstances[b].IncreaseSizeCost.Type, Mathf.FloorToInt(amount))) return false;

            _BuildingInstances[b].IncreaseSize();
            return true;
        }

        public int GetTotalProductionPerSecond(Resource r)
        {
            float amount = 0;

            foreach (Building b in _BuildingInstances.Keys)
            {
                if (_BuildingInstances[b].ResourceProduction.Resource != r) continue;
                amount += _BuildingInstances[b].ProductionPerSecond;
            }

            return Mathf.FloorToInt(amount);
        }
    }
}
