using System;
using System.Drawing;
using EcoBytes.GUI;
using u4.Engine.Scenes;
using u4.Math;
using u4.Render;

namespace EcoBytes.Scenes;

public class GameOverScene : Scene
{
    private Texture _buttonTexture;
    

    enum Type               // can get rid if you need to idc its just for init
    GameOverScene(enum type)
    {
        Type = type         // again just using this for if statement in init for testing
    }

    public override void Initialize()
    {
        
        
        if(type == 1)
        {
            UI.AddElement(new Image("test", new Point(0, 0), new Size<int>(1280, 720), new Texture("piggy_bank.png")));
        }
        else
        {
            UI.AddElement(new Image("test", new Point(0, 0), new Size<int>(1280, 720), new Texture("sea_level.png")));
        }
    }
}