using TheFantasticIsland.Helper;
using TheFantasticIsland.Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TheFantasticIsland.Ui
{
    public class ContentBuildingUI : MonoBehaviour
    {
        [SerializeField]
        private Image _Icon = null;
        [SerializeField]
        private Text _Title = null;
        [SerializeField]
        private Text _Description = null;
        [SerializeField]
        private Text _Info = null;
        [SerializeField]
        private Text _PorductionLevel = null;
        [SerializeField]
        private Text _SizeLevel = null;
        [SerializeField]
        private Button _Unlock = null;
        [SerializeField]
        private Button _IncreaseProduction = null;
        [FormerlySerializedAs("_IncreaseLevel")] [SerializeField]
        private Button _IncreaseSize = null;
        [SerializeField]
        private GameObject _ProductionChild = null;
        [SerializeField]
        private GameObject _SizeChild = null;

        private float _ProductionAmount = 0f;
        private float _SizeAmount = 0f;
        private Resource _ProductionCostR = Resource.None;
        private Resource _SizeCostR = Resource.None;

        public Button Unlock => _Unlock;
        public Button IncreaseProduction => _IncreaseProduction;
        public Button IncreaseSize => _IncreaseSize;

        private void Awake()
        {
            _ProductionChild.SetActive(false);
            _SizeChild.SetActive(false);
            IncreaseProduction.gameObject.GetComponent<Image>().color = Color.gray;
            IncreaseSize.gameObject.GetComponent<Image>().color = Color.gray;
        }

        private void LateUpdate()
        {
            if (_ProductionCostR == Resource.None || _SizeCostR == Resource.None) return;

            if (Unlock.gameObject.activeSelf)
            {
                bool unlockStatus = false;
                Unlock.enabled = ResourceManager.Instance.GetAmount(_ProductionCostR) >= _ProductionAmount;

                if (unlockStatus != Unlock.enabled) {
                    Unlock.gameObject.GetComponent<Image>().color = Unlock.enabled ? Color.green : Color.gray;
                }
            }

            if (!IncreaseProduction.gameObject.activeSelf || !IncreaseSize.gameObject.activeSelf) return;

            bool prodStatus = IncreaseProduction.enabled;
            bool sizeStatus = IncreaseSize.enabled;

            IncreaseProduction.enabled = ResourceManager.Instance.GetAmount(_ProductionCostR) >= _ProductionAmount;
            IncreaseSize.enabled = ResourceManager.Instance.GetAmount(_SizeCostR) >= _SizeAmount;

            // Set Color Button
            if (prodStatus != IncreaseProduction.enabled)
            {
                IncreaseProduction.gameObject.GetComponent<Image>().color = IncreaseProduction.enabled ? Color.green : Color.gray;
            }
            if (sizeStatus != IncreaseSize.enabled)
            {
                IncreaseSize.gameObject.GetComponent<Image>().color = IncreaseSize.enabled ? Color.green : Color.gray;
            }

        }

        public void SetIcon(Sprite s)
        {
            _Icon.sprite = s;
        }

        public void SetName(string title)
        {
            _Title.text = title;
        }

        public void SetDescription(string desc)
        {
            _Description.text = desc;
        }

        public void SetProductionInfo(string productionInfo)
        {
            _Info.text = productionInfo;
        }

        public void SetProductionLevel(int level)
        {
            _PorductionLevel.text = $"Current level: {level}";
        }
        public void SetSizeLevel(int level)
        {
            _SizeLevel.text = $"Current level: {level}";
        }

        public void ContentUnlocked()
        {
            _ProductionChild.SetActive(true);
            _SizeChild.SetActive(true);
            _Unlock.gameObject.SetActive(false);
        }

        public void SetAmount(float prod, float size)
        {
            _ProductionAmount = prod;
            _SizeAmount = size;
        }

        public void SetCostResourceType(Resource prod, Resource size)
        {
            _ProductionCostR = prod;
            _SizeCostR = size;
        }
    }
}
