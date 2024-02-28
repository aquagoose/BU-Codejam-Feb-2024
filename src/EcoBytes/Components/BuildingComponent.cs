using System;
using System.Collections.Generic;
using EcoBytes.Data;
using EcoBytes.Exceptions;
using EcoBytes.Scenes;
using u4.Engine.Entities;

namespace EcoBytes.Components;

public class BuildingComponent : Component
{
    public readonly string Id;
    
    public readonly Dictionary<string, PurchasedUpgrade> PurchasedUpgrades;

    public string Name => Building.LoadedBuildings[Id].Name;

    public BuildingComponent(string id)
    {
        Id = id;
        PurchasedUpgrades = new Dictionary<string, PurchasedUpgrade>();
    }

    public void PurchaseUpgrade(string upgradeId)
    {
        if (PurchasedUpgrades.ContainsKey(upgradeId))
            throw new UpgradePurchasedException(Upgrade.LoadedUpgrades[upgradeId].Name);
        
        PurchasedUpgrades.Add(upgradeId, new PurchasedUpgrade(GameScene.CurrentWeek));
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

        public PurchasedUpgrade(uint currentWeek)
        {
            StartingWeek = currentWeek;
            Progress = UpgradeProgress.Building;
        }
    }

    public enum UpgradeProgress
    {
        Building,
        Completed
    }
}