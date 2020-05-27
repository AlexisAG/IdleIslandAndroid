using System.Collections.Generic;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    [CreateAssetMenu(menuName = "TheFantasticIsland/Task", fileName = "NewTask")]
    public class Task : ScriptableObject
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField]
        private TaskType _Type = TaskType.None;
        [SerializeField]
        private int _BaseAmountObjective = 1;
        [SerializeField]
        private Action _Action = null;
        [SerializeField]
        private ResourceIntDictionary _BaseRewards = null;

        public string Id => _Id;
        public string Description => _Description;
        public TaskType Type => _Type;
        public int BaseAmountObjective => _BaseAmountObjective;
        public Action Action => _Action;
        public ResourceIntDictionary BaseRewards => _BaseRewards;
    }
}