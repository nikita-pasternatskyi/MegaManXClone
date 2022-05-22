using Godot;
using MP.Extensions;
using System.Collections.Generic;

namespace MP.FiniteStateMachine
{

    public sealed class State : Node
    {
        [Signal] private delegate void StateEntered();
        [Signal] private delegate void StateExit();

        public StateMachine StateMachine { get; private set; }
        public IReadOnlyList<Transition> Transitions { get; private set; }

        private List<StateAction> _enterActions;
        private List<StateAction> _updateActions;
        private List<StateAction> _fixedUpdateActions;
        private List<StateAction> _exitActions;

        public override void _Ready()
        {
            this.Disable();
        }

        public void Init(StateMachine baseStateMachine, List<StateAction> listActions = null, List<Transition> listTransitions = null)
        {
            StateMachine = baseStateMachine;

            _enterActions = new List<StateAction>();
            _fixedUpdateActions = new List<StateAction>();
            _updateActions = new List<StateAction>();
            _exitActions = new List<StateAction>();

            var actionsNode = FindNode("Actions", false);
            var target = actionsNode == null ? this : actionsNode;

            var actions = listActions == null ? target.GetChildren<StateAction>() : listActions;
            foreach (var action in actions)
            {
                AddActionToAList(action);
                action.Init(baseStateMachine);
            }

            if (listTransitions == null)
            {
                Transitions = GetTransitions(baseStateMachine);
                return;
            }
            Transitions = listTransitions;
        }

        private List<Transition> GetTransitions(StateMachine stateMachine)
        {
            List<Transition> stateTransitions = new List<Transition>();

            var transitionsNode = FindNode("Transitions", false);
            var target = transitionsNode == null ? this : transitionsNode;

            var children = target.GetChildren<Transition>();

            if (children.IsEmpty())
            {
                GD.Print($"{Name} State has no transitions!");
            }

            foreach (var transition in children)
            {
                transition.Init(stateMachine);
                stateTransitions.Add(transition);
            }
            return stateTransitions;
        }

        private void AddActionToAList(StateAction action)
        {
            if (action.OnEnter == true)
            {
                _enterActions.Add(action);
            }
            if (action.OnExit == true)
            {
                _exitActions.Add(action);
            }
            if (action.OnFixedUpdate == true)
            {
                _fixedUpdateActions.Add(action);
            }
            if (action.OnUpdate == true)
            {
                _updateActions.Add(action);
            }
        }

        public void Process(float delta)
        {
            CallActions(_updateActions, delta);
        }

        public void PhysicsProcess(float delta) => CallActions(_fixedUpdateActions, delta);

        public void Enter()
        {
            EnterStateCallback();
            EmitSignal(nameof(StateEntered));
        }

        public void Exit()
        {
            ExitStateCallback();
            EmitSignal(nameof(StateExit));
        }

        private void CallActions(in List<StateAction> actionList, float delta = -1)
        {
            for (int i = 0; i < actionList.Count; i++)
            {
                StateAction item = actionList[i];
                item.Act(delta);
            }
        }

        private void EnterStateCallback()
        {
            for (int i = 0; i < _enterActions.Count; i++)
            {
                StateAction item = _enterActions[i];
                item.OnStateEnter();
                if(item is InstantStateAction)
                    item.Act(-1);
            }
        }

        private void ExitStateCallback()
        {
            for (int i = 0; i < _exitActions.Count; i++)
            {
                StateAction item = _exitActions[i];
                item.OnStateExit();
                if (item is InstantStateAction)
                    item.Act(-1);
            }
        }
    }
}