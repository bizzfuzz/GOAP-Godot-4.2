using System.Collections.Generic;

public class AgentGoal
{
    public string Name { get;}
    public float Priority { get; set; }
    public HashSet<AgentBelief> DesiredEffects { get; } = new();

    AgentGoal(string name)
    {
        Name = name;
        Priority = 1.0f;
    }

    public class Builder
    {
        readonly AgentGoal goal;
        public Builder(string name)
        {
            goal = new AgentGoal(name);
        }
        public Builder WithPriority(float priority)
        {
            goal.Priority = priority;
            return this;
        }
        public Builder WithDesiredEffect(AgentBelief belief)
        {
            goal.DesiredEffects.Add(belief);
            return this;
        }
        public AgentGoal Build() => goal;
    }
}