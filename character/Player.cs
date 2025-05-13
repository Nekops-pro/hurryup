using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    private int Speed {get;set;} = 200;//以像素每秒为单位的移动速度
    public Vector2 ScreenSize;//屏幕大小
    [Signal]
    public delegate void HitEventHandler();
    /**
     * 初始化
     */
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }
    public override void _PhysicsProcess(double delta)
    {
        var velocity = Vector2.Zero;
        if(Input.IsActionPressed("move_right")) velocity.X += 1;
        if(Input.IsActionPressed("move_left")) velocity.X -= 1;
        if(Input.IsActionPressed("move_up")) velocity.Y -= 1;
        if(Input.IsActionPressed("move_down")) velocity.Y += 1;
        
        if(velocity.Length() > 0) {
            velocity = velocity.Normalized() * Speed;
        }
        
        Velocity = velocity;
        MoveAndSlide();
        
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if(velocity.Length() > 0){//Length() 计算向量的模长：√(x² + y²)。
            velocity = velocity.Normalized() * Speed;//Normalized() 方法返回一个单位向量，即向量除以其模长得到的向量。
            if(velocity.Y > 0){
                animatedSprite2D.Animation = "walk_front";
            }else if(velocity.X != 0){
                animatedSprite2D.Animation = "walk_right";
                animatedSprite2D.FlipH = velocity.X < 0;//左边进行水平翻转
            }
            animatedSprite2D.Play();
        }else
        {
            animatedSprite2D.Stop();
        }

        Position += velocity * (float)delta;//delta 是当前帧与上一帧之间的时间差，以秒为单位。
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    }
    
    // public override void _PhysicsProcess(double delta){
    //     var velocity = Velocity;//Velocity 是一个属性，用于获取或设置节点的当前速度
    //     var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");//Input.GetVector 是Godot中获取输入方向向量的方法。
    //     MoveAndSlide();
    //     if(IsOnWall()){
    //         velocity = Vector2.Zero;
    //     }
    // }
    /**
     * esc退出
     */

    public override void _Input(InputEvent @event){
        if(@event is InputEventKey eventkey){
            if(eventkey.Pressed && eventkey.Keycode == Key.Escape){
                GetTree().Quit();
            }
        }
    }
    private void OnBodyEntered(Node2D body){//OnBodyEntered 是一个Godot引擎的碰撞检测回调方法，当玩家与其他物体的碰撞体接触时自动触发
        GD.Print("Body has entered");
        Hide();  // 隐藏玩家角色
        EmitSignal(SignalName.Hit);  // 会触发一个名为"Hit"的自定义信号，其他节点可以监听这个信号
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled,true);  // 会在下一帧禁用玩家的碰撞体，避免立即处理可能引发的其他碰撞
        
    }
    

    public void Start(Vector2 position){
        Position = position;  // 设置玩家初始位置
        Show();  // 显示玩家角色（如果之前被隐藏）
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;  // 启用碰撞检测
    }
}
