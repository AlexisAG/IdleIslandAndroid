using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.BackupSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class BonusManager : Singleton<BonusManager>, IBackup
    {
        private readonly List<ResourceBonus> _ResourceBonuses = new List<ResourceBonus>();
        private readonly List<BuildingBonus> _BuildingBonuses = new List<BuildingBonus>();
        private readonly List<GiftBonus> _GiftBonuses = new List<GiftBonus>();

        public void SetupInterface()
        {
            //todo
        }

        public IEnumerator Save()
        {
            //todo
            throw new System.NotImplementedException();
        }

        public IEnumerator Load()
        {
            //todo:
            throw new System.NotImplementedException();
        }

        public void AddBonus(Bonus b)
        {
            switch (b)
            {
                case BuildingBonus building:
                    _BuildingBonuses.Add(building);
                    break;
                case GiftBonus gift:
                    _GiftBonuses.Add(gift);
                    break;
                case ResourceBonus resource:
                    _ResourceBonuses.Add(resource);
                    break;
            }
        }

        public float GetBonuses(Resource r)
        {
            float bonusesCoef = 0f;

            foreach (ResourceBonus bonus in _ResourceBonuses)
            {
                if (bonus.Resource != r)
                {
                    continue;
                }

                bonusesCoef += bonus.BonusCoef;
            }

            return bonusesCoef;
        }

        public Dictionary<BuildingPropertiesType, float> GetBonuses(Building b)
        {
            Dictionary<BuildingPropertiesType, float> bonuses = new Dictionary<BuildingPropertiesType, float>();

            foreach (BuildingBonus bonus in _BuildingBonuses)
            {
                if (bonus.BuildingRef != b)
                {
                    continue;
                }

                if (bonuses.ContainsKey(bonus.BuildingProperties))
                {
                    bonuses[bonus.BuildingProperties] += bonus.BonusCoef;
                }
                else
                {
                    bonuses.Add(bonus.BuildingProperties, bonus.BonusCoef);
                }
            }

            return bonuses;
        }

        public Dictionary<GiftPropertiesType, float> GetBonuses(Gift g)
        {
            Dictionary<GiftPropertiesType, float> bonuses = new Dictionary<GiftPropertiesType, float>();

            foreach (GiftBonus bonus in _GiftBonuses) 
            {
                if (bonus.GiftRef != g) 
                {
                    continue;
                }

                if (bonuses.ContainsKey(bonus.GiftProperty)) 
                {
                    bonuses[bonus.GiftProperty] += bonus.BonusCoef;
                }
                else 
                {
                    bonuses.Add(bonus.GiftProperty, bonus.BonusCoef);
                }
            }

            return bonuses;
        }
    }
}
