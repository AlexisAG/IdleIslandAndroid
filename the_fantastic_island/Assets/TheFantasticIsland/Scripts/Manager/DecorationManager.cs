﻿using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.DataSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using AgToolkit.Core.Pool;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class DecorationManager : Singleton<DecorationManager>, IBackup
    {
        [SerializeField]
        private List<Decoration> _DecorationsList = new List<Decoration>();

        private Dictionary<Decoration, int> _Wallets = new Dictionary<Decoration, int>();
        private Dictionary<Decoration, int> _DecoAvailable = new Dictionary<Decoration, int>();

        public void SetupInterface()
        {
            //todo
        }

        public IEnumerator Save()
        {
            //todo:
            yield break;
        }

        public IEnumerator Load()
        {
            //todo:
            yield break;
        }

        protected override void Awake()
        {
            foreach (Decoration d in _DecorationsList)
            {
                _Wallets.Add(d, 0);
                _DecoAvailable.Add(d, 0);
            }
        }

        public void BuyDecoration(Decoration d)
        {
            if (!ResourceManager.Instance.ChangeAmount(d.Resource, ResourceModificatorType.Cost, (int)(d.Cost * Mathf.Exp(_Wallets[d]) ))) return; // if there is not enough Resource, return

            _Wallets[d] += 1;
            _DecoAvailable[d] += 1;
        }

        public void PlaceDecoration(Decoration d)
        {
            if (_DecoAvailable[d] <= 0) return;

            DecorationInstance decoInstance = PoolManager.Instance.GetPooledObject(d.Prefab.name)?.GetComponent<DecorationInstance>();
            decoInstance.Init(d);
            //todo: configure decoInstance
            decoInstance.gameObject.SetActive(true);

            _DecoAvailable[d] -= 1;
        }

        public void TidyDecoration(Decoration d)
        {
            if (!_Wallets.ContainsKey(d)) return;
            if (_Wallets[d] <= 0) return;

            _DecoAvailable[d] += 1;
        }
    }
}
