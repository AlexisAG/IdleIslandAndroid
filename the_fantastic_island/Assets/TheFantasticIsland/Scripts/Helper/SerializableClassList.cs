using System;
using TheFantasticIsland.DataScript;
using UnityEngine;
namespace TheFantasticIsland.Helper
{
    [Serializable]
    public class ResourceModificator
    {
        [SerializeField]
        private ResourceModificatorType _Type = ResourceModificatorType.None;
        [SerializeField]
        private Resource _Resource = Resource.None;
        [SerializeField]
        private int _BaseAmount = 0;

        public ResourceModificatorType Type => _Type;
        public Resource Resource => _Resource;
        public int Amount { get; private set; } = 0;

        public ResourceModificator(ResourceModificatorType type, Resource resource, int amount)
        {
            _Type = type;
            _Resource = resource;
            _BaseAmount = amount;
        }

        public void AdjustAmount(int level = 0)
        {
            Amount = Mathf.FloorToInt(_BaseAmount * Mathf.Exp(level));
            if (Amount < 0)
            {
                Amount = -Amount;
            }
        }
    }

    [Serializable]
    public class Condition
    {
        [SerializeField]
        private string _Id = null;
        [SerializeField]
        private string _Description = null;
        [SerializeField]
        private Building _BuildingRef = null;
        [SerializeField]
        private int _LevelRequiredBase = 1;
        

        public string Id => _Id;
        public string Description => _Description;
        public Building BuildingRef => _BuildingRef;

        public int GetLevelRequired(int currentLevel)
        {
            return Mathf.FloorToInt(_LevelRequiredBase * Mathf.Exp(currentLevel));
        }
    }

    [Serializable]
    public class Objective
    {
        [SerializeField]
        protected int _AmountToReach = 1;

        private int _Amount = 0;

        public Objective(int amount)
        {
            _AmountToReach = amount;
        }

        public bool IncrementAmount()
        {
            _Amount++;

            return _Amount >= _AmountToReach;
        }

        public void IncrementDifficulty(int i = 1)
        {
            _AmountToReach += i * 10;
        }
    }
}
