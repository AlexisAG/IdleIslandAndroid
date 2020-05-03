using System;
using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.Timer;
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

        public int ProductionLevel { get; private set; }
        public int SizeLevel { get; private set; }
        public float ProductionPerSecond { get; private set; }

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

            ResourceManager.Instance.ChangeAmount(_BuildingRef.ResourceProduction.Resource, Mathf.FloorToInt(amount));
            TimerManager.Instance.StartTimer(_Timer);
        }

        public void Init(Building building, int production = 0, int size = 0)
        {
            _BuildingRef = building;
            ProductionLevel = production;
            SizeLevel = size;

            _Timer = new Timer(_BuildingRef.Id, _BuildingRef.TimeToProduct, new UnityEvent());
            _Timer.Event.AddListener(Product);
            TimerManager.Instance.StartTimer(_Timer);
        }

        public void IncreaseProduction()
        {
            ProductionLevel++;
            //todo add new worker
        }

        public void IncreaseSize()
        {
            SizeLevel++;
            //todo display new environment
        }

    }
    
}
