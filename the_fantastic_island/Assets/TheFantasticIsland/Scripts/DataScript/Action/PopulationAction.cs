using TheFantasticIsland.DataScript;
using UnityEngine;

[CreateAssetMenu(menuName = "TheFantasticIsland/Action/Population", fileName = "NewPopulationAction")]
public class PopulationAction : Action
{
    [SerializeField]
    private Population _PopulationRef = null;

    public Population PopulationRef => _PopulationRef;
}