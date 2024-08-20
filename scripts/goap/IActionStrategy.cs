using Godot;

public interface IActionStrategy
{
    bool CanExecute {get; }
    bool Completed { get; }

    void Start();
    void Stop();
    void Update(float delta);
}

public class IdleStartegy : IActionStrategy
{
    public bool CanExecute => true; //can always idle
    public bool Completed { get; }
    readonly Timer timer;

    public void Start()
    {
    }
    public void Stop()
    {
    }
    public void Update(float delta)
    {
    }
}