using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class Action : ScriptableObject
    {
        [SerializeField]
        protected string _Id = null;
        [SerializeField]
        protected string _Description = null;

        public string Id => _Id;
        public string Description => _Description;
    }

    [CreateAssetMenu(menuName = "TheFantasticIsland/Action/Building", fileName = "NewBuildingAction")]
    public class BuildingAction : Action
    {
        [SerializeField]
        private Building _BuildingRef = null;
        [SerializeField]
        private BuildingPropertiesType _BuildingPropertiesType = BuildingPropertiesType.None;

        public Building BuildingRef => _BuildingRef;
        public BuildingPropertiesType BuildingProperties => _BuildingPropertiesType;
    }

    [CreateAssetMenu(menuName = "TheFantasticIsland/Action/Population", fileName = "NewPopulationAction")]
    public class PopulationAction : Action
    {
        [SerializeField]
        private Population _PopulationRef = null;

        public Population PopulationRef => _PopulationRef;
    }

    [CreateAssetMenu(menuName = "TheFantasticIsland/Action/Gift", fileName = "NewGiftAction")]
    public class GiftAction : Action
    {
        [SerializeField]
        private Gift _GiftRef = null;

        public Gift GiftRef => _GiftRef;
    }

}
