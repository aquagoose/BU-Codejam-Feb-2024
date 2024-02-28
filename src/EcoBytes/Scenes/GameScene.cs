﻿using System;
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
    public const float WeekAdvanceTime = 1f;
    
    private float _weekAdvanceCounter;
    public static uint CurrentWeek;

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

        Entity dorsetHouse = new Entity("DorsetHouse", new Transform(new Vector3(100, 100, 0)));
        dorsetHouse.AddComponent(new Sprite(EcoBytesGame.DorsetHouse));
        dorsetHouse.AddComponent(new BuildingComponent("DH"));
        dorsetHouse.AddComponent(new ClickableBuilding());
        AddEntity(dorsetHouse);

        Entity kimmeridgeHouse = new Entity("KimmeridgeHouse", new Transform(new Vector3(1000, 100, 0)));
        kimmeridgeHouse.AddComponent(new Sprite(EcoBytesGame.KimmeridgeHouse));
        kimmeridgeHouse.AddComponent(new BuildingComponent("KH"));
        kimmeridgeHouse.AddComponent(new ClickableBuilding());
        AddEntity(kimmeridgeHouse);

        Entity pooleGateway = new Entity("PooleGateway", new Transform(new Vector3(-800, 100, 0)));
        pooleGateway.AddComponent(new Sprite(EcoBytesGame.PooleGatewayBuilding));
        pooleGateway.AddComponent(new BuildingComponent("PGB"));
        pooleGateway.AddComponent(new ClickableBuilding());
        AddEntity(pooleGateway);

        const int padding = 10;

        Size<int> buttonSize = new Size<int>(30, 40);

        const int camMoveDist = 200;
        
        ImageButton leftArrow = new ImageButton("LeftArrow",
            new Point(1280 - (buttonSize.Width) * 2 - padding * 2, 720 - buttonSize.Height - padding), buttonSize,
            EcoBytesGame.LeftArrowTexture, () => camera.MoveCamera(-camMoveDist));
        UI.AddElement(leftArrow);

        ImageButton rightArrow = new ImageButton("RightArrow",
            new Point(1280 - buttonSize.Width - padding, 720 - buttonSize.Height - padding), buttonSize,
            EcoBytesGame.RightArrowTexture, () => camera.MoveCamera(camMoveDist));
        UI.AddElement(rightArrow);

        TextElement weekText =
            new TextElement("WeekText", new Point(20, 690), EcoBytesGame.Font, 20, "????????????????");
        UI.AddElement(weekText);

        _upgradePanel = new Panel("UpgradePanel", new Point(20, 20), new Size<int>(1240, 680), Color.White);
        _upgradePanel.AddElement(new ImageButton("CloseButton", new Point(20, 20), new Size<int>(30, 30),
            EcoBytesGame.CloseButtonTexture, () => UI.RemoveElement(_upgradePanel.Name)));
        
        Point buttonPosition = new Point(50, 75);
        foreach ((string id, Upgrade upgrade) in Upgrade.LoadedUpgrades)
        {
            Button upButton = new Button($"{id}UpButton", buttonPosition, new Size<int>(500, 25), EcoBytesGame.Font, 20,
                upgrade.Name, () =>
                {
                    _currentBuilding.PurchaseUpgrade(id);
                    RefreshUpgradePanel();
                    _hasRefreshedPanel = false;
                });
            
            _upgradePanel.AddElement(upButton);

            buttonPosition.Y += 30;
        }
        
        base.Initialize();

        CurrentWeek = 1;
    }

    public void OpenUpgradePanel(BuildingComponent building)
    {
        _currentBuilding = building;
        RefreshUpgradePanel();
        UI.AddElement(_upgradePanel);
    }

    public void RefreshUpgradePanel()
    {
        foreach ((string id, Upgrade upgrade) in Upgrade.LoadedUpgrades)
        {
            Button button = _upgradePanel.GetElement<Button>($"{id}UpButton");
            button.Progress = 0;

            if (_currentBuilding.IsUpgrading(out _))
            {
                button.Enabled = false;
                continue;
            }
            
            button.Enabled = !_currentBuilding.PurchasedUpgrades.ContainsKey(id);
            
            if (!upgrade.ValidBuildings?.Contains(_currentBuilding.Id) ?? false)
                button.Enabled = false;
        }
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
        
        if (Input.KeyPressed(Key.P))
            OpenUpgradePanel(_currentBuilding);

        if (_currentBuilding != null)
        {
            if (_currentBuilding.IsUpgrading(out string upgradeId))
            {
                BuildingComponent.PurchasedUpgrade pUpgrade = _currentBuilding.PurchasedUpgrades[upgradeId];
                Upgrade upgrade = Upgrade.LoadedUpgrades[upgradeId];

                float currentProgress = (CurrentWeek - pUpgrade.StartingWeek) / (float) upgrade.BuildTime;
                float nextProgress = ((CurrentWeek + 1) - pUpgrade.StartingWeek) / (float) upgrade.BuildTime;

                _upgradePanel.GetElement<Button>($"{upgradeId}UpButton").Progress = float.Lerp(currentProgress,
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
        
        UI.GetElement<TextElement>("WeekText").Text = $"Week {CurrentWeek}";
    }
}