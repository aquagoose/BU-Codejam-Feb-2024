using System.Numerics;
using u4.Math;
using u4.Render.Renderers;

namespace EcoBytes.GUI;

public static class SpriteRendererExtensions
{
    public static void DrawRectangle(this SpriteRenderer renderer, Vector2 position, Size<int> size, Color color)
    {
        renderer.Draw(EcoBytesGame.WhiteTexture, position, color, 0, new Vector2(size.Width, size.Height), Vector2.Zero);
    }

    public static void DrawBorder(this SpriteRenderer renderer, Vector2 position, Size<int> size, Color color,
        int borderWidth)
    {
        renderer.DrawRectangle(position, new Size<int>(size.Width, borderWidth), color);
        renderer.DrawRectangle(position, new Size<int>(borderWidth, size.Height), color);
        renderer.DrawRectangle(position + new Vector2(size.Width - borderWidth, 0), new Size<int>(borderWidth, size.Height), color);
        renderer.DrawRectangle(position + new Vector2(0, size.Height), new Size<int>(size.Width, borderWidth), color);
    }

    public static void DrawBorderRectangle(this SpriteRenderer renderer, Vector2 position, Size<int> size, Color color,
        Color borderColor, int borderWidth)
    {
        renderer.DrawRectangle(position, size, color);
        renderer.DrawBorder(position, size, borderColor, borderWidth);
    }
}