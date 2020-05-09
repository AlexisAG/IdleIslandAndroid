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
}