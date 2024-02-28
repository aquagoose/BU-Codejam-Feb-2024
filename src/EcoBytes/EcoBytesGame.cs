using System.IO;
using EcoBytes.Data;
using u4.Core;
using u4.Engine;
using u4.Math;
using u4.Render;

namespace EcoBytes;

public class EcoBytesGame : Game
{
    public override void Initialize()
    {
        Logger.Debug("Loading upgrades.");
        Upgrade.LoadUpgradesFromJson(File.ReadAllText("Content/Data/Upgrades.json"));
        
        Logger.Debug("Loading buildings.");
        Building.LoadBuildingsFromJson(File.ReadAllText("Content/Data/Buildings.json"));
        
        Logger.Debug("Loading carbon factors.");
        CarbonFactors.LoadFactorsFromJson(File.ReadAllText("Content/Data/CarbonFactors.json"));
        
        base.Initialize();
    }

    public override void Draw()
    {
        Graphics.Device.ClearColorBuffer(Color.Black);
        
        // u4's main renderer currently isn't working well enough for it to draw sprites yet.
        // Fortunately, for this project, we only need the sprite renderer, so we can just use it directly for the moment. 
        Graphics.SpriteRenderer.Begin();
        base.Draw();
        Graphics.SpriteRenderer.End();
    }
}