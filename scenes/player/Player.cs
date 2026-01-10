using Godot;
using System;
using System.Collections.Generic;


public partial class Player : CharacterBody2D
{

	Dictionary<string, int> _wallet = new Dictionary<string, int>();
	Dictionary<string, int> _inventory = new Dictionary<string, int>();
	Dictionary<string, int> _insurance = new Dictionary<string, int>();
	Dictionary<string, int> _wellbeing = new Dictionary<string, int>();
	
	[Export]
	public float Speed = 300.0f;
	public float JumpVelocity = -400.0f;
	
	[Export]
	private int _maxHealth = 100;
	private int _currentHealth;
	
	[Export]
	private float _maxHunger = 100.0f;
	private float _currentHunger;
	private float _hungerDecayRate = 1.0f;
	
	public override void _Ready()
	{
		_currentHealth = _maxHealth;
		_currentHunger = _maxHunger;
		_wallet["Gold"] = 0;
		_wallet["Silver"] = 0;
		_wallet["Copper"] = 0;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		HandleMovement();
		HandleHunger(delta);
	}
	
	private void HandleMovement()
	{
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = input * Speed;
		MoveAndSlide();
	}
	
	private void HandleHunger(double delta)
	{
		_currentHunger -= _hungerDecayRate * (float)delta;
		
		if (_currentHunger <= 0)
		{
			_currentHunger = 0;
			TakeDamage(1);
		}
	}
	
	public void TakeDamage(int amount)
	{
		_currentHealth -= amount;
		if (_currentHealth <= 0)
			Die();
	}
	
	public void AddCoins(int amount, string type = "Gold")
	{
		_wallet[type] += amount;
	}
	
	public void Eat(float amount)
	{
		_currentHunger = Mathf.Min(_currentHunger + amount, _maxHunger);
		
		float healthGain = amount * 0.5f;
		_currentHealth = Mathf.Min(_currentHealth + (int)healthGain, _maxHealth);
	}
	
	private void Die()
	{
		GD.Print("Player is Dead");
	}}
