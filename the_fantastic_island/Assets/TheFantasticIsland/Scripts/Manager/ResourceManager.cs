using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [SerializeField]
        private ResourceIntDictionary _Wallet = null;

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
            UiManager.Instance.UpdateResourceInfo(r);

            return true;
        }

        public int GetAmount(Resource r)
        {
            return _Wallet[r];
        }

        public static string GetResourceText(Resource r)
        {
            switch (r) {
                case Resource.Gold:
                    return "Gold";
                case Resource.Tools:
                    return "Tools";
                case Resource.Knowledge:
                    return "Knowledge";
                case Resource.Diamond:
                    return "Diamond";
                default:
                    return "";
            }
        }

        // todo
        public static string GetResourceIconPath(Resource r)
        {
            switch (r) {
                case Resource.Gold:
                    return "";
                case Resource.Tools:
                    return "";
                case Resource.Knowledge:
                    return "";
                case Resource.Diamond:
                    return "";
                default:
                    return "";
            }
        }
    }
}

