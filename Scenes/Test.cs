using Godot;
using System;

public class Test : Node2D
{
    public static Node2D deathScreen;
    public static Camera2D mainCamera;
    public static Camera2D deathCamera;
    public static Label doorLockedLabel;


    public override void _Ready()
    {
        deathScreen = GetNode<Node2D>("CameraForDeath/DeathScreen");
        mainCamera = GetNode<Camera2D>("blocks/Player/Camera2D");
        deathCamera = GetNode<Camera2D>("CameraForDeath");
        doorLockedLabel = GetNode<Label>("DoorLocked");
    }



 public override void _Process(float delta)
 {

 }
}
