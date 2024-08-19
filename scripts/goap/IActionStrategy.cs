public interface IActionStrategy
{
    bool CanExecute {get; }
    bool Completed { get; }

    void Start();
    void Stop();
    void Update(float delta);
}