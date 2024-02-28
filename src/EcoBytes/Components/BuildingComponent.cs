using System.Collections.Generic;
using EcoBytes.Data;
using EcoBytes.Scenes;
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

    public override void Update(float dt)
    {
        base.Update(dt);

        uint currentWeek = GameScene.CurrentWeek;
        
        foreach ((string uId, PurchasedUpgrade upgrade) in PurchasedUpgrades)
        {
            if (upgrade.Progress != UpgradeProgress.Building)
                continue;
            
            Upgrade upgradeInfo = Upgrade.LoadedUpgrades[uId];

            if (currentWeek - upgrade.StartingWeek >= upgradeInfo.BuildTime)
                upgrade.Progress = UpgradeProgress.Completed;
        }
    }

    public class PurchasedUpgrade
    {
        /// <summary>
        /// The initial week this upgrade was purchased.
        /// </summary>
        public uint StartingWeek;

        /// <summary>
        /// The current progress of the building.
        /// </summary>
        public UpgradeProgress Progress;
    }

    public enum UpgradeProgress
    {
        Building,
        Completed
    }
}