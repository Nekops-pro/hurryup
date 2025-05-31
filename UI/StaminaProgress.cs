using Godot;
using System;

public partial class StaminaProgress : TextureProgressBar
{
    private CharacterBody2D _player;
    private int Range = 100;
    
    public override void _Ready()
    {
        _player = GetNode<CharacterBody2D>("/root/cun/Player");
    }

    public override void _Process(double delta)
    {
        OnStaminaProgress();
    }

    public void OnStaminaProgress(){
		var _staminaTimer = _player.GetNode<StaminaTimer>("StaminaTimer");
        if(_staminaTimer != null)
        {
            Range = (int)_staminaTimer.Get("staminapct");
            Value = Range; // 确保更新进度条显示
        }
    }
}
