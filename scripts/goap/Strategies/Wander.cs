using Godot;

namespace GoapStrategies
{
    public partial class Wander : Node3D, IActionStrategy
	{
		[Export] private Timer Timer;
		[Export] private float Duration;
		[Export] private float WanderRadius;
		[Export] private GoapAgent Goap;
		public NavigationAgent3D NavAgent { get; set; }
		private readonly static float NavAttempts = 5;
		public CharacterBody3D OwnBody;

		public bool CanExecute => !Completed;

		public bool Completed => Goap.AtDestination() || !Goap.FollowingPath();

		public void Start()
		{
			for (int i = 0; i < NavAttempts; i++)
			{
				var newDestination = Utils.NavUnitSphere(OwnBody, WanderRadius);
				NavAgent.TargetPosition =newDestination;
                if(NavAgent.IsTargetReachable())
				{
					break;
				}
			}
			Timer.Start(Duration);
		}
		public void Stop(){}
		public void Update(float delta){}
		public void TimerCompleted()
		{
			GD.Print("wander completed");
		}
	}
}