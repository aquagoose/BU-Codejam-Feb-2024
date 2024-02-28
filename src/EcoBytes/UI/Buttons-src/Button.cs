using System;
using u4.Engine.Entities;
using u4.Engine;
using u4.Math;
using System.Numerics;
using System.Drawing;
using u4.Engine.Entities;
using u4.Render;
using Color = u4.Math.Color;
using EcoBytes.Components;
using Pie.Windowing;

namespace EcoBytes.UI;

public class Button : Component
{
    public Action ButtonClick;
    public Rectangle rect;
    public Vector2 ButtonPos;

    public bool Visible;

    public Button(Action click)
    {
        ButtonClick = click;
    }

    public override void Initialize()
    {
        Vector3 position = Transform.Position;
        Size<int> size = Entity.GetComponent<Sprite>().Texture.Size;

        rect = new Rectangle((int) position.X, (int) position.Y, size.Width, size.Height);
    }

    public override void Update(float dt)
    {
        Vector2 MousePos = Input.MousePosition;
        Entity.GetComponent<Sprite>().Tint = Color.White;

        if (rect.Contains((int) MousePos.X, (int) MousePos.Y))
        {
            Entity.GetComponent<Sprite>().Tint = new Color(1.5f,1.5f,1.5f);
            if (Input.MouseButtonPressed(MouseButton.Left))
            {
                ButtonClick.Invoke();
                Entity.GetComponent<Sprite>().Tint = new Color(0.5f,0.5f,0.5f);
            }       
        }
    }

    public override void Draw() { }
}