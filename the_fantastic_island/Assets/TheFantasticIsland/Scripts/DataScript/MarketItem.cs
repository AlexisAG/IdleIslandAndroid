using UnityEngine;

namespace TheFantasticIsland.DataScript
{
    public abstract class MarketItem : ScriptableObject
    {

    }

    [CreateAssetMenu(menuName = "TheFantasticIsland/MarketItem/Bonus", fileName = "NewBonusItem")]
    public class BonusItem : MarketItem
    {

    }

    [CreateAssetMenu(menuName = "TheFantasticIsland/MarketItem/Resource", fileName = "NewResourceItem")]
    public class ResourceItem : MarketItem
    {

    }
}