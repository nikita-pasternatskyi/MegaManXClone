extends Node


func _unhandled_key_input(event):
	if(Input.is_action_just_pressed("pause")):
		get_tree().paused = !get_tree().paused
	
