using System.Numerics;
using EcoBytes.Components;
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
    public const float WeekAdvanceTime = 1.0f;

    private float _weekAdvanceCounter;
    
    public static uint CurrentWeek;
    
    public override void Initialize()
    {
        base.Initialize();

        CurrentWeek = 0;
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

    public override void Draw()
    {
        base.Draw();

        EcoBytesGame.Font.Draw(Graphics.SpriteRenderer, 20, $"Week {CurrentWeek}", new Vector2(10, 680), Color.White);
    }
}