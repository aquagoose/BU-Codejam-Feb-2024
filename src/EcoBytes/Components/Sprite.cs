using System.Numerics;
using u4.Engine.Entities;
using u4.Render;

namespace EcoBytes.Components;

public class Sprite : Component
{
    public Texture Texture;

    public Sprite(Texture texture)
    {
        Texture = texture;
    }

    public override void Draw()
    {
        Vector3 position = Transform.Position;
        Graphics.SpriteRenderer.Draw(Texture, new Vector2(position.X, position.Y));
    }
}