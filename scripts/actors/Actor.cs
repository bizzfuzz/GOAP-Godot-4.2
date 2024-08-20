using Godot;
using System;

public partial class Actor : CharacterBody3D
{
	[Export] private AnimationPlayer animationPlayer;
	[Export] private GoapAgent goapAgent;
	[Export] private Timer StatsTimer;
	[Export] private Node3D RestArea;
	[Export] private Node3D FoodArea;
	[Export] private int health = 100;
	[Export] private int stamina = 100;

	static readonly string idleAnimation = "animations/IdleAnimation";
	private Node3D target;
	Vector3 destination;
	private int maxStat = 100;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayIdleAnimation();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void PlayIdleAnimation()
	{
		animationPlayer.Play(idleAnimation);
	}
	public void StatsTimerCompleted()
	{
		UpdateStats();
		StatsTimer.Start();
	}
	private void UpdateStats()
	{
		health -= InRangeOf(FoodArea.GlobalTransform.Origin, 5f)? 20 : -10;
		stamina += InRangeOf(RestArea.GlobalTransform.Origin, 5f)? 20 : -10;
		health = Mathf.Clamp(health, 0, maxStat);
		stamina = Mathf.Clamp(stamina, 0, maxStat);
	}
	public bool InRangeOf(Vector3 position, float range)
    {
        return GlobalTransform.Origin.DistanceTo(position) <= range;
    }
}
