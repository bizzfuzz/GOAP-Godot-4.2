using System;
using Godot;

public partial class Sensor : Node
{
    [Export] private Area3D collider;
    [Export] private Timer updateTimer;

    Node3D target;
    Vector3 lastKnownPosition;
    public event Action OnTargetUpdated = delegate{};

    public Vector3 TargetPosition => target?.GlobalTransform.Origin?? Vector3.Zero;
    public bool TargetInRange => TargetPosition != Vector3.Zero;
    private string PlayerTag = "player";

    public override void _Ready()
    {
        updateTimer.Start();
    }
    public void UpdateTargetPosition(Node3D newTarget = null)
    {
        target = newTarget;
        if(TargetInRange && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.Zero))
        {
            lastKnownPosition = TargetPosition;
            OnTargetUpdated.Invoke();
        }
    }
    private void OnTimerTimeout()
    {
        UpdateTargetPosition(target);
        updateTimer.Start();
    }
    public void OnBodyEnter(Node3D body)
    {
        if(body.IsInGroup(PlayerTag))
        {
            GD.Print("player entered");
        }
    }
    public void OnBodyExit(CharacterBody3D body)
    {
        if(body.IsInGroup(PlayerTag))
        {
            GD.Print("player exited");
        }
    }
}