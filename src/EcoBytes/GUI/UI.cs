using System.Collections.Generic;
using System.Linq;
using u4.Render;

namespace EcoBytes.GUI;

public static class UI
{
    private static Dictionary<string, UIElement> _elements;

    // Set to true if the mouse is hovering over a UI element.
    // Prevents elements stacked on top of each other from clicking through.
    public static bool MouseCaptured;

    static UI()
    {
        _elements = new Dictionary<string, UIElement>();
    }

    public static void AddElement(UIElement element)
    {
        _elements.Add(element.Name, element);
    }

    public static void RemoveElement(string name)
    {
        _elements.Remove(name);
    }

    public static T GetElement<T>(string name) where T : UIElement
    {
        return (T) _elements[name];
    }

    public static void ClearElements()
    {
        _elements.Clear();
    }

    public static void Update()
    {
        MouseCaptured = false;
        
        foreach ((_, UIElement element) in _elements.Reverse())
            element.Update(ref MouseCaptured);
    }

    public static void Draw()
    {
        Graphics.SpriteRenderer.Begin();
        foreach ((_, UIElement element) in _elements)
            element.Draw(Graphics.SpriteRenderer);
        Graphics.SpriteRenderer.End();
    }
}