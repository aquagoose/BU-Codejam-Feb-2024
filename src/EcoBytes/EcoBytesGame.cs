using System.Collections.Generic;
using System.IO;
using System.Numerics;
using EcoBytes.Components;
using EcoBytes.Data;
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
    public static List<SpriteDrawInfo> Sprites;
    public static List<SpriteDrawInfo> Ui;
    
    public static GameScene GameScene;
    
    public static Font Font;

    public static Texture WhiteTexture;
    
    public static Texture DorsetHouse;

    public EcoBytesGame(GameScene scene)
    {
        GameScene = scene;

        Sprites = new List<SpriteDrawInfo>();
        Ui = new List<SpriteDrawInfo>();
    }
    
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
        DorsetHouse = new Texture("Content/Textures/dh.png");
        
        base.Initialize();
    }

    public override void Draw()
    {
        Graphics.Device.ClearColorBuffer(Color.Black);
        Sprites.Clear();
        Ui.Clear();
        
        // u4's main renderer currently isn't working well enough for it to draw sprites yet.
        // Fortunately, for this project, we only need the sprite renderer, so we can just use it directly for the moment. 
        base.Draw();

        SpriteRenderer renderer = Graphics.SpriteRenderer;
        
        // Declare Camera Transform Matrix
        Matrix4x4 camTransform = Matrix4x4.Identity;
        
        // Check for a camera in current context
        if (SceneManager.CurrentScene.TryGetEntity("Camera", out Entity camera))
        {
            camTransform = camera.GetComponent<Camera>().CamTranslation;
        }
        
        renderer.Begin(camTransform);
        foreach (SpriteDrawInfo info in Sprites)
            renderer.Draw(info.Texture, info.Position, info.Tint, 0, info.Scale, Vector2.Zero);
        renderer.End();
        
        renderer.Begin();
        foreach (SpriteDrawInfo info in Ui)
            renderer.Draw(info.Texture, info.Position, info.Tint, 0, info.Scale, Vector2.Zero);
        renderer.End();
    }

    public struct SpriteDrawInfo
    {
        public Texture Texture;
        public Vector2 Position;
        public Vector2 Scale;
        public Color Tint;

        public SpriteDrawInfo(Texture texture, Vector2 position, Vector2 scale, Color tint)
        {
            Texture = texture;
            Position = position;
            Scale = scale;
            Tint = tint;
        }
    }
}