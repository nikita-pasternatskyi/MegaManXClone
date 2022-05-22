using Godot;

namespace MP.FiniteStateMachine
{
    public class SpawnNode2D : InstantStateAction
    {
        [Export] private PackedScene _sceneToSpawn;

        public override void Act(float delta)
        {
            var scene = _sceneToSpawn.Instance();
            AddChild(scene);
        }
    }
}