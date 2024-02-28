using System.Drawing;
using System.Numerics;
using u4.Math;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class TextElement : UIElement
{
    public Font Font;

    public uint FontSize;

    public string Text;

    public Color Color;

    public TextElement(string name, Point position, Font font, uint fontSize, string text) : base(name, position, new Size<int>(0))
    {
        Font = font;
        FontSize = fontSize;
        Text = text;
        Color = Color.White;
    }
    
    public override void Draw(SpriteRenderer renderer)
    {
        Font.Draw(renderer, FontSize, Text, new Vector2(Position.X, Position.Y), Color);
    }
}