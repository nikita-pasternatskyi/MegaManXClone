using Godot;

[Tool]
public class MPStartUp : EditorPlugin
{
    private const string BasePath = "addons/MPToolKit/";
    private const string TransitionName = "Transition";
    private const string StateMachineName = "StateMachine";
    private const string NodeBaseName = "Node";
    private const string StateName = "State";
    private const string AnimatorName = "Animator";

    public override void _EnterTree()
    {
        var stateMachineScript = GD.Load<Script>(BasePath + "state_machine_plugin/state_machine/StateMachine.cs");
        var stateMachineTexture = GD.Load<Texture>(BasePath + "icons/state_machine.svg");
        AddCustomType(StateMachineName, NodeBaseName, stateMachineScript, stateMachineTexture);

        var transitionScript = GD.Load<Script>(BasePath + "state_machine_plugin/transition/Transition.cs");
        var transitionTexture = GD.Load<Texture>(BasePath + "icons/transition.svg");
        AddCustomType(TransitionName, NodeBaseName, transitionScript, transitionTexture);
        
        var stateScript = GD.Load<Script>(BasePath + "state_machine_plugin/state/State.cs");
        var stateTexture = GD.Load<Texture>(BasePath + "icons/state.svg");
        AddCustomType(StateName, NodeBaseName, stateScript, stateTexture);
}

    public override void _ExitTree()
    {
        RemoveCustomType(StateMachineName);
        RemoveCustomType(StateName);
        RemoveCustomType(TransitionName);
    }
}
