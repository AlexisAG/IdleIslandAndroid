using System;
using AgToolkit.Core.Helper.GameVars;
using TheFantasticIsland.DataScript;
using UnityEngine;
using UnityEngine.Events;

namespace TheFantasticIsland.Helper
{
    [Serializable]
    public class BuildingActionUnityEvent : UnityEvent<BuildingAction> { }
    [Serializable]
    public class PopulationActionUnityEvent : UnityEvent<PopulationAction> { }
    [Serializable]
    public class GiftActionUnityEvent : UnityEvent<GiftAction> { }
}