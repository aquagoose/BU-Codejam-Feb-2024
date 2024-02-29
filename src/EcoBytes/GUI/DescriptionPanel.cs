using System.Drawing;
using System.Numerics;
using u4.Math;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class DescriptionPanel : UIElement
{
    public Font Font;
    
    public string Title;
    public string Description;
    
    public DescriptionPanel(string name, Point position, Size<int> size, Font font, string title, string description) : base(name, position, size)
    {
        Font = font;
        Title = title;
        Description = description;
    }

    public override void Draw(SpriteRenderer renderer)
    {
        Vector2 position = new Vector2(Position.X, Position.Y);
        const int borderWidth = 3;
        
        renderer.DrawBorderRectangle(position, Size, Color.White, Color.Black, borderWidth);
        
        Font.Draw(renderer, 32, Font.FitToWidth(32, Title, Size.Width - 10), position + new Vector2(borderWidth + 5), Color.Black);
        Font.Draw(renderer, 20, Font.FitToWidth(20, Description, Size.Width - 10), position + new Vector2(borderWidth + 5, 100), Color.Black);
    }
}