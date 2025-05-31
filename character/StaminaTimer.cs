using Godot;
using System;

public partial class StaminaTimer : Timer
{

	[Export] private double maxstamina {get;set;} = 50;//玩家的耐力值上限
	[Export] private double reststamina {get;set;} = 2.5;//玩家的耐力值恢复速度
	[Export] private double consumestamina {get;set;} = 2;//玩家的耐力值消耗速度
	[Export] private double attackstamina {get;set;} = 5;//玩家的攻击行为耐力值消耗值
	public double stamina;//玩家的当前耐力值
	public int staminapct;//玩家的当前耐力值百分比
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		stamina = maxstamina;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		GD.Print("当前体力值："+(stamina/maxstamina*100));
		staminapct = (int)(stamina/maxstamina*100);
	}


	//休息状态
	public void OnRestState(double delta){
		//检测体力值是否为最大值
		if(stamina < maxstamina){
			stamina += reststamina * delta;
		}else{
			stamina = maxstamina;
		}
	}

	//消耗状态
	public void OnConsumeState(double delta){
		//检测体力值是否为0
		if(stamina > 0){
			stamina -= reststamina * delta;
		}else{
			stamina = 0;
		}
	}

	//攻击状态
	public void OnAttackState(){
		//检测体力值是否为0
		if(stamina > 0){
			stamina -= reststamina;
		}else{
			stamina = 0;
		}
	}
}
