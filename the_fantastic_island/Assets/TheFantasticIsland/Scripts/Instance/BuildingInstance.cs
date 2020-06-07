using System;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.DataSystem;
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
        [SerializeField]
        private ResourceModificator _ResourceProduction = null;
        [SerializeField]
        private ResourceModificator _Cost = null;
        [SerializeField]
        private ResourceModificator _IncreaseSizeCost = null;

        private Building _BuildingRef = null;
        private Timer _Timer = null;
        private Dictionary<BuildingPropertiesType, GameEventTrigger> _GameEventTriggers = new Dictionary<BuildingPropertiesType, GameEventTrigger>();
        public int ProductionLevel { get; private set; }
        public int SizeLevel { get; private set; }
        public float ProductionPerSecond { get; private set; }

        public ResourceModificator ResourceProduction => _ResourceProduction;
        public ResourceModificator IncreaseSizeCost => _IncreaseSizeCost;
        public ResourceModificator Cost => _Cost;

        private void OnApplicationQuit()
        {
            // Reset value | Todo: maybe useless when Save System will be implemented
            _ResourceProduction.AdjustAmount();
            _Cost.AdjustAmount();
            _IncreaseSizeCost.AdjustAmount();
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
            float amount = GetProductionAmount();

            ProductionPerSecond = amount / _BuildingRef.TimeToProduct;

            ResourceManager.Instance.ChangeAmount(_ResourceProduction.Resource, _ResourceProduction.Type, Mathf.FloorToInt(amount));
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

            _ResourceProduction = new ResourceModificator(ResourceModificatorType.Reward, _BuildingRef.Resource, _BuildingRef.BaseProduction);
            _Cost = new ResourceModificator(ResourceModificatorType.Cost, _BuildingRef.Resource, _BuildingRef.Cost);
            _IncreaseSizeCost = new ResourceModificator(ResourceModificatorType.Cost, _BuildingRef.ResourceSizeCost, _BuildingRef.SizeCost);
        }

        public void IncreaseProduction()
        {
            ProductionLevel++;
            //todo add new worker

            _GameEventTriggers[BuildingPropertiesType.ProductivityCost].Trigger();
        }

        public void IncreaseSize()
        {
            SizeLevel++;
            //todo display new environment

            _GameEventTriggers[BuildingPropertiesType.SizeCost].Trigger();
        }
        public float GetProductionCost() 
        {
            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(_BuildingRef);
            float productionCostBonus = bonuses.ContainsKey(BuildingPropertiesType.ProductivityCost) ? bonuses[BuildingPropertiesType.ProductivityCost] : 0f;

            // adjust amount
            Cost.AdjustAmount(ProductionLevel);
            float amount = Cost.Amount;
            amount += Cost.Amount * productionCostBonus;

            return amount;
        }

        public float GetSizeCost()
        {
            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(_BuildingRef);
            float sizeCostBonus = bonuses.ContainsKey(BuildingPropertiesType.SizeCost) ? bonuses[BuildingPropertiesType.SizeCost] : 0f;

            // adjust amount
            IncreaseSizeCost.AdjustAmount(SizeLevel);
            float amount = IncreaseSizeCost.Amount;
            amount += IncreaseSizeCost.Amount * sizeCostBonus;

            return amount;
        }

        public float GetProductionAmount() {
            Dictionary<BuildingPropertiesType, float> bonuses = BonusManager.Instance.GetBonuses(_BuildingRef);
            _ResourceProduction.AdjustAmount(ProductionLevel);

            float amount = _ResourceProduction.Amount * (SizeLevel + 1);
            float productivityBonus = bonuses.ContainsKey(BuildingPropertiesType.Productivity) ? bonuses[BuildingPropertiesType.Productivity] : 0f;
            float resourceBonus = BonusManager.Instance.GetBonuses(_ResourceProduction.Resource);

            amount += _ResourceProduction.Amount * productivityBonus;
            amount += amount * resourceBonus;

            return amount;
        }

    }
    
}
