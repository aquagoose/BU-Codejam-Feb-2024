using System.Numerics;
using u4.Engine.Entities;
using u4.Math;
using u4.Render;

namespace EcoBytes.Components;

public class TextElement : Component
{
    public Font Font;

    public string Text;

    public uint FontSize;

    public Color Color;

    public Vector2 TextOffset;

    public bool IsStatic;

    public TextElement(Font font, string text, uint fontSize, Color color, Vector2 textOffset, bool isStatic = false)
    {
        Font = font;
        Text = text;
        FontSize = fontSize;
        Color = color;
        TextOffset = textOffset;
        IsStatic = isStatic;
    }

    public override void Draw()
    {
        Vector3 position = Transform.Position;
        Font.Draw(FontSize, Text, new Vector2(position.X, position.Y) + TextOffset, Color, IsStatic);
    }
}