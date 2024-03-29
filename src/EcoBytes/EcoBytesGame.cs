﻿using System.Collections.Generic;
using System.IO;
using System.Numerics;
using EcoBytes.Components;
using EcoBytes.Data;
using EcoBytes.GUI;
using EcoBytes.Scenes;
using u4.Core;
using u4.Engine;
using u4.Engine.Entities;
using u4.Engine.Scenes;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;

namespace EcoBytes;

public class EcoBytesGame : Game
{
    public static Font Font;

    public static Texture WhiteTexture;

    public static Texture LeftArrowTexture;
    public static Texture RightArrowTexture;
    public static Texture CloseButtonTexture;
    
    public static Texture DorsetHouse;
    public static Texture KimmeridgeHouse;
    public static Texture PooleGatewayBuilding;

    public static Texture MoneyIcon;
    public static Texture TimeIcon;
    public static Texture ElectricIcon;
    public static Texture GasIcon;
    
    public override void Initialize()
    {
        Logger.Debug("Loading upgrades.");
        Upgrade.LoadUpgradesFromJson(File.ReadAllText("Content/Data/Upgrades.json"));
        
        Logger.Debug("Loading buildings.");
        Building.LoadBuildingsFromJson(File.ReadAllText("Content/Data/Buildings.json"));
        
        Logger.Debug("Loading carbon factors.");
        CarbonFactors.LoadFactorsFromJson(File.ReadAllText("Content/Data/CarbonFactors.json"));
        
        Logger.Debug("Loading font.");
        Font = new Font(Graphics.Device, "Content/Roboto-Regular.ttf");
        
        Logger.Debug("Loading textures.");
        WhiteTexture = new Texture(new byte[]{ 255, 255, 255, 255 }, new Size<int>(1, 1));
        LeftArrowTexture = new Texture("Content/Textures/LeftArrow.png");
        RightArrowTexture = new Texture("Content/Textures/RightArrow.png");
        CloseButtonTexture = new Texture("Content/Textures/CloseButton.png");
        DorsetHouse = new Texture("Content/Textures/dh.png");
        KimmeridgeHouse = new Texture("Content/Textures/kh.png");
        PooleGatewayBuilding = new Texture("Content/Textures/pgb.png");
        MoneyIcon = new Texture("Content/Textures/money.png");
        TimeIcon = new Texture("Content/Textures/clock.png");
        ElectricIcon = new Texture("Content/Textures/plug.png");
        GasIcon = new Texture("Content/Textures/fext.png");
        
        base.Initialize();
    }

    public override void Update(float dt)
    {
        UI.Update();
        base.Update(dt);
    }

    public override void Draw()
    {
        Graphics.Device.ClearColorBuffer(Color.CornflowerBlue);
        SpriteRenderer renderer = Graphics.SpriteRenderer;
        
        // Declare Camera Transform Matrix
        Matrix4x4 camTransform = Matrix4x4.Identity;
        
        // Check for a camera in current context
        if (SceneManager.CurrentScene.TryGetEntity("Camera", out Entity camera))
            camTransform = camera.GetComponent<Camera>().CamTranslation;
        
        // u4's main renderer currently isn't working well enough for it to draw sprites yet.
        // Fortunately, for this project, we only need the sprite renderer, so we can just use it directly for the moment.
        renderer.Begin(camTransform);
        base.Draw();
        renderer.End();
        
        UI.Draw();
    }

    public static void SetScene(Scene scene)
    {
        UI.ClearElements();
        SceneManager.SetScene(scene);
    }
}