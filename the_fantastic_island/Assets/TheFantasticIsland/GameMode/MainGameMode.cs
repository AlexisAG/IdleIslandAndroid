using System.Collections;
using AgToolkit.Core.GameModes;
using TheFantasticIsland.Manager;
using UnityEngine;

namespace TheFantasticIsland.GameModes
{
    public class MainGameMode : GameMode
    {
        public override IEnumerator OnLoad() {
            yield return base.OnLoad();

            //todo pool

            //Load
            yield return TaskManager.Instance.Load();
            yield return BonusManager.Instance.Load();
            yield return BuildingManager.Instance.Load();
            yield return DecorationManager.Instance.Load();
            yield return GiftManager.Instance.Load();
        }
    }
}
