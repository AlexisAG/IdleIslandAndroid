using System;
using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.Singleton;
using AgToolkit.AgToolkit.Core.Timer;
using TheFantasticIsland.DataScript;
using UnityEngine;
using UnityEngine.Events;

namespace TheFantasticIsland.Manager
{
    public class GiftManager : Singleton<GiftManager>
    {
        [SerializeField]
        private List<Gift> _Gifts = new List<Gift>();

        private readonly Dictionary<Gift, Timer> _TimersGift = new Dictionary<Gift, Timer>();

        protected override void Awake()
        {
            base.Awake();

            foreach (var g in _Gifts)
            {
                UnityEvent unityEvent = new UnityEvent();
                unityEvent.AddListener( (() => {InstantiateGameObject(g);}) );

                Timer timer = new Timer(g.Id, g.Timer, unityEvent);

                _TimersGift.Add(g, timer);
                StartTimer(g);
            }
        }

        private bool GiftExist(Gift g)
        {
            int index = _Gifts.IndexOf(g);

            if (index >= 0) return true;

            Debug.Assert(false, $"Gift {g.Id} is not refferenced in the GiftManager");

            return false;
        }

        private void InstantiateGameObject(Gift g)
        {
            GameObject.Instantiate(g.Prefab);
        }

        public void StartTimer(Gift g)
        {
            if (!GiftExist(g)) return;

            TimerManager.Instance.StartTimer(_TimersGift[g]);
        }

        public void ResetTimer(Gift g)
        {
            if (!GiftExist(g)) return;

            TimerManager.Instance.ResetTimer(_TimersGift[g]);
        }

    }
}
