using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "Gift", fileName = "NewGift")]
    public class Gift : ScriptableObject
    {
        [SerializeField]
        private string _Id;
        [SerializeField]
        private string _Description;
        [SerializeField]
        private ResourceModificator _Reward;
        [SerializeField]
        private GameObject _Prefab;

        public string Id => _Id;
        public string Description => _Description;
        public ResourceModificator Reward => _Reward;
        public GameObject Prefab => _Prefab;
    }
}