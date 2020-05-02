using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "Decoration", fileName = "NewDecoration")]
    public class Decoration : ScriptableObject
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField]
        private ResourceModificator _Cost = null;
        [SerializeField]
        private GameObject _Prefab = null;

        public string Id => _Id;
        public string Description => _Description;
        public ResourceModificator Cost => _Cost;
        public GameObject Prefab => _Prefab;
    }
}