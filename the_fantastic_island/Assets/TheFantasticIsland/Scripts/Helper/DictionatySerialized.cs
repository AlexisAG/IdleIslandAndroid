using System;
using System.Collections;
using System.Collections.Generic;
using AgToolkit.Core.Helper.Drawer;
using AgToolkit.Core.Helper.Serialization;
using UnityEditor;
using UnityEngine;

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
    
