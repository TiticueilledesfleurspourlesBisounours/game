using Godot;
using System;

public class Player : KinematicBody2D
{


	[Export] public float Gravity = 980f;
	[Export] public float Speed = 700;
	[Export] public float JumpPowerAlive = 1100;
	[Export] public float JumpPowerGost = 1100;
	[Export] public float WaitTimeDeath = 30;

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
	public static AnimationPlayer animationAlive;
	public static AnimationPlayer animationGhost;
	public static AudioStreamPlayer backgroundMusic;
	public static AudioStreamPlayer backgroundWind;
	public static int numberOfDeath;
	


	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		alive = GetNode<Sprite>("Alive");
		gost = GetNode<Sprite>("Gost");
		animationAlive = GetNode<AnimationPlayer>("AnimationPlayerAlive");
		animationGhost = GetNode<AnimationPlayer>("AnimationPlayerGhost");
		backgroundMusic = GetNode<AudioStreamPlayer>("BackgroundMusic");
		backgroundWind = GetNode<AudioStreamPlayer>("WindBackground");
		backgroundMusic.Play();
		backgroundWind.Play();
		numberOfDeath = 3;
	}

	public override void _PhysicsProcess(float delta)
	{
		//Change les textes vus au debut par le joueur apres activation du levier
		if (Levier.isLevierEnabled == true)
		{
			Test.doorLockedLabel.Text = "";
			Test.thirdLabel.Text = "You are on the right way ...";
			Test.thirdLabel2.Text = "";
			Test.secondLabel.Text = "Go back further ...";
			Test.secondLabel2.Text = "";
			Test.firstLabel.Text = "Sometimes, good things are hidden behind the dark things ...";
			Test.wrongWay.Visible = true;
			
		}

		if (backgroundMusic.Playing == false)
		{
			backgroundMusic.Play();
		}

		if(backgroundWind.Playing == false)
		{
			backgroundWind.Play();
		}

		if(isAlive == true)
		{
			animationGhost.Stop();
			animationAlive.Play("FlyAlive");
		}

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

		//Enregistre la position ou le joueur est devenu fantome
		if (isAlive == false)
		{
			alive.Position = ToLocal(bodyPos);
			
		}
		//Ecran indiquant que le joueur a perdu
		if (isAlive == false && timer.TimeLeft == 0)
		{
			Test.mainCamera.Current = false;
			Test.deathCamera.Current = true;
			Test.deathScreen.Visible = true;
		}
			

		if (Input.IsActionPressed("Interact"))
		{
			GD.Print(Dist());
			if (Dist() < DistGost)
				Respawn();
		}

		vel.y += Gravity * delta;
		vel = MoveAndSlide(vel, new Vector2(0, -1));
	}

	//Tue le joueur pour etre en mode fantome
	public void Kill()
	{
		if(isAlive)
		{
			//Si le nombre de mort est superieur a "numberOfDeath" alors le joueur perd
			if (numberOfDeath > 0)
			{
				bodyPos = GlobalPosition;
				isAlive = false;
				gost.Visible = true;
				alive.RotationDegrees = 90;
				bodyPos.y += YCorrection;
				bodyPos.x += XCorrection;
				timer.Start(WaitTimeDeath);
				animationAlive.Stop();
				animationGhost.Play("FlyGhost");
				numberOfDeath -= 1;
			}
			else
			{
				Test.mainCamera.Current = false;
				Test.deathCamera.Current = true;
				Test.deathScreen.Visible = true;
			}
			
		}
	}

	public float Dist()
	{
		float a = XFact * Mathf.Pow(alive.Position.x, 2);
		a += XFact * Mathf.Pow(alive.Position.y, 2);
		a = Mathf.Sqrt(a);
		return a;
	}

	//Permet au joueur de revenir dans le mode en vie
	public static void Respawn()
	{
		isAlive = true;
		gost.Visible = false;
		alive.Position = new Vector2(0, 0);
		alive.RotationDegrees = 0;
	}

	//Permet de tourner le personnage en fonction du sens de mouvement
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
