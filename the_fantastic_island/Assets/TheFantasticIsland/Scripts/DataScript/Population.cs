using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class Population : ScriptableObject
    {
        [SerializeField]
        protected string _Id;
        [SerializeField]
        protected string _Description;
        [SerializeField]
        protected float _TimeProduction;
        [SerializeField]
        protected Bonus _Bonus;
        [SerializeField]
        protected ResourceModificator _Cost;
        [SerializeField]
        protected GameObject _Prefab;

        public string Id => _Id;
        public string Description => _Description;
        public float TimeProduction => _TimeProduction;
        public Bonus Bonus => _Bonus;
        public ResourceModificator Cost => _Cost;
        public GameObject Prefab => _Prefab;
    }

    [CreateAssetMenu(menuName = "Population/Human", fileName = "NewHuman")]
    public class Human : Population
    {

    }

    [CreateAssetMenu(menuName = "Population/Animal", fileName = "NewAnimal")]
    public class Animal : Population
    {
        [SerializeField]
        private Condition _Condition;

        public Condition Condition => _Condition;
    }
}