using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class Population : ScriptableObject
    {
        [SerializeField]
        protected string _Id = null;
        [SerializeField]
        protected string _Description = null;
        [SerializeField]
        protected float _TimeProduction = 5f;
        [SerializeField]
        protected Bonus _Bonus = null;
        [SerializeField]
        protected ResourceModificator _Cost = null;
        [SerializeField]
        protected GameObject _Prefab = null;

        public string Id => _Id;
        public string Description => _Description;
        public float TimeProduction => _TimeProduction;
        public Bonus Bonus => _Bonus;
        public ResourceModificator Cost => _Cost;
        public GameObject Prefab => _Prefab;
    }

    [CreateAssetMenu(menuName = "TheFantasticIsland/Population/Human", fileName = "NewHuman")]
    public class Human : Population
    {

    }

    [CreateAssetMenu(menuName = "TheFantasticIsland/Population/Animal", fileName = "NewAnimal")]
    public class Animal : Population
    {
        [SerializeField]
        private Condition _Condition = null;

        public Condition Condition => _Condition;
    }
}