using System;
using Godot;
public static class Utils
{
    public static Vector3 RandomPointInUnitSphere(float radius)
    {
        return new Vector3(GD.Randf(), GD.Randf(), GD.Randf()) * radius;
    }
    public static Vector3 NavUnitSphere(Node3D position, float radius)
    {
        return NavUnitSphere(position.GlobalTransform.Origin, radius);
    }
    public static Vector3 NavUnitSphere(Vector3 position, float radius)
    {
        var point = OnUnitSphere(radius);
        point.Y = 0;
        return position + point;
    }
    public static Vector3 OnUnitSphere(float radius)
    {
        float theta = (float)(GD.Randf() * 2.0 * Math.PI);
        float phi = (float)(Math.Acos(2.0 * GD.Randf() - 1.0));

        float x = (float)(Math.Sin(phi) * Math.Cos(theta));
        float y = (float)(Math.Sin(phi) * Math.Sin(theta));
        float z = (float)(Math.Cos(phi));

        return new Vector3(x, y, z) * radius;
    }
}