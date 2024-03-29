﻿using System;
using System.Drawing;
using System.Numerics;
using u4.Math;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class Button : UIElement
{
    private bool _hasSentHoverEvent;
    
    public Font Font;

    public uint TextSize;

    public string Text;

    public Action Click;

    public Action Hover;

    public Action UnHover;

    public Color Color;

    public Color HoverColor;

    public Color ClickColor;

    public Color TextColor;

    public float Progress;
    
    public Button(string name, Point position, Size<int> size, Font font, uint textSize, string text, Action click) :
        base(name, position, size)
    {
        Font = font;
        TextSize = textSize;
        Text = text;
        Click = click;
        Hover = null;
        
        Color = Color.LightSeaGreen;
        HoverColor = Color.MediumSeaGreen;
        ClickColor = Color.MediumSpringGreen;
        TextColor = Color.White;

        Progress = 0;
    }

    public override void Update(ref bool mouseCaptured)
    {
        base.Update(ref mouseCaptured);

        if (IsHovered)
        {
            if (!_hasSentHoverEvent)
            {
                _hasSentHoverEvent = true;
                Hover?.Invoke();
            }
        }
        else if (_hasSentHoverEvent)
        {
            _hasSentHoverEvent = false;
            UnHover.Invoke();
        }

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
        
        renderer.DrawRectangle(position, new Size<int>((int) (Size.Width * Progress), Size.Height), new Color(1.0f, 1.0f, 1.0f, 0.5f));
    }
}