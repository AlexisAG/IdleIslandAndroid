using System;
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

        private void Update() //TODO DELETE THIS
        {
            Debug.Log(GetTotalProductionPerSecond(Resource.Gold));
        }

        private void Start()
        {
            CoroutineManager.Instance.StartCoroutine(Load());
        }

        public IEnumerator Save()
        {
            throw new System.NotImplementedException();
            //todo:
        }

        public IEnumerator Load()
        {
            //Load buildings list from bundle
            _Buildings = BundleDataManager.Instance.GetBundleData<Building>(_BundleName);
            BuyBuilding(_Buildings[0]); // TODO DELETE THIS
            yield return null;
            //todo: Load player progression saved
        }

        public void BuyBuilding(Building b)
        {
            if (_BuildingInstances.ContainsKey(b))
            {
                IncreaseBuildingProduction(b);
                return;
            }

            if (!ResourceManager.Instance.ChangeAmount(b.Resource, ResourceModificatorType.Cost, b.Cost)) return;

            BuildingInstance instance = Instantiate(b.BuildingPrefab).GetComponent<BuildingInstance>();
            instance.Init(b);
            instance.gameObject.SetActive(true);

            _BuildingInstances.Add(b, instance);
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
