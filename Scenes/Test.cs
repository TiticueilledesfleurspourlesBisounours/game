using Godot;
using System;

public class Test : Node2D
{
    public static Node2D deathScreen;
    public static Camera2D mainCamera;
    public static Camera2D deathCamera;
    public static Label doorLockedLabel;
    public static Label thirdLabel;
    public static Label thirdLabel2;
    public static Label secondLabel;
    public static Label secondLabel2;
    public static Label firstLabel;
    public static Label wrongWay;
    public static CollisionPolygon2D platform1;


    public override void _Ready()
    {
        deathScreen = GetNode<Node2D>("CameraForDeath/DeathScreen");
        mainCamera = GetNode<Camera2D>("blocks/Player/Camera2D");
        deathCamera = GetNode<Camera2D>("CameraForDeath");
        doorLockedLabel = GetNode<Label>("DoorLocked");
        thirdLabel = GetNode<Label>("But");
        thirdLabel2 = GetNode<Label>("WhatAHappiness");
        secondLabel = GetNode<Label>("HowToDie");
        secondLabel2 = GetNode<Label>("HowToRespawn");
        firstLabel = GetNode<Label>("Press E to interact");
        wrongWay = GetNode<Label>("wrongway");
        platform1 = GetNode<CollisionPolygon2D>("StaticBody2D/CollisionPolygon2D");
    }



 public override void _Process(float delta)
 {
        if (Player.isAlive == false)
        {
            platform1.Disabled = false;
        }
        else
        {
            platform1.Disabled = true;
        }
 }

}
