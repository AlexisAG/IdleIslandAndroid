using System;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.Timer;
using AgToolkit.Core.Helper;
using AgToolkit.Core.Helper.Events;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using TheFantasticIsland.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace TheFantasticIsland.Instance
{
    public class BuildingInstance : MonoBehaviour
    {
        private Building _BuildingRef = null;
        private Timer _Timer = null;
        private Dictionary<BuildingPropertiesType, GameEventTrigger> _GameEventTriggers = new Dictionary<BuildingPropertiesType, GameEventTrigger>();

        public int ProductionLevel { get; private set; }
        public int SizeLevel { get; private set; }
        public float ProductionPerSecond { get; private set; }

        private void OnApplicationQuit()
        {
            // Reset value | Todo: maybe useless when Save System will be implemented
            _BuildingRef.ResourceProduction.AdjustAmount();
            _BuildingRef.Cost.AdjustAmount();
            _BuildingRef.IncreaseSizeCost.AdjustAmount();
        }

        private void SetupGameEventListeners()
        {
            foreach (BuildingAction a in _BuildingRef.BuildingActions)
            {
                GameEventTrigger trigger = gameObject.AddComponent<GameEventTrigger>();
                BuildingActionGameVar gameVar = new BuildingActionGameVar {Value = a};
                GameEvent gameEvent = (GameEvent)TaskManager.Instance.BuildingActionGameEventListener.Event;

                trigger.Init(gameEvent, gameVar);
                _GameEventTriggers.Add(a.BuildingProperties, trigger);
            }
        }

        private void Product()
        {
            _BuildingRef.ResourceProduction.AdjustAmount(ProductionLevel);

            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(_BuildingRef);

            float amount = _BuildingRef.ResourceProduction.Amount * (SizeLevel + 1);
            float productivityBonus = bonuses.ContainsKey(BuildingPropertiesType.Productivity) ? bonuses[BuildingPropertiesType.Productivity] : 0f;
            float resourceBonus = BonusManager.Instance.GetBonuses(_BuildingRef.ResourceProduction.Resource);

            amount += _BuildingRef.ResourceProduction.Amount * productivityBonus;
            amount += amount * resourceBonus;

            ProductionPerSecond = amount / _BuildingRef.TimeToProduct;

            ResourceManager.Instance.ChangeAmount(_BuildingRef.ResourceProduction.Resource, _BuildingRef.ResourceProduction.Type, Mathf.FloorToInt(amount));
            TimerManager.Instance.StartTimer(_Timer);
        }

        public void Init(Building building, int production = 0, int size = 0)
        {
            _BuildingRef = building;
            ProductionLevel = production;
            SizeLevel = size;
            SetupGameEventListeners();
            _Timer = new Timer(_BuildingRef.Id, _BuildingRef.TimeToProduct, new UnityEvent());
            _Timer.Event.AddListener(Product);
            TimerManager.Instance.StartTimer(_Timer);
        }

        public void IncreaseProduction()
        {
            ProductionLevel++;
            //todo add new worker

            _GameEventTriggers[BuildingPropertiesType.ProductivityCost].Trigger();
            _BuildingRef.Cost.AdjustAmount(ProductionLevel);
        }

        public void IncreaseSize()
        {
            SizeLevel++;
            //todo display new environment

            _GameEventTriggers[BuildingPropertiesType.SizeCost].Trigger();
            _BuildingRef.IncreaseSizeCost.AdjustAmount(SizeLevel);
        }

    }
    
}
