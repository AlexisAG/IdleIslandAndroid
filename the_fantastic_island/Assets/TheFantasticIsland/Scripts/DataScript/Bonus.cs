using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class Bonus : ScriptableObject
    {
        [SerializeField]
        protected string _Id = null;
        [SerializeField]
        protected string _Description = null;
        [SerializeField]
        protected float _BonusCoef = .2f;

        public string Id => _Id;
        public string Description => _Description;
        public float BonusCoef => _BonusCoef;
    }

    [CreateAssetMenu(menuName = "Bonus/Building", fileName = "NewBuildingBonus")]
    public class BuildingBonus : Bonus
    {
        [SerializeField]
        private Building _BuildingRef = null;
        [SerializeField]
        private BuildingPropertiesType _BuildingProperties = BuildingPropertiesType.None;

        public Building BuildingRef => _BuildingRef;
        public BuildingPropertiesType BuildingProperties => _BuildingProperties;
    }

    [CreateAssetMenu(menuName = "Bonus/Gift", fileName = "NewGiftBonus")]
    public class GiftBonus : Bonus
    {
        [SerializeField]
        private Gift _GiftRef = null;
        [SerializeField]
        private GiftPropertiesType _GiftProperties = GiftPropertiesType.None;

        public Gift GiftRef => _GiftRef;
        public GiftPropertiesType GiftProperty => _GiftProperties;
    }

    [CreateAssetMenu(menuName = "Bonus/Resource", fileName = "NewResourceBonus")]
    public class ResourceBonus : Bonus
    {
        [SerializeField]
        private Resource _Resource = Resource.None;

        public Resource Resource => _Resource;
    }
}