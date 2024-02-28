using System.Drawing;
using System.Numerics;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class Image : UIElement
{
    public Texture Texture;

    public Image(string name, Point position, Size<int> size, Texture texture) : base(name, position, size)
    {
        Texture = texture;
    }

    public override void Draw(SpriteRenderer renderer)
    {
        Size<int> texSize = Texture.Size;
        Vector2 scale = new Vector2(Size.Width / (float) texSize.Width, Size.Height / (float) texSize.Height);
        renderer.Draw(Texture, new Vector2(Position.X, Position.Y), Color.White, 0, scale, Vector2.Zero);
        
        base.Draw(renderer);
    }
}