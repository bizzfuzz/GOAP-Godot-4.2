using Godot;
using System;
using System.Collections.Generic;

public class BeliefFactory
{
    readonly GOAPAgent agent;
    readonly Dictionary<string, AgentBelief> beliefs;

    public BeliefFactory(GOAPAgent agent, Dictionary<string, AgentBelief> beliefs)
    {
        this.agent = agent;
        this.beliefs = beliefs;
    }
    public void AddBelief(string name, Func<bool> condition)
    {
        beliefs.Add(name, new AgentBelief.Builder(name)
        .WithCondition(condition)
        .Build());
    }
    public void AddGoalBelief(string name, float range, Transform3D locationCondition)
    {
        AddLocationBelief(name, range, locationCondition.Origin);
    }
    public void AddLocationBelief(string name, float range, Vector3 locationCondition)
    {
        beliefs.Add(name, new AgentBelief.Builder(name)
        .WithCondition(() => InRangeOf(locationCondition, range))
        .WithObservedLocation(()=>locationCondition)
        .Build());
    }

    bool InRangeOf(Vector3 pos, float range)
    {
        return agent.GlobalTransform.Origin.DistanceTo(pos) <= range;
    }
}

public class AgentBelief
{
    [Export]public string Name { get; set; }
    [Export]Func<bool> condition = () => false;
    [Export]Func<Vector3> observedLocation = () => new Vector3(0,0,0);

    public Vector3 Location => observedLocation();

    AgentBelief(string name)
    {
        this.Name = name;
    }
    public bool EvaluateCondition() => condition();

    public class Builder
    {
        readonly AgentBelief belief;
        public Builder(string name)
        {
            belief = new AgentBelief(name);
        }
        public Builder WithCondition(Func<bool> condition)
        {
            belief.condition = condition;
            return this;
        }
        public Builder WithObservedLocation(Func<Vector3> observedLocation)
        {
            belief.observedLocation = observedLocation;
            return this;
        }
        public AgentBelief Build() => belief;
    }
}