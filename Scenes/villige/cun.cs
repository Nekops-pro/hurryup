using Godot;
using System;

public partial class cun : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void NewGame(){
		var player = GetNode<Player>("Player");
		if(player != null){
			player.Position = GetNode<Marker2D>("startposition").Position;
		}
	}

}
