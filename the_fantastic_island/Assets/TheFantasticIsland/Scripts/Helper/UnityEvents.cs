using System;
using AgToolkit.Core.Helper.GameVars;
using TheFantasticIsland.DataScript;
using UnityEngine;
using UnityEngine.Events;

namespace TheFantasticIsland.Helper
{
    public class BuildingActionUnityEvent : UnityEvent<BuildingAction> { }
    public class PopulationActionUnityEvent : UnityEvent<PopulationAction> { }
    public class GiftActionUnityEvent : UnityEvent<GiftAction> { }
}