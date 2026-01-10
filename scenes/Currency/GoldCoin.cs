using Godot;
using System;

public partial class GoldCoin : Area2D
{
	[Export]
	private int _value = 1;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntering;
	}
	
	private void OnBodyEntering(Node2D body)
	{
		if (body is Player player)
		{
			player.AddCoins(_value);
			QueueFree();
		}
	}
	
}
