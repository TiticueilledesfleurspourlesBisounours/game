using Godot;
using System;

public class Levier : Node2D
{
    public Texture ferme;
    public Texture ouvert;
    public Sprite sprite;
    public bool isPlayerIn;
    public static bool isLevierEnabled;

    [Signal]
    delegate void On_Activate();

    public override void _Ready()
    {
        ferme = GD.Load<Texture>("res://Objects/Blocks/levierFerme.png");
        ouvert = GD.Load<Texture>("res://Objects/Blocks/levierOuvert.png");
        sprite = GetNode<Sprite>("Area2D/Sprite");
        sprite.Texture = ferme;
        isLevierEnabled = false;
        Connect("On_Activate", GetNode(".."), "On_Levier_Activated");
    }
    public override void _Process(float delta)
    {
        if (isPlayerIn && !isLevierEnabled)
        {
            if(Input.IsActionPressed("Interact"))
            {
                sprite.Texture = ouvert;
                isLevierEnabled = true;
                EmitSignal("On_Activate");
            }
        }
    }
    public void Entered(Node node)
    {
        if (node.GetGroups().Contains("Player"))
        {
            isPlayerIn = true;
        }  
    }
    public void Exited(Node node)
    {
        if (node.GetGroups().Contains("Player"))
        {
            isPlayerIn = false;
        }
    }



}
