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

    public int Cost;
    public int Time;
    
    public DescriptionPanel(string name, Point position, Size<int> size, Font font, string title, string description, int cost, int time) : base(name, position, size)
    {
        Font = font;
        Title = title;
        Description = description;
        Cost = cost;
        Time = time;
    }

    public override void Draw(SpriteRenderer renderer)
    {
        Vector2 position = new Vector2(Position.X, Position.Y);
        const int borderWidth = 3;
        
        renderer.DrawBorderRectangle(position, Size, Color.White, Color.Black, borderWidth);

        string title = Font.FitToWidth(32, Title, Size.Width - 10);
        Size<int> titleSize = Font.MeasureString(32, title);
        
        Font.Draw(renderer, 32, title, position + new Vector2(borderWidth + 5), Color.Black);

        if (Cost >= 0)
        {
            Font.Draw(renderer, 20, Cost > 0 ? Cost.ToString("C0") : "FREE",
                position + new Vector2(borderWidth + 5, titleSize.Height + 25), Color.Black);
            
            Font.Draw(renderer, 20, Time > 0 ? $"{Time} week{(Time != 1 ? "s" : "")}" : "Instant", position + new Vector2(100, titleSize.Height + 25), Color.Black);
        }

        Font.Draw(renderer, 20, Font.FitToWidth(20, Description, Size.Width - 10), position + new Vector2(borderWidth + 5, titleSize.Height + 50), Color.Black);
    }
}