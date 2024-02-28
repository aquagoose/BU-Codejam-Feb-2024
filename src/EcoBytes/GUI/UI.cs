using System.Collections.Generic;

namespace EcoBytes.GUI;

public static class UI
{
    private static List<UIElement> _elements;

    static UI()
    {
        _elements = new List<UIElement>();
    }

    public static void AddElement(UIElement element)
    {
        _elements.Add(element);
    }
}