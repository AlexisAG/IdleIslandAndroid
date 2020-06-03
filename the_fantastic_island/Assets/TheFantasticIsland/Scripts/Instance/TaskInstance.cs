using System.Collections;
using System.Collections.Generic;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.Instance
{
    public class TaskInstance
    {
        public Objective Objective { get; private set; }
        public List<ResourceModificator> Rewards { get; private set; } = new List<ResourceModificator>();
        public Task TaskRef { get; private set; }

        public int CurrentLevel { get; private set; } = 0;

        public TaskInstance(int objectifBaseAmount, ResourceIntDictionary rewards, Task task)
        {
            TaskRef = task;
            Objective = new Objective(objectifBaseAmount);

            foreach (Resource r in rewards.Keys)
            {
                Rewards.Add(new ResourceModificator(ResourceModificatorType.Reward, r, rewards[r]));
            }
        }

        public void IncrementLevel()
        {
            CurrentLevel++;
        }
    }
}