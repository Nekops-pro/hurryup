using Godot;
using System;

public partial class RootNode : Node2D
{

	[Export] private PackedScene MobScene{get;set;}
	public int Score;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameStart();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void GameStart(){
		Score = 0;
		var player = GetNode<Player>("Player");
		var startposition = GetNode<Marker2D>("StartPosition");
		player.Start(startposition.Position);

		GetNode<Timer>("StartTimer").Start();
	}

	public void GamerOver(){
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void OnScoreTimerTimeout(){
		Score++;
	}

	public void OnStartTimerTimeout(){
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	public void MobTimerTimeout(){
		Mob mob = MobScene.Instantiate<Mob>();
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		mob.Position = mobSpawnLocation.Position;
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);
		AddChild(mob);
	}
}
