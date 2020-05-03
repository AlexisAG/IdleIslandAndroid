using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [SerializeField]
        private ResourceIntDictionary _Wallet = null;

        public bool ChangeAmount(Resource r, int amount)
        {
            int currentAmount = _Wallet[r];

            if (currentAmount <= 0)
            {
                return false; // if no amount of this resource, return false
            }

            currentAmount += amount;

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

