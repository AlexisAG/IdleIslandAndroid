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
}
