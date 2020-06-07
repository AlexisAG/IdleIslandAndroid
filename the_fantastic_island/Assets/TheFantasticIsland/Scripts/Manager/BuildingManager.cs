using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.DataSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using AgToolkit.Core.Helper;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using TheFantasticIsland.Instance;
using TheFantasticIsland.Ui;
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
        private GameObject _UiContent = null;

        private Dictionary<Building, BuildingInstance> _BuildingInstances = new Dictionary<Building, BuildingInstance>();

        private void SetupInterface()
        {
            Debug.Assert(_UiContent != null);

            UiManager.Instance.UiBuilding.SetTitle("Buildings");

            foreach (Building b in _Buildings)
            {
                ContentBuildingUI element = GameObject.Instantiate(_UiContent).GetComponent<ContentBuildingUI>();
                
                element.SetName(b.Id);
                element.SetDescription(b.Description);
                element.SetIcon(b.Sprite);
                element.SetCostResourceType(b.Resource, b.ResourceSizeCost);
                element.SetProductionInfo($"{ResourceManager.GetResourceText(b.Resource)}: {b.BaseProduction}/{b.TimeToProduct}s");
                element.SetAmount(b.Cost, b.SizeCost);
                element.Unlock.GetComponentInChildren<Text>().text = $"Unlock it for {b.Cost} {b.Resource}.";

                if (_BuildingInstances.ContainsKey(b))
                {
                    element.SetProductionLevel(_BuildingInstances[b].ProductionLevel + 1);
                    element.SetSizeLevel(_BuildingInstances[b].SizeLevel + 1);
                    element.SetAmount(_BuildingInstances[b].GetProductionCost(), _BuildingInstances[b].GetSizeCost());
                    element.SetProductionInfo($"{ResourceManager.GetResourceText(b.Resource)}: {_BuildingInstances[b].GetProductionAmount()}/{b.TimeToProduct}s");
                    element.ContentUnlocked();
                }
                else
                {
                    element.Unlock.onClick.AddListener((() =>
                    {
                        if(!BuildingManager.Instance.BuyBuilding(b)) return;

                        if (!_BuildingInstances.ContainsKey(b)) return;

                        element.SetProductionLevel(_BuildingInstances[b].ProductionLevel +1);
                        element.SetSizeLevel(_BuildingInstances[b].SizeLevel + 1);
                        element.SetAmount(_BuildingInstances[b].GetProductionCost(), _BuildingInstances[b].GetSizeCost());
                        element.SetProductionInfo($"{ResourceManager.GetResourceText(b.Resource)}: {_BuildingInstances[b].GetProductionAmount()}/{b.TimeToProduct}s");
                            
                        //Set price info
                        element.IncreaseProduction.GetComponentInChildren<Text>().text = $"{ResourceManager.GetResourceText(b.Resource)}: {_BuildingInstances[b].GetProductionCost()}";
                        element.IncreaseSize.GetComponentInChildren<Text>().text = $"{ResourceManager.GetResourceText(b.ResourceSizeCost)}: {_BuildingInstances[b].GetSizeCost()}";

                        //Add listener buttons
                        element.IncreaseProduction.onClick.AddListener((() =>
                        {
                            if (!IncreaseBuildingProduction(b)) return;
                            element.IncreaseProduction.GetComponentInChildren<Text>().text = $"{ResourceManager.GetResourceText(b.Resource)}: {_BuildingInstances[b].GetProductionCost()}";
                            element.SetProductionLevel(_BuildingInstances[b].ProductionLevel + 1);
                            element.SetAmount(_BuildingInstances[b].GetProductionCost(), _BuildingInstances[b].GetSizeCost());
                            element.SetProductionInfo($"{ResourceManager.GetResourceText(b.Resource)}: {_BuildingInstances[b].GetProductionAmount()}/{b.TimeToProduct}s");
                        }));
                        element.IncreaseSize.onClick.AddListener((() =>
                        {
                            if (!IncreaseBuildingSize(b)) return;
                            element.IncreaseSize.GetComponentInChildren<Text>().text = $"{ResourceManager.GetResourceText(b.ResourceSizeCost)}: {_BuildingInstances[b].GetSizeCost()}";
                            element.SetSizeLevel(_BuildingInstances[b].SizeLevel + 1);
                            element.SetAmount(_BuildingInstances[b].GetProductionCost(), _BuildingInstances[b].GetSizeCost());
                            element.SetProductionInfo($"{ResourceManager.GetResourceText(b.Resource)}: {_BuildingInstances[b].GetProductionAmount()}/{b.TimeToProduct}s");
                        }));

                        element.ContentUnlocked();
                    }));
                }

                UiManager.Instance.UiBuilding.AddContent(element.gameObject);   
            }
            UiManager.Instance.UiBuilding.gameObject.SetActive(false);
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

            float amount = _BuildingInstances[b].GetProductionCost();

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

            float amount = _BuildingInstances[b].GetSizeCost();

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
