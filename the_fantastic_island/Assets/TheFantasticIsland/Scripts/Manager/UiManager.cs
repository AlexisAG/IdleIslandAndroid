using System;
using AgToolkit.AgToolkit.Core.Singleton;
using TheFantasticIsland.Helper;
using TheFantasticIsland.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace TheFantasticIsland.Manager
{
    public class UiManager : Singleton<UiManager>
    {
        [SerializeField]
        private ScrollableInterface _UiBuilding = null;
        [SerializeField]
        private Text _GoldText = null;
        [SerializeField]
        private Text _ToolsText = null;
        [SerializeField]
        private Text _KnowledgeText = null;
        [SerializeField]
        private Text _DiamondText = null;

        public ScrollableInterface UiBuilding => _UiBuilding;

        private string GetFormatedAmout(Resource r)
        {
            return ResourceManager.Instance.GetAmount(r).ToString(); // todo
        }

        public void UpdateResourceInfo(Resource r)
        {
            string s = GetFormatedAmout(r);

            switch (r)
            {
                case Resource.Gold:
                    _GoldText.text = s;
                    break;
                case Resource.Tools:
                    _ToolsText.text = s;
                    break;
                case Resource.Knowledge:
                    _KnowledgeText.text = s;
                    break;
                case Resource.Diamond:
                    _DiamondText.text = s;
                    break;
            }
        }
    }
}
