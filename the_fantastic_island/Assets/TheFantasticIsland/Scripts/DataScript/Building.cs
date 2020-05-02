using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "Building", fileName = "NewBuilding")]
    public class Building : ScriptableObject
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField]
        private ResourceModificator _ResourceProduction = null;
        [SerializeField]
        private ResourceModificator _IncreaseSizeCost = null;
        [SerializeField]
        private ResourceModificator _Cost = null;
        [SerializeField]
        private BuildingBonus _SizeBonus = null;
        [SerializeField]
        private GameObject _BuildingPrefab = null;

        public string Id => _Id;
        public string Description => _Description;
        public ResourceModificator ResourceProduction => _ResourceProduction;
        public ResourceModificator IncreaseSizeCost => _IncreaseSizeCost;
        public ResourceModificator Cost => _Cost;
        public BuildingBonus SizeBonus => _SizeBonus;
        public GameObject BuildingPrefab => _BuildingPrefab;
    }
}
