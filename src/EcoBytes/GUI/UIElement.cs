using System.Drawing;
using System.Numerics;
using u4.Engine;
using u4.Math;
using u4.Render.Renderers;

namespace EcoBytes.GUI;

public abstract class UIElement
{
    public Point Position;
    
    public Size<int> Size;

    protected bool IsHovered;
    protected bool IsClicked;

    protected UIElement(Point position, Size<int> size)
    {
        Position = position;
        Size = size;
    }

    public virtual void Update(ref bool mouseCaptured)
    {
        Vector2 mPos = Input.MousePosition;

        IsHovered = true;
        
        if (mPos.X >= Position.X && mPos.X < Position.X + Size.Width &&
            mPos.Y >= Position.Y && mPos.Y < Position.Y + Size.Height &&
            !mouseCaptured)
        {
            mouseCaptured = true;

            IsHovered = true;
        }
    }

    public abstract void Draw(SpriteRenderer renderer);
}