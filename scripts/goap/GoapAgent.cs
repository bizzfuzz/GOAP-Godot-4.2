using System.Collections.Generic;
using System.Reflection.Metadata;
using Godot;

public partial class GoapAgent : Node3D
{
	[Export] private CharacterBody3D OwnBody;
	[Export] private NavigationAgent3D NavAgent;
	[Export] private Sensor ChaseSensor;
	[Export] private Sensor AttackSensor;
	[Export] private GoapStrategies.Idle idleStrategy;
	[Export] private GoapStrategies.Wander wanderStrategy;
	[Export] private float InteractRange = 2.0f;

	private AgentGoal LastGoal;
	private AgentGoal CurrentGoal;
	private AgentAction CurrentAction;

	private Dictionary<string, AgentBelief> Beliefs;
	private HashSet<AgentAction> Actions;
	private HashSet<AgentGoal> Goals;

	public override void _Ready()
	{
		SetUpBeliefs();
		SetUpActions();
		SetUpGoals();

		ChaseSensor.OnTargetUpdated += HandleTargetUpdated;
		AttackSensor.OnTargetUpdated += HandleTargetUpdated;
		wanderStrategy.NavAgent = NavAgent;
		wanderStrategy.OwnBody = OwnBody;
	}
	private void SetUpBeliefs()
    {
		Beliefs = new Dictionary<string, AgentBelief>();
		BeliefFactory factory = new(this, Beliefs);

		factory.AddBelief("nothing", ()=>false);
		factory.AddBelief("agentIdle", ()=>NavAgent.IsNavigationFinished());
		factory.AddBelief("agentMoving", ()=>!NavAgent.IsNavigationFinished());
	}
	private void SetUpActions()
    {
		Actions = new HashSet<AgentAction>();
		/*Actions.Add(new AgentAction.Builder("relax")
            .WithStrategy(new IdleStartegy())
			.AddEffect(Beliefs["nothing"])
			.Build());
		Actions.Add(new AgentAction.Builder("wander")
			.WithStrategy(new WanderStartegy())
			.AddEffect(Beliefs["agentMoving"])
            .Build());*/
	}
	private void SetUpGoals()
    {
		Goals = new HashSet<AgentGoal>();
		Goals.Add(new AgentGoal.Builder("relax")
			.WithPriority(1)
			.WithDesiredEffect(Beliefs["nothing"])
            .Build());
		Goals.Add(new AgentGoal.Builder("wander")
		    .WithPriority(1)
            .WithDesiredEffect(Beliefs["agentMoving"])
            .Build());
	}
	private void HandleTargetUpdated()
    {
		GD.Print("Target updated");
		CurrentAction = null;
		CurrentGoal = null;
	}
	public bool InRangeOf(Vector3 position)
    {
        return OwnBody.GlobalTransform.Origin.DistanceTo(position) <= InteractRange;
    }
	public bool AtDestination()
	{
		return NavAgent.DistanceToTarget() <= InteractRange;
	}
	public bool FollowingPath()
	{
		return !NavAgent.IsNavigationFinished();
	}
}
