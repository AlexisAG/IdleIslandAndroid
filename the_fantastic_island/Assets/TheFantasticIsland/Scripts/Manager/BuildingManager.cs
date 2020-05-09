using System;
using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.BackupSystem;
using AgToolkit.AgToolkit.Core.Singleton;
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
        
        private Dictionary<Building, BuildingInstance> _BuildingInstances = new Dictionary<Building, BuildingInstance>();

        public void SetupInterface()
        {
            // todo:
        }

        public IEnumerator Save()
        {
            throw new System.NotImplementedException();
            //todo:
        }

        public IEnumerator Load()
        {
            //todo
            throw new System.NotImplementedException();
        }

        public void BuyBuilding(Building b)
        {
            if (_BuildingInstances.ContainsKey(b))
            {
                IncreaseBuildingProduction(b);
                return;
            }
            if (b.Cost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {b.Cost.Type} (ResourceModificatorType)");
                return;
            }

            if (!ResourceManager.Instance.ChangeAmount(b.Cost.Resource, b.Cost.Type, b.Cost.Amount)) return;

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

            if (b.Cost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {b.Cost.Type} (ResourceModificatorType)");
                return;
            }

            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(b);
            float productionCostBonus = bonuses.ContainsKey(BuildingPropertiesType.ProductivityCost) ? bonuses[BuildingPropertiesType.ProductivityCost] : 0f;

            // adjust amount
            b.Cost.AdjustAmount(_BuildingInstances[b].ProductionLevel);
            float amount = b.Cost.Amount;
            amount += b.Cost.Amount * productionCostBonus;

            if (!ResourceManager.Instance.ChangeAmount(b.Cost.Resource, b.Cost.Type,Mathf.FloorToInt(amount))) return;

            _BuildingInstances[b].IncreaseProduction();
        }

        public void IncreaseBuildingSize(Building b)
        {
            if (b.IncreaseSizeCost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {b.Cost.Type} (ResourceModificatorType)");
                return;
            }
            if (!_BuildingInstances.ContainsKey(b)) {
                return;
            }

            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(b);
            float sizeCostBonus = bonuses.ContainsKey(BuildingPropertiesType.SizeCost) ? bonuses[BuildingPropertiesType.SizeCost] : 0f;

            // adjust amount
            b.IncreaseSizeCost.AdjustAmount(_BuildingInstances[b].SizeLevel);
            float amount = b.IncreaseSizeCost.Amount;
            amount += b.IncreaseSizeCost.Amount * sizeCostBonus;

            if (!ResourceManager.Instance.ChangeAmount(b.IncreaseSizeCost.Resource, b.IncreaseSizeCost.Type, Mathf.FloorToInt(amount))) return;

            _BuildingInstances[b].IncreaseSize();
        }

        public int GetTotalProductionPerSecond(Resource r)
        {
            float amount = 0;

            foreach (Building b in _BuildingInstances.Keys)
            {
                if (b.ResourceProduction.Resource != r) continue;
                amount += _BuildingInstances[b].ProductionPerSecond;
            }

            return Mathf.FloorToInt(amount);
        }

        private void Start()
        {
            BuyBuilding(_Buildings[0]); //todo delete this
        }
    }
}
