using System.Collections;
using System.Collections.Generic;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

public class PopulationInstance : MonoBehaviour
{
    [SerializeField]
    private Population _PopulationRef = null;
    [SerializeField]
    private ResourceModificator _Cost = null;

    public ResourceModificator Cost => _Cost;


    public void Init(Population p)
    {
        _PopulationRef = p;
        _Cost = new ResourceModificator(ResourceModificatorType.Cost, p.Resource, p.Cost);
    }
}
