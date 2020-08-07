using Godot;
using System;

public class FinalZone : Node2D
{
    public bool isPlayerInFinal;

    public override void _Ready()
    {
        isPlayerInFinal = false;   
    }


 public override void _Process(float delta)
  {
        if (isPlayerInFinal)
        {
            Player.Respawn();
        }
      
  }
    //Verifie si le jeu entre dans la zone finale et si oui le transforme en vivant pour ne pas perdre en fantome car le corps est au debut 
    public void _on_Area2D_body_entered(Node node)
    {
        if (node == Test.player)
        {
            isPlayerInFinal = true;
        }
        
    }
}
