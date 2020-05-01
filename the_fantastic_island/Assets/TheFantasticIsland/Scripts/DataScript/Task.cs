using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class Task : ScriptableObject
    {
        [SerializeField]
        protected string _Id;
        [SerializeField]
        protected string _Description;
        [SerializeField]
        protected int _AmountToReach = 1;
        [SerializeField]
        protected Action _Action;
        [SerializeField]
        protected List<ResourceModificator> _Rewards;

        public string Id => _Id;
        public string Description => _Description;
        public int AmountToReach => _AmountToReach;
        public Action Action => _Action;
        public List<ResourceModificator> Rewards => _Rewards;
    }

    [CreateAssetMenu(menuName = "Task/Success", fileName = "NewSuccess")]
    public class Success : Task
    {
        [SerializeField]
        private Success _NextSuccess;

        public Success NexSuccess => _NextSuccess;
    }

    [CreateAssetMenu(menuName = "Task/Mission", fileName = "NewMission")]
    public class Mission : Task
    {
        [SerializeField]
        private int _Level = 1;

        public int Level => _Level;
    }
}