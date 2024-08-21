using System.Threading.Tasks;
using Godot;

public interface IActionStrategy
{
    bool CanExecute {get; }
    bool Completed { get; }

    void Start();
    void Stop();
    void Update(float delta);
}