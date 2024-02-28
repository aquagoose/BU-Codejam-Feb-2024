using System.Collections.Generic;
using Newtonsoft.Json;

namespace EcoBytes.Data;

public readonly struct Building
{
    /// <summary>
    /// Building name.
    /// </summary>
    public readonly string Name;
    
    /// <summary>
    /// Average electric consumption.
    /// </summary>
    public readonly double Elec;
    
    /// <summary>
    /// Average gas consumption.
    /// </summary>
    public readonly double Gas;

    public Building(string name, double elec, double gas)
    {
        Name = name;
        Elec = elec;
        Gas = gas;
    }

    public static Dictionary<string, Building> LoadedBuildings;

    public static void LoadBuildingsFromJson(string json)
    {
        LoadedBuildings = JsonConvert.DeserializeObject<Dictionary<string, Building>>(json);
    }
}