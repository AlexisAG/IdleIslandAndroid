using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.DataSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using AgToolkit.Core.Helper;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using TheFantasticIsland.Instance;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class BuildingManager : Singleton<BuildingManager>, IBackup
    {
        [SerializeField]
        private List<Building> _Buildings = new List<Building>();
        [SerializeField]
        private string _BundleName = "";

        private Dictionary<Building, BuildingInstance> _BuildingInstances = new Dictionary<Building, BuildingInstance>();

        private void SetupInterface()
        {
            // todo:
        }

        private void Start()
        {
            CoroutineManager.Instance.StartCoroutine(Load());
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
        }

        public void BuyBuilding(Building b)
        {
            if (_BuildingInstances.ContainsKey(b))
            {
                IncreaseBuildingProduction(b);
                return;
            }

            if (!ResourceManager.Instance.ChangeAmount(b.Resource, ResourceModificatorType.Cost, b.Cost)) return;

            CreateBuilding(b);

        }

        public void IncreaseBuildingProduction(Building b)
        {
            if (!_BuildingInstances.ContainsKey(b)) {
                return;
            }

            if (_BuildingInstances[b].Cost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {_BuildingInstances[b].Cost.Type} (ResourceModificatorType)");
                return;
            }

            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(b);
            float productionCostBonus = bonuses.ContainsKey(BuildingPropertiesType.ProductivityCost) ? bonuses[BuildingPropertiesType.ProductivityCost] : 0f;

            // adjust amount
            _BuildingInstances[b].Cost.AdjustAmount(_BuildingInstances[b].ProductionLevel);
            float amount = _BuildingInstances[b].Cost.Amount;
            amount += _BuildingInstances[b].Cost.Amount * productionCostBonus;

            if (!ResourceManager.Instance.ChangeAmount(_BuildingInstances[b].Cost.Resource, _BuildingInstances[b].Cost.Type,Mathf.FloorToInt(amount))) return;

            _BuildingInstances[b].IncreaseProduction();
        }

        public void IncreaseBuildingSize(Building b)
        {
            if (_BuildingInstances[b].IncreaseSizeCost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {_BuildingInstances[b].Cost.Type} (ResourceModificatorType)");
                return;
            }
            if (!_BuildingInstances.ContainsKey(b)) {
                return;
            }

            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(b);
            float sizeCostBonus = bonuses.ContainsKey(BuildingPropertiesType.SizeCost) ? bonuses[BuildingPropertiesType.SizeCost] : 0f;

            // adjust amount
            _BuildingInstances[b].IncreaseSizeCost.AdjustAmount(_BuildingInstances[b].SizeLevel);
            float amount = _BuildingInstances[b].IncreaseSizeCost.Amount;
            amount += _BuildingInstances[b].IncreaseSizeCost.Amount * sizeCostBonus;

            if (!ResourceManager.Instance.ChangeAmount(_BuildingInstances[b].IncreaseSizeCost.Resource, _BuildingInstances[b].IncreaseSizeCost.Type, Mathf.FloorToInt(amount))) return;

            _BuildingInstances[b].IncreaseSize();
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
