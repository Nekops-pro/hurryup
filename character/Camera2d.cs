using Godot;
using System;

public partial class Camera2d : Camera2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		getMap();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void getMap(){
		Node thisnode = this;
		while(thisnode != null){
			var map = thisnode.GetNodeOrNull<TileMapLayer>("地面");
			if(map != null){
				var maps = map.GetUsedRect();
				var rectsize = map.TileSet.TileSize;
				maps.Size *= rectsize;
				LimitLeft = 0;
				LimitTop = 0;
				LimitRight = maps.Size.X;
				LimitBottom = maps.Size.Y;
				GD.Print("地图宽度：" + maps.Size.X);
				return;
			}
			thisnode = thisnode.GetParent();
		}
		
		// 方法2：使用FindChild递归查找（Godot 4.0+）
		// var map = FindChild("地面", true, false) as TileMapLayer;
		
		GD.PrintErr("未找到地面TileMapLayer节点");
	}
	
}
