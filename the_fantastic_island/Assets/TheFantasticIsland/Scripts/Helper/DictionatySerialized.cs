using System;
using AgToolkit.Core.Helper.Serialization;
using UnityEditor;
#if UNITY_EDITOR
using AgToolkit.Core.Helper.Drawer;
#endif

namespace TheFantasticIsland.Helper
{
    [Serializable]
    public class ResourceIntDictionary : SerializableDictionary<Resource, int>
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ResourceIntDictionary))]
    public class ResourceIntDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {

    }
#endif
}