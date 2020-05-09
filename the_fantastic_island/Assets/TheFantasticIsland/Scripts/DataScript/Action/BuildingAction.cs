using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "TheFantasticIsland/Action/Building", fileName = "NewBuildingAction")]
public class BuildingAction : Action
{
    [SerializeField]
    private Building _BuildingRef = null;
    [SerializeField]
    private BuildingPropertiesType _BuildingPropertiesType = BuildingPropertiesType.None;

    public Building BuildingRef => _BuildingRef;
    public BuildingPropertiesType BuildingProperties => _BuildingPropertiesType;
}