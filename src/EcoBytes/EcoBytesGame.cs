using System.IO;
using System.Numerics;
using EcoBytes.Data;
using u4.Core;
using u4.Engine;
using u4.Math;
using u4.Render;

namespace EcoBytes;

public class EcoBytesGame : Game
{
    public static Font Font;

    public static Texture DorsetHouse;
    
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
        DorsetHouse = new Texture("Content/Textures/dh.png");
        
        base.Initialize();
    }

    public override void Draw()
    {
        Graphics.Device.ClearColorBuffer(Color.Black);
        
        // u4's main renderer currently isn't working well enough for it to draw sprites yet.
        // Fortunately, for this project, we only need the sprite renderer, so we can just use it directly for the moment. 
        Graphics.SpriteRenderer.Begin();
        base.Draw();
        
        Font.Draw(Graphics.SpriteRenderer, 20, "Hello", new Vector2(20), Color.White);
        
        Graphics.SpriteRenderer.End();
    }
}