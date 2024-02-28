using System.Numerics;
using EcoBytes.Components;
using EcoBytes.UI;
using Pie.Windowing;
using u4.Engine;
using u4.Engine.Entities;
using u4.Engine.Scenes;
using u4.Math;
using u4.Render;

namespace EcoBytes.Scenes;

public class GameScene : Scene
{
    /// <summary>
    /// The amount of time, in seconds, that a week takes to advance.
    /// </summary>
    public const float WeekAdvanceTime = 2.0f;

    private float _weekAdvanceCounter;
    
    public static uint CurrentWeek;
    
    public override void Initialize()
    {
        Entity test = new Entity("test", new Transform(new Vector3(100f, 100f, 0)));
        test.AddComponent(new Sprite(EcoBytesGame.DorsetHouse));
        test.AddComponent(new BuildingComponent("DH"));
        test.AddComponent(new TextElement(EcoBytesGame.Font, "Dorset House", 100, Color.White, new Vector2(-100, -100)));
        AddEntity(test);

        // Create Main Camera
        Entity mainCamera = new Entity("Camera");
        Camera camera = new Camera();
        mainCamera.AddComponent(camera);

        // Navigation Buttons
        Entity navRight = new Entity("Right Button", new Transform(new Vector3(1193f, 360f, 0f)));
        navRight.AddComponent(new Sprite(new Texture("Content/Textures/RightArrow.png"), true) { Scale = new Vector2(0.5f) });
        navRight.AddComponent(new Button(() => camera.MoveCamera(500)));

        Entity navLeft = new Entity("Left Button", new Transform(new Vector3(87f, 360, 0f)));
        navLeft.AddComponent(new Sprite(new Texture("Content/Textures/RightArrow.png"), true) { Scale = new Vector2(-0.5f, 0.5f) });
        navLeft.AddComponent(new Button(() => camera.MoveCamera(-500)));
        
        AddEntity(navRight);
        AddEntity(navLeft);
        AddEntity(mainCamera);
        
        base.Initialize();

        CurrentWeek = 1;
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        _weekAdvanceCounter += dt;
        if (_weekAdvanceCounter >= WeekAdvanceTime)
        {
            _weekAdvanceCounter -= WeekAdvanceTime;
            CurrentWeek++;
        }
        
        if (Input.KeyPressed(Key.P))
            GetEntity("test").GetComponent<BuildingComponent>().PurchaseUpgrade("LEDFixture");
    }

    public override void Draw()
    {
        base.Draw();

        EcoBytesGame.Font.Draw(20, $"Week {CurrentWeek}", new Vector2(10, 680), Color.White, true);
    }
}