using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.Singleton;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [SerializeField]
        private Dictionary<Resource, int> _Wallet = new Dictionary<Resource, int>();

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

            _Wallet[r] = amount; 

            return true;
        }

        public int GetAmount(Resource r)
        {
            return _Wallet[r];
        }
    }
}

