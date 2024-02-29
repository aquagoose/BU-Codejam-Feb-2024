using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EcoBytes.Data;

public readonly struct Upgrade
{
    /// <summary>
    /// The name of the upgrade, as displayed in-game.
    /// </summary>
    public readonly string Name;
    
    /// <summary>
    /// The cost of the upgrade.
    /// </summary>
    public readonly int Cost;
    
    /// <summary>
    /// The build time, in weeks.
    /// </summary>
    public readonly int BuildTime;
    
    /// <summary>
    /// The electricity impact, in percent.
    /// </summary>
    public readonly double ElecImpact;
    
    /// <summary>
    /// The gas impact, in percent.
    /// </summary>
    public readonly double GasImpact;

    /// <summary>
    /// If true, the impacts will be interpreted in kWh instead of percent. 
    /// </summary>
    public readonly bool ImpactAsKwh;

    /// <summary>
    /// If not null, then this upgrade only works in certain buildings.
    /// </summary>
    public readonly HashSet<string> ValidBuildings;
    
    /// <summary>
    /// The description of the upgrade, as displayed in-game.
    /// </summary>
    public readonly string Description;

    public Upgrade(string name, int cost, int buildTime, double elecImpact, double gasImpact, bool impactAsKwh, HashSet<string> validBuildings, string description)
    {
        Name = name;
        Cost = cost;
        BuildTime = buildTime;
        ElecImpact = elecImpact;
        GasImpact = gasImpact;
        ImpactAsKwh = impactAsKwh;
        ValidBuildings = validBuildings;
        Description = description;
    }

    public static Dictionary<string, Upgrade> LoadedUpgrades;

    public static void LoadUpgradesFromJson(string json)
    {
       LoadedUpgrades = JsonConvert.DeserializeObject<Dictionary<string, Upgrade>>(json);
    }
}