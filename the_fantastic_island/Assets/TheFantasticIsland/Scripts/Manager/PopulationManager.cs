using System.Collections;
using System.Collections.Generic;
using AgToolkit.AgToolkit.Core.DataSystem;
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

        private Dictionary<Population, List<PopulationInstance>> _Wallet = new Dictionary<Population, List<PopulationInstance>>();

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
            if (p is Animal a)
            {
                //todo: check if the condition is ok with building manager
            }

            if (!ResourceManager.Instance.ChangeAmount(p.Resource, ResourceModificatorType.Cost, (int)(p.Cost * Mathf.Exp(_Wallet[p].Count)))) return;

            PopulationInstance pop =  Instantiate(p.Prefab).GetComponent<PopulationInstance>();
            //todo configure populationInstance
            pop.gameObject.SetActive(true);

            _Wallet[p].Add(pop);
            BonusManager.Instance.AddBonus(p.Bonus);
        }
    }
}
