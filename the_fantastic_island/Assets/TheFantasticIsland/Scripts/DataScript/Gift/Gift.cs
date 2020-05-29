using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "TheFantasticIsland/Gift", fileName = "NewGift")]
    public class Gift : ScriptableObject
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField, Tooltip("Timer in seconds")]
        private float _Timer = 300f;
        [SerializeField]
        private int _Reward = 0;
        [SerializeField]
        private Resource _Resource = 0;
        [SerializeField]
        private GameObject _Prefab = null;

        public string Id => _Id;
        public string Description => _Description;
        public float Timer => _Timer;
        public int Reward => _Reward;
        public Resource Resource => _Resource;
        public GameObject Prefab => _Prefab;
    }
}