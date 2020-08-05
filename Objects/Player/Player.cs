using Godot;
using System;

public class Player : KinematicBody2D
{


	[Export] public float Gravity = 980f;
	[Export] public float Speed = 900;
	[Export] public float JumpPowerAlive = 1200;
	[Export] public float JumpPowerGost = 1200;
	[Export] public float WaitTimeDeath = 25;

	[Export] public float XCorrection = 0;
	[Export] public float YCorrection = 100;

	[Export] public float XFact = 1.6f;
	[Export] public float YFact = 0.7f;
	[Export] public float DistGost = 400;

	public static Timer Timer
	{
		get => timer;
	}
	public static bool IsAlive
	{
		get => isAlive;
	}


	private static Timer timer;
	private static Sprite alive;
	private static Sprite gost;

	private static Vector2 vel;
	public static bool canMove = true;
	public static bool isAlive = true;
	private static Vector2 bodyPos;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		alive = GetNode<Sprite>("Alive");
		gost = GetNode<Sprite>("Gost");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (Input.IsActionPressed("Die"))
		{
			Kill();
		}

			if (IsOnFloor())
			vel.x = 0;

		if (canMove)
		{
			HorizontalMouvement(delta);
			JUMP();
		}

		if (isAlive == false)
			alive.Position = ToLocal(bodyPos);

		if (Input.IsActionPressed("Interact"))
		{
			GD.Print(Dist());
			if (Dist() < DistGost)
				Respawn();
		}

		vel.y += Gravity * delta;
		vel = MoveAndSlide(vel, new Vector2(0, -1));
	}

	public void Kill()
	{
		if(isAlive)
		{
			bodyPos = GlobalPosition;
			isAlive = false;
			gost.Visible = true;
			alive.RotationDegrees = 90;
			bodyPos.y += YCorrection;
			bodyPos.x += XCorrection;
			timer.Start(WaitTimeDeath);
		}
	}

	public float Dist()
	{
		float a = XFact * Mathf.Pow(alive.Position.x, 2);
		a += XFact * Mathf.Pow(alive.Position.y, 2);
		a = Mathf.Sqrt(a);
		return a;
	}

	public void Respawn()
	{
		isAlive = true;
		gost.Visible = false;
		alive.Position = new Vector2(0, 0);
		alive.RotationDegrees = 0;
	}

	public void Timeout()
	{
		if(isAlive == false)
		{
			//Lost Game
		}
		GD.Print("Death");
		Kill();
	}

	private void HorizontalMouvement(float delta)
	{
		if (Input.IsActionPressed("ui_right"))
		{
			vel.x = Speed;
			if (isAlive)
				alive.FlipH = false;
			else
				gost.FlipH = false;
		}
		else if (Input.IsActionPressed("ui_left"))
		{
			vel.x = -Speed;
			if (isAlive)
				alive.FlipH = true;
			else
				gost.FlipH = true;
		}
	}

	private void JUMP()
	{
		if (IsOnFloor())
			if (Input.IsActionPressed("ui_up"))
			{
				if (isAlive)
					vel.y -= JumpPowerAlive;
				else
					vel.y -= JumpPowerGost;
			}
	}
}
