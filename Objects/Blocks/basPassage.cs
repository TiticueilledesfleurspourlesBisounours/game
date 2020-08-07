using Godot;
using System;

public class basPassage : StaticBody2D
{
   public CollisionPolygon2D collisionBas;

    public override void _Ready()
    {
        collisionBas = GetNode<CollisionPolygon2D>("CollisionPolygon2D"); 
    }

    //Si le levier est active, alors les collision shape du passage a gauche du spawn sont desactivees et le joueur peut passer
    public override void _Process(float delta)
    {
        if (Levier.isLevierEnabled == true)
        {
            collisionBas.Disabled = true;
        }
    }
  
}
