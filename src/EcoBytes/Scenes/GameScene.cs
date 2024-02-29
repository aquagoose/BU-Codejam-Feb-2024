using System;
using System.Drawing;
using System.Numerics;
using EcoBytes.Components;
using EcoBytes.Data;
using EcoBytes.GUI;
using Pie.Windowing;
using u4.Engine;
using u4.Engine.Entities;
using u4.Engine.Scenes;
using u4.Math;
using Color = u4.Math.Color;
using TextElement = EcoBytes.GUI.TextElement;

namespace EcoBytes.Scenes;

public class GameScene : Scene
{
    /// <summary>
    /// The amount of time, in seconds, that a week takes to advance.
    /// </summary>
    public const float WeekAdvanceTime = 0.5f;
    
    private float _weekAdvanceCounter;
    public uint CurrentWeek;

    public int RemainingBudget;

    private Panel _upgradePanel;
    private bool _hasRefreshedPanel;
    private BuildingComponent _currentBuilding;
    
    public override void Initialize()
    {
        // Create Main Camera
        Entity mainCamera = new Entity("Camera");
        Camera camera = new Camera();
        mainCamera.AddComponent(camera);
        AddEntity(mainCamera);

        const float xOffset = -1000;
        const float scaleX = 3200;

        Entity grass = new Entity("Grass", new Transform(new Vector3(xOffset, 500, 0)));
        grass.AddComponent(new Sprite(EcoBytesGame.WhiteTexture) { Scale = new Vector2(scaleX, 300), Tint = Color.Green });
        AddEntity(grass);
        
        Entity road = new Entity("Road", new Transform(new Vector3(xOffset, 350, 0)));
        road.AddComponent(new Sprite(EcoBytesGame.WhiteTexture) { Scale = new Vector2(scaleX, 200), Tint = new Color(0.85f, 0.85f, 0.85f) } );
        AddEntity(road);

        Entity dorsetHouse = new Entity("DorsetHouse", new Transform(new Vector3(100, 100, 0)));
        dorsetHouse.AddComponent(new Sprite(EcoBytesGame.DorsetHouse));
        dorsetHouse.AddComponent(new BuildingComponent("DH"));
        dorsetHouse.AddComponent(new ClickableBuilding());
        AddEntity(dorsetHouse);

        Entity kimmeridgeHouse = new Entity("KimmeridgeHouse", new Transform(new Vector3(1000, 60, 0)));
        kimmeridgeHouse.AddComponent(new Sprite(EcoBytesGame.KimmeridgeHouse));
        kimmeridgeHouse.AddComponent(new BuildingComponent("KH"));
        kimmeridgeHouse.AddComponent(new ClickableBuilding());
        AddEntity(kimmeridgeHouse);

        Entity pooleGateway = new Entity("PooleGateway", new Transform(new Vector3(-800, 30, 0)));
        pooleGateway.AddComponent(new Sprite(EcoBytesGame.PooleGatewayBuilding));
        pooleGateway.AddComponent(new BuildingComponent("PGB"));
        pooleGateway.AddComponent(new ClickableBuilding());
        AddEntity(pooleGateway);

        const int padding = 10;

        Size<int> buttonSize = new Size<int>(30, 40);

        const int camMoveDist = 300;
        
        ImageButton leftArrow = new ImageButton("LeftArrow",
            new Point(1280 - (buttonSize.Width) * 2 - padding * 2, 720 - buttonSize.Height - padding), buttonSize,
            EcoBytesGame.LeftArrowTexture, () => camera.MoveCamera(-camMoveDist));
        UI.AddElement(leftArrow);

        ImageButton rightArrow = new ImageButton("RightArrow",
            new Point(1280 - buttonSize.Width - padding, 720 - buttonSize.Height - padding), buttonSize,
            EcoBytesGame.RightArrowTexture, () => camera.MoveCamera(camMoveDist));
        UI.AddElement(rightArrow);

        TextElement weekText =
            new TextElement("WeekText", new Point(20, 680), EcoBytesGame.Font, 20, "????????????????");
        UI.AddElement(weekText);

        ProgressBar weekProgress =
            new ProgressBar("WeekProgress", new Point(20, 705), new Size<int>(130, 10), Color.Black);
        UI.AddElement(weekProgress);

        TextElement money =
            new TextElement("Money", new Point(20, 650), EcoBytesGame.Font, 20, "£999999999999999999999");
        UI.AddElement(money);

        _upgradePanel = new Panel("UpgradePanel", new Point(20, 20), new Size<int>(1240, 680), new Color(1f, 1f, 1f, 0.85f));
        _upgradePanel.AddElement(new ImageButton("CloseButton", new Point(20, 20), new Size<int>(30, 30),
            EcoBytesGame.CloseButtonTexture, () => UI.RemoveElement(_upgradePanel.Name)));
        
        _upgradePanel.AddElement(new DescriptionPanel("DescriptionPanel", new Point(650, 75), new Size<int>(550, 300),
            EcoBytesGame.Font, "?????", "???????", 123456, 999999));
        
        Point buttonPosition = new Point(75, 75);
        foreach ((string id, Upgrade upgrade) in Upgrade.LoadedUpgrades)
        {
            Button upButton = new Button(id, buttonPosition, new Size<int>(500, 25), EcoBytesGame.Font, 20,
                upgrade.Name, () =>
                {
                    _currentBuilding.PurchaseUpgrade(id);
                    RefreshUpgradePanel();
                    _hasRefreshedPanel = false;
                });

            upButton.Hover = () =>
            {
                DescriptionPanel panel = _upgradePanel.GetElement<DescriptionPanel>("DescriptionPanel");
                Upgrade upgrade = Upgrade.LoadedUpgrades[upButton.Name];
                panel.Title = upgrade.Name;
                panel.Description = upgrade.Description;
                panel.Cost = upgrade.Cost;
                panel.Time = upgrade.BuildTime;
                panel.ElectricImpact = upgrade.ElecImpact;
                panel.GasImpact = upgrade.GasImpact;
            };

            upButton.UnHover = ResetDescription;
            
            _upgradePanel.AddElement(upButton);

            buttonPosition.Y += 30;
        }
        
        base.Initialize();

        CurrentWeek = 0;
        RemainingBudget = 500000;
    }

    public void OpenUpgradePanel(BuildingComponent building)
    {
        _currentBuilding = building;
        RefreshUpgradePanel();
        UI.AddElement(_upgradePanel);
    }

    public void RefreshUpgradePanel()
    {
        ResetDescription();

        bool isUpgrading = _currentBuilding.IsUpgrading(out _);
        
        foreach ((string id, Upgrade upgrade) in Upgrade.LoadedUpgrades)
        {
            Button button = _upgradePanel.GetElement<Button>(id);
            button.Progress = 0;

            if (isUpgrading || upgrade.Cost > RemainingBudget)
            {
                button.Enabled = false;
                continue;
            }
            
            button.Enabled = !_currentBuilding.PurchasedUpgrades.ContainsKey(id);
            
            if (!upgrade.ValidBuildings?.Contains(_currentBuilding.Id) ?? false)
                button.Enabled = false;
        }
    }

    public void ResetDescription()
    {
        DescriptionPanel panel = _upgradePanel.GetElement<DescriptionPanel>("DescriptionPanel");
        panel.Cost = -1;
        panel.Title = $"Upgrade {_currentBuilding.Name}.";
        panel.Description =
            $"Get any upgrade you want that's within your budget. Be wary though, some upgrades take far longer than others!\n\nHover over each upgrade to see a description here.\n\nUnavailable upgrades are greyed out. This may be because they're unavailable, completed, or you can't afford them.\n\nElectric: {_currentBuilding.ElectricConsumption:0.00}kWh\nGas: {_currentBuilding.GasConsumption:0.00}kWh";
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        _weekAdvanceCounter += dt;
        if (_weekAdvanceCounter >= WeekAdvanceTime)
        {
            _weekAdvanceCounter -= WeekAdvanceTime;
            CurrentWeek++;
        }

        UI.GetElement<ProgressBar>("WeekProgress").Progress = _weekAdvanceCounter / WeekAdvanceTime;

        if (_currentBuilding != null)
        {
            if (_currentBuilding.IsUpgrading(out string upgradeId))
            {
                BuildingComponent.PurchasedUpgrade pUpgrade = _currentBuilding.PurchasedUpgrades[upgradeId];
                Upgrade upgrade = Upgrade.LoadedUpgrades[upgradeId];

                float currentProgress = (CurrentWeek - pUpgrade.StartingWeek) / (float) upgrade.BuildTime;
                float nextProgress = ((CurrentWeek + 1) - pUpgrade.StartingWeek) / (float) upgrade.BuildTime;

                _upgradePanel.GetElement<Button>($"{upgradeId}").Progress = float.Lerp(currentProgress,
                    nextProgress, _weekAdvanceCounter / WeekAdvanceTime);
            }
            else
            {
                if (!_hasRefreshedPanel)
                {
                    RefreshUpgradePanel();
                    _hasRefreshedPanel = true;
                }
            }
        }
        
        UI.GetElement<TextElement>("WeekText").Text = $"{ (CurrentWeek / 52) + 2024 } Week {(CurrentWeek % 52) + 1}";
        UI.GetElement<TextElement>("Money").Text = RemainingBudget.ToString("C0");
        
        if (Input.KeyPressed(Key.P))
            EcoBytesGame.SetScene(new GameScene());
    }
}