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
        private int _BaseProduction = 0;
        [SerializeField]
        private int _Cost = 0;
        [SerializeField]
        private int _SizeCost = 0;
        [SerializeField]
        private Resource _Resource = Resource.Gold;
        [SerializeField]
        private GameObject _BuildingPrefab = null;
        [SerializeField]
        private GameObject _WorkerPrefab = null;
        [SerializeField]
        private List<BuildingAction> _BuildingActions = new List<BuildingAction>();

        public string Id => _Id;
        public string Description => _Description;
        public float TimeToProduct => _TimeToProduct;
        public int BaseProduction => _BaseProduction;
        public int Cost => _Cost;
        public int SizeCost => _SizeCost;
        public Resource Resource => _Resource;
        public GameObject BuildingPrefab => _BuildingPrefab;
        public GameObject WorkerPrefab => _WorkerPrefab;
        public List<BuildingAction> BuildingActions => _BuildingActions;
    }
}
