using System;
using System.Numerics;
using u4.Engine.Entities;

namespace EcoBytes.Components;

public class Camera : Component
{
    private const float MoveSpeed = 0.1f;
    private float _moveCounter;
    private float _targetX;
    private float _startX;

    public Matrix4x4 CamTranslation => Matrix4x4.CreateTranslation(-Transform.Position);
    
    public Camera()
    {
        _moveCounter = MoveSpeed;
    }

    public void MoveCamera(int distance)
    {
        if (_moveCounter >= MoveSpeed)
            _moveCounter = 0;
        _startX = Transform.Position.X;
        _targetX = _startX + distance;
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        if (_moveCounter >= MoveSpeed)
            return;

        float lerpValue = _moveCounter / MoveSpeed;
        float easeValue = lerpValue < 0.5
            ? 4 * lerpValue * lerpValue * lerpValue
            : 1 - float.Pow(-2 * lerpValue + 2, 3) / 2f;
        
        Transform.Position.X = float.Lerp(_startX, _targetX, easeValue);

        // Clamp camera position on screen.
        Transform.Position.X = float.Clamp(Transform.Position.X, -900, 880);

        _moveCounter += dt;
    }
}