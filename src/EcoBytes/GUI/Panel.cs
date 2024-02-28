using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using u4.Math;
using u4.Render.Renderers;
using Color = u4.Math.Color;

namespace EcoBytes.GUI;

public class Panel : UIElement
{
    private Dictionary<string, UIElement> _subElements;
    
    public Color Color;

    public Panel(string name, Point position, Size<int> size, Color color) : base(name, position, size)
    {
        Color = color;

        _subElements = new Dictionary<string, UIElement>();
    }

    public void AddElement(UIElement element)
    {
        _subElements.Add(element.Name, element);
    }

    public T GetElement<T>(string name) where T : UIElement
    {
        return (T) _subElements[name];
    }

    public override void Update(ref bool mouseCaptured)
    {
        foreach ((_, UIElement element) in _subElements.Reverse())
            element.Update(ref mouseCaptured);
        
        base.Update(ref mouseCaptured);
    }

    public override void Draw(SpriteRenderer renderer)
    {
        renderer.DrawRectangle(new Vector2(Position.X, Position.Y), Size, Color);
        
        foreach ((_, UIElement element) in _subElements)
            element.Draw(renderer);
        
        base.Draw(renderer);
    }
}