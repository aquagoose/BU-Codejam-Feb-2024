using System.Numerics;
using u4.Engine.Entities;
using u4.Math;
using u4.Render;

namespace EcoBytes.Components;

public class Sprite : Component
{
    public Texture Texture;

    public Color Tint;

    public Sprite(Texture texture)
    {
        Texture = texture;
        Tint = Color.White;
    }

    public override void Draw()
    {
        Vector3 position = Transform.Position;
        Graphics.SpriteRenderer.Draw(Texture, new Vector2(position.X, position.Y), Tint, 0, Vector2.One, Vector2.Zero);
    }
}