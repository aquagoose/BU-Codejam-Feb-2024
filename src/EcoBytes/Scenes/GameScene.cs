using System.Numerics;
using EcoBytes.Components;
using EcoBytes.Data;
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
        Entity test = new Entity("test");
        test.AddComponent(new Sprite(EcoBytesGame.DorsetHouse));
        test.AddComponent(new BuildingComponent("DH"));
        test.AddComponent(new TextElement(EcoBytesGame.Font, "Hello", 100, Color.White, Vector2.Zero));
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
        
        if (Input.KeyPressed(Key.P))
            GetEntity("test").GetComponent<BuildingComponent>().PurchaseUpgrade("LEDFixture");
    }

    public override void Draw()
    {
        base.Draw();

        EcoBytesGame.Font.Draw(20, $"Week {CurrentWeek}", new Vector2(10, 680), Color.White, true);
    }
}