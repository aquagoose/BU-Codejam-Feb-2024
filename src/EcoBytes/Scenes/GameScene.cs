using System;
using System.Drawing;
using System.Numerics;
using EcoBytes.Components;
using EcoBytes.Data;
using EcoBytes.GUI;
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
    public const float WeekAdvanceTime = 2.0f;
    
    private float _weekAdvanceCounter;
    public static uint CurrentWeek;
    
    public override void Initialize()
    {
        // Create Main Camera
        Entity mainCamera = new Entity("Camera");
        Camera camera = new Camera();
        mainCamera.AddComponent(camera);
        AddEntity(mainCamera);

        const int padding = 10;

        Size<int> buttonSize = new Size<int>(30, 40);

        ImageButton leftArrow = new ImageButton("LeftArrow",
            new Point(1280 - (buttonSize.Width) * 2 - padding * 2, 720 - buttonSize.Height - padding), buttonSize,
            EcoBytesGame.LeftArrowTexture, () => camera.MoveCamera(-10));
        UI.AddElement(leftArrow);

        ImageButton rightArrow = new ImageButton("RightArrow",
            new Point(1280 - buttonSize.Width - padding, 720 - buttonSize.Height - padding), buttonSize,
            EcoBytesGame.RightArrowTexture, () => camera.MoveCamera(10));
        UI.AddElement(rightArrow);

        TextElement weekText =
            new TextElement("WeekText", new Point(20, 690), EcoBytesGame.Font, 20, "????????????????");
        UI.AddElement(weekText);

        Panel upgradePanel = new Panel("UpgradePanel", new Point(20, 20), new Size<int>(1240, 680), Color.White);
        upgradePanel.AddElement(new ImageButton("CloseButton", new Point(20, 20), new Size<int>(30, 30),
            EcoBytesGame.CloseButtonTexture, () => UI.RemoveElement(upgradePanel.Name)));

        const string bName = "PGB";
        
        Point buttonPosition = new Point(50, 75);
        foreach ((string id, Upgrade upgrade) in Upgrade.LoadedUpgrades)
        {
            Button upButton = new Button($"{id}UpButton", buttonPosition, new Size<int>(500, 25), EcoBytesGame.Font, 20,
                upgrade.Name, () => Console.WriteLine("youir mum"));

            if (!upgrade.ValidBuildings?.Contains(bName) ?? false)
                upButton.Enabled = false;
            
            upgradePanel.AddElement(upButton);

            buttonPosition.Y += 30;
        }
        
        UI.AddElement(upgradePanel);
        
        base.Initialize();

        CurrentWeek = 1;
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

        UI.GetElement<TextElement>("WeekText").Text = $"Week {CurrentWeek}";
    }
}