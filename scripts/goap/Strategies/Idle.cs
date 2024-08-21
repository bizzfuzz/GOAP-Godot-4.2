using Godot;
using System;

namespace GoapStrategies
{
	public partial class Idle : Node3D, IActionStrategy
	{
		[Export] private Timer Timer;
		[Export] private float Duration;

		public bool CanExecute => true;

		public bool Completed {get; private set; } = false;

		public void Start()
		{
			Timer.Start(Duration);
		}
		public void Stop(){}
		public void Update(float delta){}
		public void TimerCompleted()
		{
			GD.Print("idle completed");
			Completed = true;
		}
    }
}