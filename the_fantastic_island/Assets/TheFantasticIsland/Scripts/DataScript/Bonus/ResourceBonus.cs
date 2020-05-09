using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "TheFantasticIsland/Bonus/Resource", fileName = "NewResourceBonus")]
public class ResourceBonus : Bonus
{
    [SerializeField]
    private Resource _Resource = Resource.None;

    public Resource Resource => _Resource;
}