using System.Collections;
using System.Collections.Generic;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using TheFantasticIsland.Manager;
using UnityEngine;

public class DecorationInstance : MonoBehaviour
{
    private Decoration _DecorationRef = null;
    private Vector3 _Position = Vector3.zero;

    public void Init(Decoration d)
    {
        _DecorationRef = d;
    }

    public void Remove()
    {
        DecorationManager.Instance.TidyDecoration(_DecorationRef);
        Destroy(gameObject);
    }
}
