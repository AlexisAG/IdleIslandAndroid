using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class MarketItem : ScriptableObject
    {

    }

    [CreateAssetMenu(menuName = "MarketItem/Bonus", fileName = "NewBonusItem")]
    public class BonusItem : MarketItem
    {

    }

    [CreateAssetMenu(menuName = "MarketItem/Resource", fileName = "NewResourceItem")]
    public class ResourceItem : MarketItem
    {

    }
}