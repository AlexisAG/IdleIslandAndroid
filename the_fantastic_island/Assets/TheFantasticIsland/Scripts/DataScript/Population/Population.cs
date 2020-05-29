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
        protected int _Cost = 0;
        [SerializeField]
        protected Resource _Resource;
        [SerializeField]
        protected GameObject _Prefab = null;

        public string Id => _Id;
        public string Description => _Description;
        public float TimeProduction => _TimeProduction;
        public Bonus Bonus => _Bonus;
        public int Cost => _Cost;
        public Resource Resource => _Resource;
        public GameObject Prefab => _Prefab;
    }
}