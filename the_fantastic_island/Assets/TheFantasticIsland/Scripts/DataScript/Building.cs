using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "Building", fileName = "NewBuilding")]
    public class Building : ScriptableObject
    {
        [SerializeField]
        private string _Id;
        [SerializeField]
        private string _Description;
        [SerializeField]
        private ResourceModificator _ResourceProduction;
        [SerializeField]
        private ResourceModificator _IncreaseProductionCost;
        [SerializeField]
        private ResourceModificator _IncreaseSizeCost;
        [SerializeField]
        private BuildingBonus _SizeBonus;
        [SerializeField]
        private GameObject _BuildingPrefab;
        [SerializeField]
        private GameObject _WorkerPrefab;

        public string Id => _Id;
        public string Description => _Description;
        public ResourceModificator ResourceProduction => _ResourceProduction;
        public ResourceModificator IncreaseProductionCost => _IncreaseProductionCost;
        public ResourceModificator IncreaseSizeCost => _IncreaseSizeCost;
        public BuildingBonus SizeBonus => _SizeBonus;
        public GameObject BuildingPrefab => _BuildingPrefab;
        public GameObject WorkerPrefab => _WorkerPrefab;
    }
}
