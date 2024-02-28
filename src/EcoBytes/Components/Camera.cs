using System;
using System.Numerics;
using u4.Engine.Entities;

namespace EcoBytes.Components;

public class Camera : Component
{

    public Matrix4x4 CamTranslation => Matrix4x4.CreateTranslation(Transform.Position);

    public void MoveCamera(int distance)
    {
        Transform.Position.X -= distance;
    }
    
}