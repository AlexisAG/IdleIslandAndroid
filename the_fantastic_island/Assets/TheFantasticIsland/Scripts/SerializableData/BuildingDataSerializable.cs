using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingDataSerializable : DataSerializable
{
    public string BuildingId { get; private set; }
    public int SizeLevel { get; private set; }
    public int ProductionLevel { get; private set; }

    public BuildingDataSerializable(string id, int size, int prod)
    {
        BuildingId = id;
        SizeLevel = size;
        ProductionLevel = prod;
    }
}
