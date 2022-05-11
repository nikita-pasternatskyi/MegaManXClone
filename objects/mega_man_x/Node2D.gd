extends Node2D

export var sprite_path: NodePath
export var ghost_particles_path : NodePath
export var frame : float

var sprite : Sprite
var ghost_particles : Particles2D

func _ready():
	sprite = get_node(sprite_path) as Sprite
	ghost_particles = get_node(ghost_particles_path) as Particles2D

func _process(delta):
	ghost_particles.material.set("particle_frame_to_display", frame)
