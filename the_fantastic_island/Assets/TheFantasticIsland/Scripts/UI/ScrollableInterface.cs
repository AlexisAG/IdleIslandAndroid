using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheFantasticIsland.Ui
{
    public class ScrollableInterface : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Contents = null;
        [SerializeField]
        private GameObject _Title = null;


        private void Awake()
        {
            Debug.Assert(_Contents != null, $"{gameObject.name} Content is null.");
        }

        public void AddContent(GameObject obj)
        {
            obj.transform.SetParent(_Contents.transform);
            obj.transform.SetAsLastSibling();
        }

        public void AddContent(GameObject obj, int place)
        {
            obj.transform.SetParent(_Contents.transform);
            obj.transform.SetSiblingIndex(place);
        }

        public void RemoveContent(int index)
        {
            Transform obj = _Contents.transform.GetChild(index);

            Debug.Assert(obj != null, $"There is no object in {gameObject.name} contents with index {index}.");
            if (obj == null) return;

            Destroy(obj);
        }

        public void RemoveContent(GameObject obj)
        {
            if (!obj.transform.IsChildOf(_Contents.transform)) return;

            Destroy(obj);
        }

        public void ClearContents()
        {
            for (int i = 0; i < _Contents.transform.childCount; i++)
            {
                Destroy(_Contents.transform.GetChild(i).gameObject);
            }
        }

        public void SetTitle(string title)
        {
            _Title.GetComponentInChildren<Text>().text = title;
        }
    }
}
