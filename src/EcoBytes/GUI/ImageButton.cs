using System;
using System.Drawing;
using System.Numerics;
using u4.Math;
using u4.Render;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class ImageButton : UIElement
{
    public Texture Texture;
    
    public Action Click;

    public ImageButton(string name, Point position, Size<int> size, Texture texture, Action click) : base(name, position, size)
    {
        Texture = texture;
        Click = click;
    }

    public override void Update(ref bool mouseCaptured)
    {
        base.Update(ref mouseCaptured);
        
        if (IsClicked)
            Click.Invoke();
    }

    public override void Draw(SpriteRenderer renderer)
    {
        Color tint = Color.White;

        if (IsHovered)
            tint = new Color(1.5f, 1.5f, 1.5f);
        if (IsMouseClicked)
            tint = new Color(0.5f, 0.5f, 0.5f);

        Size<int> texSize = Texture.Size;
        Vector2 scale = new Vector2(Size.Width / (float) texSize.Width, Size.Height / (float) texSize.Height);
        
        renderer.Draw(Texture, new Vector2(Position.X, Position.Y), tint, 0, scale, Vector2.Zero);
        
        base.Draw(renderer);
    }
}