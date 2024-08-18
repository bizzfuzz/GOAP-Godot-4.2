using System;
using Godot;

public partial class Sensor : Node
{
    [Export] private Area3D collider;
    [Export] private Timer updateTimer;

    Node3D target;
    Vector3 lastKnownPosition;
    float test;
    public event Action OnTargetUpdated = delegate{};

    public Vector3 TargetPosition => target?.GlobalTransform.Origin?? Vector3.Zero;
    public bool TargetInRange => TargetPosition != Vector3.Zero;

    public void UpdateTargetPosition(Node3D newTarget)
    {
        target = newTarget;
        if(TargetInRange && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.Zero))
        {
            lastKnownPosition = TargetPosition;
            OnTargetUpdated.Invoke();
        }
    }

     public override void _Ready()
    {
        updateTimer.Start();
    }
    private void OnTimerTimeout()
    {
        updateTimer.Start();
    }
}