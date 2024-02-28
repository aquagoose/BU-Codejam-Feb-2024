using System;
using System.Drawing;
using System.Numerics;
using u4.Math;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class Button : UIElement
{
    public Font Font;

    public uint TextSize;

    public string Text;

    public Action Click;

    public Color Color;

    public Color HoverColor;

    public Color ClickColor;

    public Color TextColor;
    
    public Button(string name, Point position, Size<int> size, Font font, uint textSize, string text, Action click) :
        base(name, position, size)
    {
        Font = font;
        TextSize = textSize;
        Text = text;
        Click = click;
        
        Color = Color.Red;
        HoverColor = Color.Green;
        ClickColor = Color.Orange;
        TextColor = Color.White;
    }

    public override void Update(ref bool mouseCaptured)
    {
        base.Update(ref mouseCaptured);
        
        if (IsClicked)
            Click.Invoke();
    }

    public override void Draw(SpriteRenderer renderer)
    {
        Color color = Color;

        if (IsHovered)
            color = HoverColor;
        if (IsMouseClicked)
            color = ClickColor;

        Vector2 position = new Vector2(Position.X, Position.Y);
        Size<int> textSize = Font.MeasureString(TextSize, Text);
        
        renderer.DrawRectangle(position, Size, color);
        
        Font.Draw(renderer, TextSize, Text, position + new Vector2(Size.Width / 2, Size.Height / 2) - new Vector2(textSize.Width / 2, textSize.Height / 2), TextColor);
        
        base.Draw(renderer);
    }
}