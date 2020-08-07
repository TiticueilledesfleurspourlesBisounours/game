using Godot;
using System;

public class basPassage : StaticBody2D
{
   public CollisionPolygon2D collisionBas;

    public override void _Ready()
    {
        collisionBas = GetNode<CollisionPolygon2D>("CollisionPolygon2D"); 
    }


    public override void _Process(float delta)
    {
        if (Levier.isLevierEnabled == true)
        {
            collisionBas.Disabled = true;
        }
    }
  
}
