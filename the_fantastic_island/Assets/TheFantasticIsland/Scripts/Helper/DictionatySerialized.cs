#if UNITY_EDITOR
using System;
using AgToolkit.Core.Helper.Drawer;
using AgToolkit.Core.Helper.Serialization;
using UnityEditor;

namespace TheFantasticIsland.Helper
{
    [Serializable]
    public class ResourceIntDictionary : SerializableDictionary<Resource, int>
    {
    }

    [CustomPropertyDrawer(typeof(ResourceIntDictionary))]
    public class ResourceIntDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {

    }
}
#endif