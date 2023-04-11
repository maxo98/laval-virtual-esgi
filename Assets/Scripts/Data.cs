using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuildingData
{
    public float ElectricityConsumptionDay { get; private set; }
    public float ElectricityConsumptionNight { get; private set; }
}

public struct GeneratorData
{
    public float ElectricityProduction { get; set; }
}
