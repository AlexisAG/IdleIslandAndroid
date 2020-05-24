using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class ResourceManager : Singleton<ResourceManager>
    {
#if UNITY_EDITOR
        [SerializeField]
        private ResourceIntDictionary _Wallet = null;
#else
        private Dictionary<Resource, int> _Wallet = new Dictionary<Resource, int>();
#endif
        public bool ChangeAmount(Resource r, ResourceModificatorType type, int amount)
        {

            switch (type)
            {
                case ResourceModificatorType.Cost when amount > 0:
                case ResourceModificatorType.Reward when amount < 0:
                    amount = -amount;
                    break;
            }

            int currentAmount = _Wallet[r] + amount;
            
            if (currentAmount < 0)
            {
                return false; // if the amount is a cost & there is not enough resource, return false
            }

            _Wallet[r] = currentAmount; 

            return true;
        }

        public int GetAmount(Resource r)
        {
            return _Wallet[r];
        }
    }
}

