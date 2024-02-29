using System.Drawing;
using System.Numerics;
using u4.Math;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class ProgressBar : UIElement
{
    public float Progress;

    public Color Color;

    public ProgressBar(string name, Point position, Size<int> size, Color color) : base(name, position, size)
    {
        Progress = 0;
        Color = color;
    }

    public override void Draw(SpriteRenderer renderer)
    {
        renderer.DrawRectangle(new Vector2(Position.X, Position.Y),
            new Size<int>((int) (Size.Width * Progress), Size.Height), Color);
    }
}