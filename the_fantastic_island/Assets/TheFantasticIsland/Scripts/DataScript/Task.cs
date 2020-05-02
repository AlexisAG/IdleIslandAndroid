using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "Task", fileName = "NewTask")]
    public class Task : ScriptableObject
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField]
        private TaskType _Type = TaskType.None;
        [SerializeField]
        private Objective _Objective = null;
        [SerializeField]
        private Action _Action = null;
        [SerializeField]
        private List<ResourceModificator> _Rewards = null;

        public string Id => _Id;
        public string Description => _Description;
        public TaskType Type => _Type;
        public Objective Objective => _Objective;
        public Action Action => _Action;
        public List<ResourceModificator> Rewards => _Rewards;
    }
}