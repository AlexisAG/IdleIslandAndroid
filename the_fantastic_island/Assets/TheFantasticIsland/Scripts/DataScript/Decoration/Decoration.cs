using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "TheFantasticIsland/Decoration", fileName = "NewDecoration")]
    public class Decoration : ScriptableObject
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField]
        private int _Cost = 0;
        [SerializeField]
        private Resource _Resource = Resource.Gold;
        [SerializeField]
        private GameObject _Prefab = null;

        public string Id => _Id;
        public string Description => _Description;
        public int Cost => _Cost;
        public Resource Resource => _Resource;
        public GameObject Prefab => _Prefab;
    }
}