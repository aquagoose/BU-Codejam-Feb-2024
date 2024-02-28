using System;
using System.Drawing;
using EcoBytes.GUI;
using u4.Engine.Scenes;
using u4.Math;
using u4.Render;

namespace EcoBytes.Scenes;

public class TestScene : Scene
{
    private Texture _buttonTexture;
    
    public override void Initialize()
    {
        base.Initialize();

        _buttonTexture = new Texture("Content/Textures/RightArrow.png");

        ImageButton button1 = new ImageButton("button1", new Point(100, 100), new Size<int>(100, 100), _buttonTexture, () =>
        {
            Console.WriteLine("Button 1 clicked!");
        });
        
        ImageButton button2 = new ImageButton("button2", new Point(150, 150), new Size<int>(100, 100), _buttonTexture, () =>
        {
            Console.WriteLine("Button 2 clicked!");
        });
        
        UI.AddElement(button1);
        UI.AddElement(button2);
    }
}