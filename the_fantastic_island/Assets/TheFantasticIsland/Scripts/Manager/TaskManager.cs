using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.DataSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using TheFantasticIsland.Instance;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class TaskManager : Singleton<TaskManager>, IBackup
    {
        [SerializeField]
        private string _BundleName = "";
        [SerializeField]
        private List<TaskInstance> _Missions = new List<TaskInstance>();
        [SerializeField]
        private List<TaskInstance> _Success = new List<TaskInstance>();

        private TaskInstance _CurrentMission = null;

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
            
            // Init GameEventListener & Create GameEvent
            BuildingActionGameEventListener.Init(ScriptableObject.CreateInstance<BuildingActionGameEvent>(), buildingActionUnityEvent);
            PopulationActionGameEventListener.Init(ScriptableObject.CreateInstance<PopulationActionGameEvent>(), populationActionUnityEvent);
            GiftActionGameEventListener.Init(ScriptableObject.CreateInstance<GiftActionGameEvent>(), giftActionUnityEvent);

            // Register the listeners
            BuildingActionGameEventListener.Event.RegisterListener(BuildingActionGameEventListener);
            PopulationActionGameEventListener.Event.RegisterListener(PopulationActionGameEventListener);
            GiftActionGameEventListener.Event.RegisterListener(GiftActionGameEventListener);
        }

        private void Start()
        {
            StartCoroutine(Load());
        }

        private void ActionExecuted(BuildingAction a) {

            foreach (TaskInstance task in _Success) 
            {

                // check if it's a success action
                if (!(task.TaskRef.Action is BuildingAction action)) continue;
                if (a.BuildingRef != action.BuildingRef) continue;
                if (a.BuildingProperties != action.BuildingProperties) continue;

                if (!task.Objective.IncrementAmount()) continue; //if success is not complete continue

                CashRewards(task.Rewards, task.CurrentLevel);

                task.Objective.IncrementDifficulty();
                task.IncrementLevel();
            }

            // check if it's a mission action
            if (!(_CurrentMission.TaskRef.Action is BuildingAction missionAction)) return;
            if (a.BuildingRef != missionAction.BuildingRef) return;
            if (a.BuildingProperties != missionAction.BuildingProperties) return;

            if (!_CurrentMission.Objective.IncrementAmount()) return; //if success is not complete continue

            CashRewards(_CurrentMission.Rewards, _CurrentMission.CurrentLevel);
            NextMission();
            Debug.LogWarning("CASH REWARD");
        }
        private void ActionExecuted(GiftAction a) 
        {

            foreach (TaskInstance task in _Success) {

                // check if it's a success action
                if (!(task.TaskRef.Action is GiftAction action)) continue;
                if (a.GiftRef != action.GiftRef) continue;

                if (!task.Objective.IncrementAmount()) continue; //if success is not complete continue

                CashRewards(task.Rewards, task.CurrentLevel);
                task.Objective.IncrementDifficulty();
                task.IncrementLevel();
            }

            // check if it's a Mission action
            if (!(_CurrentMission.TaskRef.Action is GiftAction missionAction)) return;
            if (a.GiftRef != missionAction.GiftRef) return;

            if (!_CurrentMission.Objective.IncrementAmount()) return; //if success is not complete continue

            CashRewards(_CurrentMission.Rewards, _CurrentMission.CurrentLevel);
            NextMission();
        }
        private void ActionExecuted(PopulationAction a) {
            foreach (TaskInstance task in _Success) 
            {

                // check if it's a success action
                if (!(task.TaskRef.Action is PopulationAction action)) continue;
                if (a.PopulationRef != action.PopulationRef) continue;

                if (!task.Objective.IncrementAmount()) continue; //if success is not complete continue

                CashRewards(task.Rewards, task.CurrentLevel);
                task.Objective.IncrementDifficulty();
                task.IncrementLevel();
            }

            // check if it's a Mission action
            if (!(_CurrentMission.TaskRef.Action is PopulationAction missionAction)) return;
            if (a.PopulationRef != missionAction.PopulationRef) return;

            if (!_CurrentMission.Objective.IncrementAmount()) return; //if success is not complete continue

            CashRewards(_CurrentMission.Rewards, _CurrentMission.CurrentLevel);
            NextMission();
        }

        private void NextMission()
        {
            int index = _Missions.IndexOf(_CurrentMission);

            if (index == -1) return;

            index++;

            if (index >= _Missions.Count)
            {
                _CurrentMission = _Missions[0];
                _CurrentMission.IncrementLevel();
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
                ResourceManager.Instance.ChangeAmount(reward.Resource, reward.Type, reward.Amount);
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

             BundleDataManager.Instance.GetBundleData<Task>(_BundleName).ForEach((t => {
                
                switch (t.Type)
                {
                    case TaskType.Mission:
                        _Missions.Add(new TaskInstance(t.BaseAmountObjective, t.BaseRewards, t));
                        break;
                    case TaskType.Success:
                        _Success.Add(new TaskInstance(t.BaseAmountObjective, t.BaseRewards, t));
                        break;
                }
                

            }));

            _CurrentMission = _Missions[0];

            yield return null;
            //todo load player saves
        }
    }
}
