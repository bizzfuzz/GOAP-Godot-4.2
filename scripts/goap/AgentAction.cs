using System.Collections.Generic;

public class AgentAction
{
    public string Name { get; set; }
    public float Cost { get; set; }

    public HashSet<AgentBelief> Preconditions { get; } = new();
    public HashSet<AgentBelief> Effects { get; } = new();

    IActionStrategy Strategy;
    public bool Completed => Strategy.Completed;
    
    public AgentAction(string name)
    {
        Name = name;
    }
    public void Start() => Strategy.Start();
    public void Stop() => Strategy.Stop();
    public void Update(float delta)
    {
        if (Strategy.CanExecute)
        {
            Strategy.Update(delta);
        }
        if (Strategy.Completed)
        {
            foreach (var effect in Effects)
            {
                effect.EvaluateCondition();
            }
        }
    }

    public class Builder
    {
        readonly AgentAction action;

        public Builder(string name)
        {
            action = new AgentAction(name)
            {
                Cost = 1,
            };
        }
        public Builder WithCost(float cost)
        {
            action.Cost = cost;
            return this;
        }
        public Builder WithStrategy(IActionStrategy strategy)
        {
            action.Strategy = strategy;
            return this;
        }

        public Builder AddPrecondition(AgentBelief belief)
        {
            action.Preconditions.Add(belief);
            return this;
        }
        public Builder AddEffect(AgentBelief belief)
        {
            action.Effects.Add(belief);
            return this;
        }
        public AgentAction Build() => action;
    }
}