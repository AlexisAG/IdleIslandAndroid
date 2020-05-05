using System.Collections;
using System.Collections.Generic;
using AgToolkit.Core.Helper.Events;
using AgToolkit.Core.Helper.Events.Listeners;
using TheFantasticIsland.DataScript;
using UnityEngine;

namespace TheFantasticIsland.Helper
{
    public class BuildingActionGameEventListener : GameEventListener<BuildingAction, BuildingActionGameEvent, BuildingActionUnityEvent> { }
    public class PopulationActionGameEventListener : GameEventListener<PopulationAction, PopulationActionGameEvent, PopulationActionUnityEvent> { }
    public class GiftActionGameEventListener : GameEventListener<GiftAction, GiftActionGameEvent, GiftActionUnityEvent> { }
}
