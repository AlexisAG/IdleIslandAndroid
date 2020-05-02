using System;
using TheFantasticIsland;
using TheFantasticIsland.DataScript;
using UnityEngine;

namespace TheFantasticIsland
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

        public void ApplyModificator(float bonus)
        {
            // call ResourceManager & apply the modification
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class Condition
    {
        [SerializeField]
        private string _Id;
        [SerializeField]
        private string _Description;
        [SerializeField]
        private Building _BuildingRef;
        [SerializeField]
        private int _LevelRequiredBase = 1;

        private int _LevelRequired;

        public string Id => _Id;
        public string Description => _Description;
        public Building BuildingRef => _BuildingRef;
        public int LevelRequired => _LevelRequired;

        public Condition()
        {
            AdjustCondition();
        }

        public void AdjustCondition(int level = 0)
        {
            _LevelRequired = Mathf.FloorToInt(_LevelRequiredBase * Mathf.Exp(level));
        }
    }
}
