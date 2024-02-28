using System.Collections.Generic;
using u4.Engine.Entities;

namespace EcoBytes.Components;

public class BuildingComponent : Component
{
    public readonly string Name;
    
    public readonly Dictionary<string, PurchasedUpgrade> PurchasedUpgrades;

    public BuildingComponent(string name)
    {
        Name = name;
        PurchasedUpgrades = new Dictionary<string, PurchasedUpgrade>();
    }

    public class PurchasedUpgrade
    {
        
    }
}