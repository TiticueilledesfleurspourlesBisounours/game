using Godot;
using System;

public class House : Node2D
{

    private Node door;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        door = GetNode("door");
        
    }


    public override void _Process(float delta)
    {
        
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
