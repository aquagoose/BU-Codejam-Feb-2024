using System;
using System.Collections.Generic;
using EcoBytes.Data;
using EcoBytes.Exceptions;
using EcoBytes.Scenes;
using u4.Engine.Entities;
using u4.Engine.Scenes;

namespace EcoBytes.Components;

public class BuildingComponent : Component
{
    public readonly string Id;
    
    public readonly Dictionary<string, PurchasedUpgrade> PurchasedUpgrades;

    public string Name => Building.LoadedBuildings[Id].Name;

    public double ElectricConsumption
    {
        get
        {
            double consumption = Building.LoadedBuildings[Id].Elec;

            foreach ((string id, PurchasedUpgrade pUpgrade) in PurchasedUpgrades)
            {
                if (pUpgrade.Progress != UpgradeProgress.Completed)
                    continue;
                
                Upgrade upgrade = Upgrade.LoadedUpgrades[id];

                if (upgrade.ImpactAsKwh)
                    consumption += upgrade.ElecImpact;
                else
                {
                    double elecPercent = upgrade.ElecImpact / 100.0;
                    consumption += consumption * elecPercent;
                }
            }

            return consumption;
        }
    }

    public double GasConsumption
    {
        get
        {
            double consumption = Building.LoadedBuildings[Id].Gas;

            foreach ((string id, PurchasedUpgrade pUpgrade) in PurchasedUpgrades)
            {
                if (pUpgrade.Progress != UpgradeProgress.Completed)
                    continue;
                
                Upgrade upgrade = Upgrade.LoadedUpgrades[id];

                if (upgrade.ImpactAsKwh)
                    consumption += upgrade.GasImpact;
                else
                {
                    double gasPercent = upgrade.GasImpact / 100.0;
                    consumption += consumption * gasPercent;
                }
            }

            return consumption;
        }
    }

    public BuildingComponent(string id)
    {
        Id = id;
        PurchasedUpgrades = new Dictionary<string, PurchasedUpgrade>();
    }

    public void PurchaseUpgrade(string upgradeId)
    {
        if (IsUpgrading(out _))
            throw new MultipleUpgradeException();

        if (PurchasedUpgrades.ContainsKey(upgradeId))
            throw new UpgradePurchasedException(Upgrade.LoadedUpgrades[upgradeId].Name);
        
        PurchasedUpgrades.Add(upgradeId, new PurchasedUpgrade(((GameScene) SceneManager.CurrentScene).CurrentWeek));
        ((GameScene) SceneManager.CurrentScene).RemainingBudget -= Upgrade.LoadedUpgrades[upgradeId].Cost;
    }
    
    public bool IsUpgrading(out string upgradeId)
    {
        upgradeId = null;
        
        foreach ((string id, PurchasedUpgrade upgrade) in PurchasedUpgrades)
        {
            if (upgrade.Progress == UpgradeProgress.Building)
            {
                upgradeId = id;
                return true;
            }
        }

        return false;
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        uint currentWeek = ((GameScene) SceneManager.CurrentScene).CurrentWeek;
        
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