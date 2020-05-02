using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.BackupSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.DataScript;
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
            if (b.Cost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {b.Cost.Type} (ResourceModificatorType)");
                return;
            }

            if (_BuildingInstances.ContainsKey(b))
            {
                IncreaseBuildingProduction(b);
                return;
            }

            if (!ResourceManager.Instance.ChangeAmount(b.Cost.Resource, b.Cost.Amount)) return;

            BuildingInstance instance = Instantiate(b.BuildingPrefab).GetComponent<BuildingInstance>();
            //todo configure buildingInstance
            instance.gameObject.SetActive(true);

            _BuildingInstances.Add(b, instance);
        }

        public void IncreaseBuildingProduction(Building b)
        {
            if (!_BuildingInstances.ContainsKey(b)) {
                return;
            }

            //todo
        }

        public void IncreaseBuildingSize(Building b)
        {
            if (b.Cost.Type != ResourceModificatorType.Cost) {
                Debug.Assert(false, $"Building may have a {ResourceModificatorType.Cost} and not a {b.Cost.Type} (ResourceModificatorType)");
                return;
            }

            if (!_BuildingInstances.ContainsKey(b)) {
                return;
            }

            //todo
        }
    }
}
