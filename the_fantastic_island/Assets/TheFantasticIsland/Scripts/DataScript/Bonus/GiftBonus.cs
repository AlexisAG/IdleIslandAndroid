using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "TheFantasticIsland/Bonus/Gift", fileName = "NewGiftBonus")]
public class GiftBonus : Bonus
{
    [SerializeField]
    private Gift _GiftRef = null;
    [SerializeField]
    private GiftPropertiesType _GiftProperties = GiftPropertiesType.None;

    public Gift GiftRef => _GiftRef;
    public GiftPropertiesType GiftProperty => _GiftProperties;
}