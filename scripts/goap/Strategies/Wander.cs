using Godot;

namespace GoapStrategies
{
	public partial class Wander : Node3D, IActionStrategy
	{
		[Export] private Timer Timer;
		[Export] private float Duration;
		[Export] private float WanderRadius;
		public NavigationAgent3D NavAgent { get; set; }

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
			GD.Print("wander completed");
			Completed = true;
		}
	}
}