using System.Collections.Generic;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "TheFantasticIsland/Building", fileName = "NewBuilding")]
    public class Building : ScriptableObject
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField]
        private float _TimeToProduct = 5f;
        [SerializeField]
        private ResourceModificator _ResourceProduction = null;
        [SerializeField]
        private ResourceModificator _Cost = null;
        [SerializeField]
        private ResourceModificator _IncreaseSizeCost = null;
        [SerializeField]
        private GameObject _BuildingPrefab = null;
        [SerializeField]
        private GameObject _WorkerPrefab = null;
        [SerializeField]
        private List<BuildingAction> _BuildingActions = new List<BuildingAction>();

        public string Id => _Id;
        public string Description => _Description;
        public float TimeToProduct => _TimeToProduct;
        public ResourceModificator ResourceProduction => _ResourceProduction;
        public ResourceModificator IncreaseSizeCost => _IncreaseSizeCost;
        public ResourceModificator Cost => _Cost;
        public GameObject BuildingPrefab => _BuildingPrefab;
        public GameObject WorkerPrefab => _WorkerPrefab;
        public List<BuildingAction> BuildingActions => _BuildingActions;
    }
}
