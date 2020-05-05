using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AgToolkit.AgToolkit.Core.BackupSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;
using UnityEngine.Analytics;

namespace TheFantasticIsland.Manager
{
    public class TaskManager : Singleton<TaskManager>, IBackup
    {
        [SerializeField]
        private List<Task> _Missions = new List<Task>();
        [SerializeField]
        private List<Task> _Success = new List<Task>();
        [SerializeField]
        private BuildingActionGameEvent _BuildingActionGameEvent = null;
        [SerializeField]
        private PopulationActionGameEvent _PopulationActionGameEvent = null;
        [SerializeField]
        private GiftActionGameEvent _GiftActionGameEvent = null;

        private int _CurrentMissionLevel = 0;
        private Task _CurrentMission = null;
        private Dictionary<Task, int> _SuccessLevel = new Dictionary<Task, int>();

        public BuildingActionGameEventListener BuildingActionGameEventListener { get; private set; }
        public PopulationActionGameEventListener PopulationActionGameEventListener { get; private set; }
        public GiftActionGameEventListener GiftActionGameEventListener { get; private set; }

        protected override void Awake() 
        {
            base.Awake();

            // Create unity event
            BuildingActionUnityEvent buildingActionUnityEvent = new BuildingActionUnityEvent();
            buildingActionUnityEvent.AddListener(ActionExecuted);
            PopulationActionUnityEvent populationActionUnityEvent = new PopulationActionUnityEvent();
            populationActionUnityEvent.AddListener(ActionExecuted);
            GiftActionUnityEvent giftActionUnityEvent = new GiftActionUnityEvent();
            giftActionUnityEvent.AddListener(ActionExecuted);

            // Add GameEventListener
            BuildingActionGameEventListener = gameObject.AddComponent<BuildingActionGameEventListener>();
            PopulationActionGameEventListener = gameObject.AddComponent<PopulationActionGameEventListener>();
            GiftActionGameEventListener = gameObject.AddComponent<GiftActionGameEventListener>();
            

            BuildingActionGameEventListener.Init(_BuildingActionGameEvent, buildingActionUnityEvent);
            PopulationActionGameEventListener.Init(_PopulationActionGameEvent, populationActionUnityEvent);
            GiftActionGameEventListener.Init(_GiftActionGameEvent, giftActionUnityEvent);

            _CurrentMission = _Missions[0];

            foreach (Task success in _Success)
            {
                _SuccessLevel.Add(success, 0);
            }
        }


        private void ActionExecuted(BuildingAction a) {

            foreach (Task task in _SuccessLevel.Keys) 
            {

                // check if it's a success action
                if (!(task.Action is BuildingAction action)) continue;
                if (a.BuildingRef != action.BuildingRef) continue;
                if (a.BuildingProperties != action.BuildingProperties) continue;

                if (!task.Objective.IncrementAmount()) continue; //if success is not complete continue

                CashRewards(task.Rewards, _SuccessLevel[task]);

                task.Objective.IncrementDifficulty();
                _SuccessLevel[task]++;
            }

            // check if it's a success action
            if (!(_CurrentMission.Action is BuildingAction missionAction)) return;
            if (a.BuildingRef != missionAction.BuildingRef) return;
            if (a.BuildingProperties != missionAction.BuildingProperties) return;

            if (!_CurrentMission.Objective.IncrementAmount()) return; //if success is not complete continue

            CashRewards(_CurrentMission.Rewards, _CurrentMissionLevel);
            NextMission();
        }
        private void ActionExecuted(GiftAction a) 
        {

            foreach (Task task in _SuccessLevel.Keys) {

                // check if it's a success action
                if (!(task.Action is GiftAction action)) continue;
                if (a.GiftRef != action.GiftRef) continue;

                if (!task.Objective.IncrementAmount()) continue; //if success is not complete continue

                CashRewards(task.Rewards, _SuccessLevel[task]);
                task.Objective.IncrementDifficulty();
                _SuccessLevel[task]++;
            }

            // check if it's a success action
            if (!(_CurrentMission.Action is GiftAction missionAction)) return;
            if (a.GiftRef != missionAction.GiftRef) return;

            if (!_CurrentMission.Objective.IncrementAmount()) return; //if success is not complete continue

            CashRewards(_CurrentMission.Rewards, _CurrentMissionLevel);
            NextMission();
        }
        private void ActionExecuted(PopulationAction a) {
            foreach (Task task in _SuccessLevel.Keys) 
            {

                // check if it's a success action
                if (!(task.Action is PopulationAction action)) continue;
                if (a.PopulationRef != action.PopulationRef) continue;

                if (!task.Objective.IncrementAmount()) continue; //if success is not complete continue

                CashRewards(task.Rewards, _SuccessLevel[task]);
                task.Objective.IncrementDifficulty();
                _SuccessLevel[task]++;
            }

            // check if it's a success action
            if (!(_CurrentMission.Action is PopulationAction missionAction)) return;
            if (a.PopulationRef != missionAction.PopulationRef) return;

            if (!_CurrentMission.Objective.IncrementAmount()) return; //if success is not complete continue

            CashRewards(_CurrentMission.Rewards, _CurrentMissionLevel);
            NextMission();
        }

        private void NextMission()
        {
            int index = _Missions.IndexOf(_CurrentMission);

            if (index == -1) return;

            index++;

            if (index > _Missions.Count)
            {
                _CurrentMission = _Missions[0];
                _CurrentMissionLevel++;
                _CurrentMission.Objective.IncrementDifficulty();
            }
            else
            {
                _CurrentMission = _Missions[index];
            }
        }

        private void CashRewards(List<ResourceModificator> rm, int level)
        {
            foreach (ResourceModificator reward in rm)
            {
                reward.AdjustAmount(level);
                ResourceManager.Instance.ChangeAmount(reward.Resource, reward.Amount);
            }
        }

        public void SetupInterface()
        {

        }

        public IEnumerator Save()
        {
            //todo
            throw new System.NotImplementedException();
        }

        public IEnumerator Load()
        {
            //todo
            throw new System.NotImplementedException();
        }
    }
}
