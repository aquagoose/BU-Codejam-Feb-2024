using System;
using System.Drawing;
using System.Numerics;
using Pie.Windowing;
using u4.Engine;
using u4.Math;
using u4.Render.Renderers;

namespace EcoBytes.GUI;

public abstract class UIElement
{
    public readonly string Name;
    
    public Point Position;
    
    public Size<int> Size;

    protected bool IsHovered;
    protected bool IsMouseClicked;
    protected bool IsClicked;

    protected UIElement(string name, Point position, Size<int> size)
    {
        Name = name;
        Position = position;
        Size = size;
    }

    public virtual void Update(ref bool mouseCaptured)
    {
        Vector2 mPos = Input.MousePosition;

        IsHovered = false;
        IsClicked = false;
        
        if (mPos.X >= Position.X && mPos.X < Position.X + Size.Width &&
            mPos.Y >= Position.Y && mPos.Y < Position.Y + Size.Height &&
            !mouseCaptured)
        {
            mouseCaptured = true;

            IsHovered = true;

            if (Input.MouseButtonDown(MouseButton.Left))
                IsMouseClicked = true;
            else if (IsMouseClicked)
            {
                IsMouseClicked = false;
                IsClicked = true;
            }
        }
        else
        {
            IsMouseClicked = false;
            IsClicked = false;
        }
    }

    public abstract void Draw(SpriteRenderer renderer);

    public delegate void OnClick(UIElement element);
}