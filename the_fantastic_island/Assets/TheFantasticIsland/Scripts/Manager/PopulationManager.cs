using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.BackupSystem;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.DataScript;
using TheFantasticIsland.Helper;
using UnityEngine;

namespace TheFantasticIsland.Manager
{
    public class PopulationManager : Singleton<PopulationManager>, IBackup
    {
        [SerializeField]
        private List<Population> _Populations = new List<Population>();

        private Dictionary<Population, int> _Wallet = new Dictionary<Population, int>();

        public void SetupInterface()
        {
            //todo
        }

        public IEnumerator Save()
        {
            throw new System.NotImplementedException();
            //todo
        }

        public IEnumerator Load()
        {
            throw new System.NotImplementedException();
            //todo
        }

        public void BuyPopulation(Population p)
        {
            if (p.Cost.Type != ResourceModificatorType.Cost)
            {
                Debug.Assert(false, $"Population may have a {ResourceModificatorType.Cost} and not a {p.Cost.Type} (ResourceModificatorType)");
                return;
            }

            if (p is Animal a)
            {
                //todo: check if the condition is ok with building manager
            }

            p.Cost.AdjustAmount(_Wallet[p]); // adjust price for the decoration (security)

            if (!ResourceManager.Instance.ChangeAmount(p.Cost.Resource, p.Cost.Amount)) return;


            PopulationInstance pop =  Instantiate(p.Prefab).GetComponent<PopulationInstance>();
            //todo configure populationInstance
            pop.gameObject.SetActive(true);

            _Wallet[p] += 1;

            p.Cost.AdjustAmount(_Wallet[p]);

            BonusManager.Instance.AddBonus(p.Bonus);
        }
    }
}
