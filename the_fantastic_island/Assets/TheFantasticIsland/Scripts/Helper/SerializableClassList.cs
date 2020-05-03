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

        private int _Amount = 0;

        public ResourceModificatorType Type => _Type;
        public Resource Resource => _Resource;
        public int Amount => _Amount;

        public void AdjustAmount(int level = 0)
        {
            _Amount = Mathf.FloorToInt(_BaseAmount * Mathf.Exp(level));

            switch (_Type) {
                case ResourceModificatorType.Reward:
                    if (_Amount < 0)
                    {
                        _Amount = -_Amount;
                    }
                    break;
                case ResourceModificatorType.Cost:
                    if (_Amount > 0)
                    {
                        _Amount = -_Amount;
                    }
                    break;
                default:
                    Debug.Assert(false, $"ResourceModificator Type is not implemented for {_Type}");
                    break;
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

        private int _LevelRequired;

        public string Id => _Id;
        public string Description => _Description;
        public Building BuildingRef => _BuildingRef;
        public int LevelRequired => _LevelRequired;

        public void AdjustCondition(int level = 0)
        {
            _LevelRequired = Mathf.FloorToInt(_LevelRequiredBase * Mathf.Exp(level));
        }
    }

    [Serializable]
    public class Objective
    {
        [SerializeField]
        protected int _AmountToReach = 1;

        private int _Amount = 0;

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
