using System.Collections.Generic;
using System.Linq;

public interface IGoapPlanner
{
    ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal=null);
}

public class GoapPlanner : IGoapPlanner
{
    public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal)
    {
        List<AgentGoal> orderedGoals = goals
        .Where(goal => goal.DesiredEffects.Any(
            belief => !belief.EvaluateCondition())
        )
        .ToList();
    }
}

public class ActionPlan
{
    public AgentGoal Goal { get;}
    public Stack<AgentAction> Actions { get; }
    public float TotalCost { get; }

    public ActionPlan(AgentGoal goal, Stack<AgentAction> actions, float totalCost)
    {
        Goal = goal;
        Actions = actions;
        TotalCost = totalCost;
    }
}