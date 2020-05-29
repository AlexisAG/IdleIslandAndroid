using System.Collections;
using System.Collections.Generic;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

public class GiftInstance : MonoBehaviour
{
    private Gift _GiftRef = null;
    private ResourceModificator _Reward;


    public Gift GiftRef => _GiftRef;
    public ResourceModificator Reward => _Reward;


    public void Init(Gift g)
    {

    }

}
