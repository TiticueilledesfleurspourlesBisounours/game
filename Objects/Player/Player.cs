using Godot;
using System;

public class Player : KinematicBody2D
{

    public override void _Ready()
    {
        
    }
    
	

	public static float GRAVITY = 1800;
	public static float YLIMITESPEED = 500.0f;
	public static float SPEED = 2000;
	public static float JUMP_POWER = -24000 * 4;
	public static bool canMove = true;



	

	Vector2 UP = new Vector2(0, -1);
	Vector2 vel;
	bool on_ground;
	





	private void HorizontalMouvement(float delta)
	{
		/////////Move Right
		if (Input.IsActionPressed("ui_right"))
		{
			vel.x = SPEED ;
		}


		/////////Move Left
		if (Input.IsActionPressed("ui_left"))
		{
			vel.x = -SPEED;
		}

	}
	
	private void JUMP(float delta)
	{

		if (on_ground)
		{
			vel.y = 0;
		}
		
		if (on_ground && Input.IsActionPressed("ui_up"))
		{
			vel.y += JUMP_POWER  * delta;
			on_ground = false;
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		vel.x = 0;

		if (!on_ground && IsOnFloor())
		{
			on_ground = true;
		}
		else if (!IsOnFloor())
		{
			on_ground = false;
		}

		if (canMove)
		{
			HorizontalMouvement(delta);
			JUMP(delta);
		}

		vel.y += GRAVITY * delta;
		
		MoveAndSlide(vel, UP);
	}

}
