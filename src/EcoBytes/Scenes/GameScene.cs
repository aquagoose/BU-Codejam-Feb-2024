using System.Numerics;
using EcoBytes.Components;
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
        // Create Main Camera
        Entity mainCamera = new Entity("Camera");
        mainCamera.AddComponent(new Camera());
        AddEntity(mainCamera);
        
        Entity test = new Entity("test", new Transform(new Vector3(100f, 100f, 0)));
        test.AddComponent(new Sprite(EcoBytesGame.DorsetHouse));
        test.AddComponent(new BuildingComponent("DH"));
        test.AddComponent(new TextElement(EcoBytesGame.Font, "Dorset House", 100, Color.White, new Vector2(-100, -100)));
        AddEntity(test);
        
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
    }
}