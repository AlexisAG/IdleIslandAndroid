using TheFantasticIsland.DataScript;
using UnityEngine;

[CreateAssetMenu(menuName = "TheFantasticIsland/Action/Gift", fileName = "NewGiftAction")]
public class GiftAction : Action
{
    [SerializeField]
    private Gift _GiftRef = null;

    public Gift GiftRef => _GiftRef;
}