using Godot;
using System.Collections.Generic;
using MP.Extensions;

namespace MP.FiniteStateMachine
{
    public sealed class StateMachine : Node, IStateMachine
    {
        [Export] private NodePath _rootNodePath;
        [Export] private NodePath _sharedStatePath;
        [Export] private NodePath _startStatePath;
        [Signal] private delegate Node StateChanged();

        private Node _rootNode;
        private State _sharedState;
        private IReadOnlyList<Transition> _currentSharedTransitions = new List<Transition>();

        private State _startState;
        private State _currentState;
        private IReadOnlyList<Transition> _currentStateTransitions = new List<Transition>();

        private Dictionary<System.Type, Node> _nodes = new Dictionary<System.Type, Node>();

        private void CreateSharedState()
        {
            _sharedState = new State();
            _sharedState._Ready();
            AddChild(_sharedState, true);
        }

        public override void _Ready()
        {
            this.TryGetNodeFromPath(_rootNodePath, out _rootNode);

            if (_sharedStatePath == null)
                CreateSharedState();
            else
                this.TryGetNodeFromPath(_sharedStatePath, out _sharedState);
            _sharedState.Init(this);
            _currentSharedTransitions = _sharedState.Transitions;

            var def = GetNode(_startStatePath);

            if ((def is State) == false)
                throw new System.InvalidCastException(nameof(_startStatePath));

            _startState = def as State;

            foreach (var child in this.GetChildren<State>(false))
            {
                if (child == _sharedState)
                    continue;
                child.Init(this);
            }

            foreach (var child in this.GetChildren<StateMachine>(true))
            {
                child.Disable();
            }

            if((GetParent() is StateMachine) == true)
            {
                this.Disable();
                return;
            }

            ChangeState(_startState);
        }
        
        public override void _Process(float delta)
        {
            _sharedState.Process(delta);
            _currentState.Process(delta);

            var sharedTransitionState = CheckTransitions(_currentSharedTransitions);
            if (sharedTransitionState.change == true)
                ChangeState(sharedTransitionState.newState);

            var currentTransitionState = CheckTransitions(_currentStateTransitions);
            if (currentTransitionState.change == true)
                ChangeState(currentTransitionState.newState);
        }
        
        public override void _PhysicsProcess(float delta)
        {
            _sharedState.PhysicsProcess(delta);
            _currentState.PhysicsProcess(delta);
        }
        
        public T GetCachedNode<T>() where T:Node
        {
            if(_nodes.TryGetValue(typeof(T), out Node value))
            {
                return (T)value;
            }

            if(_rootNode.TryGetNodeOfType<T> (out T res, includeMe: true) == false)
            {
                GD.PrintErr($"No node of type {typeof(T)} was found!");
                return null;
            }
            _nodes.Add(typeof(T), res);
            return res;
        }

        
        private void ExitMachine()
        {
            _sharedState.Exit();
            _currentState?.Exit();
            this.Disable();
        }

        private void EnterMachine(State newState = null)
        {
            _sharedState.Enter();
            this.Enable();
            if (newState == null)
                newState = _startState;
            ChangeState(newState);
        }
        
        private void ChangeState(State newState)
        {
            EmitSignal(nameof(StateChanged), newState);
            if (newState.StateMachine != this)
            {
                newState.StateMachine.EnterMachine(newState);
                ExitMachine();
                return;
            }
            _currentState?.Exit();
            _currentState = newState; 
            _currentStateTransitions = _currentState.Transitions;
            _currentState.Enter();
        }

        private (bool change, State newState) CheckTransitions(in IReadOnlyCollection<Transition> transitions)
        {
            foreach(var transition in transitions)
            {
                if (transition.Check() == true)
                {
                    return (true, transition.ToState);
                }
            }
            return (false, null);
        }
    }
}