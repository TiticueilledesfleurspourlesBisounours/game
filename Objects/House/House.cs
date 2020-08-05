using Godot;
using System;

public class House : Node2D
{

    private Node door;
    public static CollisionPolygon2D thirdFloor;
    public static CollisionPolygon2D thirdFloorExit;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        door = GetNode("door");
        thirdFloor = GetNode<CollisionPolygon2D>("floors's house/thirdFloor");
        thirdFloorExit = GetNode<CollisionPolygon2D>("floors's house/thirdFloorExit");

    }


    public override void _Process(float delta)
    {
        if (Player.IsAlive == false)
        {
            thirdFloor.OneWayCollision = true;
            thirdFloorExit.Disabled = true;
        }
        else
        {
            thirdFloor.OneWayCollision = false;
            thirdFloorExit.Disabled = false;
        }

    }

    public void On_Levier_Activated()
    {
        Open_Door();
    }


    public void Open_Door()
    {
        door.GetNode<CollisionPolygon2D>("Collision").Disabled = true;
        door.GetNode<Sprite>("img").Texture = GD.Load<Texture>("res://Objects/Blocks/dooropen..png");
    }
}
