using System;
using AgToolkit.Core.Helper.Events;
using TheFantasticIsland.DataScript;
using UnityEngine;

namespace TheFantasticIsland.Helper
{
    [Serializable]
    public class BuildingActionGameEvent : GameEvent<BuildingAction> { }
    [Serializable]
    public class PopulationActionGameEvent : GameEvent<PopulationAction> { }
    [Serializable]
    public class GiftActionGameEvent : GameEvent<GiftAction> { }
}
