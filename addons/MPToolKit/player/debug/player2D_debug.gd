extends Node2D

export var player_2d_node_path : NodePath

onready var parent = get_node(player_2d_node_path)
onready var label = $RichTextLabel

const LINE_BREAK = "\n"
const SEPARATOR = ", "
const VELOCITY = "Velocity: "
const GROUNDED = LINE_BREAK + "Grounded: "
const GROUND_NORMAL = LINE_BREAK + "Ground Normal: "
const SNAP_VECTOR = LINE_BREAK + "Snap Vector: "
const REQUESTED_JUMP = LINE_BREAK + "Requested Jump: "

func _ready():
	label.bbcode_enabled = true
	pass

func _process(delta):
	var velocityX = parent.Velocity.x
	var velocityY = parent.Velocity.y
	velocityX = stepify(velocityX, 0.01)
	velocityY = stepify(velocityY, 0.01)
	var velocity = VELOCITY + str(velocityX) + SEPARATOR + str(velocityY)
	
	var grounded = GROUNDED + str(parent.get_Grounded())
	
	var groundNormalX = parent.get_GroundNormal().x
	var groundNormalY = parent.get_GroundNormal().y
	groundNormalX = stepify(groundNormalX,0.01)
	groundNormalY = stepify(groundNormalY, 0.01)
	var groundNormal = GROUND_NORMAL + str(groundNormalX) + SEPARATOR + str(groundNormalY)
	
	var snapVector = SNAP_VECTOR + str(parent._snapVector);
	var requestedJump = REQUESTED_JUMP + str(parent._requestedJump);
	
	label.bbcode_text = velocity + grounded + groundNormal + snapVector + requestedJump
	pass
