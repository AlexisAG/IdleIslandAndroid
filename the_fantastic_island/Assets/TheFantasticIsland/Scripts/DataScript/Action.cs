using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class Action : ScriptableObject
    {
        [SerializeField]
        protected string _Id;
        [SerializeField]
        protected string _Description;

        public string Id => _Id;
        public string Description => _Description;
    }

    [CreateAssetMenu(menuName = "Action/Building", fileName = "NewBuildingAction")]
    public class BuildingAction : Action
    {
        [SerializeField]
        private Building _BuildingRef;
        [SerializeField]
        private BuildingPropertiesType _BuildingPropertiesType;

        public Building BuildingRef => _BuildingRef;
        public BuildingPropertiesType BuildingProperties => _BuildingPropertiesType;
    }

    [CreateAssetMenu(menuName = "Action/Population", fileName = "NewPopulationAction")]
    public class PopulationAction : Action
    {
        [SerializeField]
        private Population _PopulationRef;

        public Population PopulationRef => _PopulationRef;
    }

    [CreateAssetMenu(menuName = "Action/Gift", fileName = "NewGiftAction")]
    public class GiftAction : Action
    {
        [SerializeField]
        private Gift _GiftRef;

        public Gift GiftRef => _GiftRef;
    }

}
