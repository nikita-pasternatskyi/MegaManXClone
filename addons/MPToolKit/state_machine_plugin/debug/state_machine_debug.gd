extends CanvasLayer

export var pathToStateMachine : NodePath

const METHOD_NAME = "StateChanged"
onready var label = $RichTextLabel
var state_machine : Node

func _ready():
	state_machine = get_node(pathToStateMachine)
	state_machine.connect(METHOD_NAME, self, "_on_StateMachine_StateChanged")
	pass


func _on_StateMachine_StateChanged(new_state):
	var state_node = new_state as Node
	var newStateMachine = new_state.StateMachine as Node
	if(state_machine != newStateMachine):
		state_machine.disconnect(METHOD_NAME, self, "_on_StateMachine_StateChanged")
		state_machine = newStateMachine
		state_machine.connect(METHOD_NAME, self, "_on_StateMachine_StateChanged")
		pass
	label.text = state_machine.name + ": " + state_node.name
	pass
