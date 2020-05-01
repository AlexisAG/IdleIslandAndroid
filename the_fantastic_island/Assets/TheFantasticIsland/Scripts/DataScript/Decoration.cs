using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "Decoration", fileName = "NewDecoration")]
    public class Decoration : ScriptableObject
    {
        [SerializeField]
        private string _Id;
        [SerializeField]
        private string _Description;
        [SerializeField]
        private ResourceModificator _Cost;
        [SerializeField]
        private GameObject _Prefab;

        public string Id => _Id;
        public string Description => _Description;
        public ResourceModificator Cost => _Cost;
        public GameObject Prefab => _Prefab;
    }
}