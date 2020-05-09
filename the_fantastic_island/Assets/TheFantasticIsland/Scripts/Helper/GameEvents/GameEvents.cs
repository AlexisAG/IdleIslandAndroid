using System;
using AgToolkit.Core.Helper.Events;
using TheFantasticIsland.DataScript;
using UnityEngine;

namespace TheFantasticIsland.Helper
{
    [Serializable, CreateAssetMenu(menuName = "TheFantasticIsland/GameEvents/Building", fileName = "NewBuildingGameEvent")]
    public class BuildingActionGameEvent : GameEvent<BuildingAction> { }
    [Serializable, CreateAssetMenu(menuName = "TheFantasticIsland/GameEvents/Population", fileName = "NewPopulationGameEvent")]
    public class PopulationActionGameEvent : GameEvent<PopulationAction> { }
    [Serializable, CreateAssetMenu(menuName = "TheFantasticIsland/GameEvents/Gift", fileName = "NewGiftGameEvent")]
    public class GiftActionGameEvent : GameEvent<GiftAction> { }
}
