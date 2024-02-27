using u4.Engine;
using u4.Math;
using u4.Render;

namespace EcoBytes;

public class EcoBytesGame : Game
{
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