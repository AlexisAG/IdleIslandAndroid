using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "TheFantasticIsland/Bonus/Building", fileName = "NewBuildingBonus")]
public class BuildingBonus : Bonus
{
    [SerializeField]
    private Building _BuildingRef = null;
    [SerializeField]
    private BuildingPropertiesType _BuildingProperties = BuildingPropertiesType.None;

    public Building BuildingRef => _BuildingRef;
    public BuildingPropertiesType BuildingProperties => _BuildingProperties;
}