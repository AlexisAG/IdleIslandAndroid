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
            if (!ResourceManager.Instance.ChangeAmount(b.Cost.Resource, b.Cost.Amount)) return;

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

            // adjust amount
            b.Cost.AdjustAmount(_BuildingInstances[b].ProductionLevel);
            float amount = b.Cost.Amount;
            amount += b.Cost.Amount * BonusManager.Instance.GetBonuses(b)[BuildingPropertiesType.ProductivityCost];

            if (!ResourceManager.Instance.ChangeAmount(b.Cost.Resource, Mathf.FloorToInt(amount))) return;

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

            // adjust amount
            b.IncreaseSizeCost.AdjustAmount(_BuildingInstances[b].SizeLevel);
            float amount = b.IncreaseSizeCost.Amount;
            amount += b.IncreaseSizeCost.Amount * BonusManager.Instance.GetBonuses(b)[BuildingPropertiesType.SizeCost];

            if (!ResourceManager.Instance.ChangeAmount(b.IncreaseSizeCost.Resource, Mathf.FloorToInt(amount))) return;

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
