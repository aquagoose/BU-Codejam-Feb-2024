using System.Numerics;
using EcoBytes.GUI;
using EcoBytes.Scenes;
using Pie.Windowing;
using u4.Engine;
using u4.Engine.Entities;
using u4.Engine.Scenes;
using u4.Math;

namespace EcoBytes.Components;

public class ClickableBuilding : Component
{
    public override void Update(float dt)
    {
        base.Update(dt);

        Sprite sprite = Entity.GetComponent<Sprite>();
        
        Vector2 position = new Vector2(Transform.Position.X, Transform.Position.Y);
        Size<int> size = new Size<float>(sprite.Texture.Size.Width * sprite.Scale.X,
            sprite.Texture.Size.Height * sprite.Scale.Y).As<int>();

        Vector3 camPos = SceneManager.CurrentScene.GetEntity("Camera").Transform.Position;
        
        Vector2 mPos = Input.MousePosition + new Vector2(camPos.X, camPos.Y);

        sprite.Tint = Color.White;
        
        if (mPos.X >= position.X && mPos.X < position.X + size.Width &&
            mPos.Y >= position.Y && mPos.Y < position.Y + size.Height &&
            !UI.MouseCaptured)
        {
            sprite.Tint = new Color(1.5f, 1.5f, 1.5f);
            
            if (Input.MouseButtonPressed(MouseButton.Left))
                ((GameScene) SceneManager.CurrentScene).OpenUpgradePanel(Entity.GetComponent<BuildingComponent>());
        }
    }
}