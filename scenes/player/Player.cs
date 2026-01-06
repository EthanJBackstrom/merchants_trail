using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	
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
	}
	
	public override voide _PhysicsProcess(double delta)
	{
		HandleMovement();
		HandleHunger(delta);
	}
	
	private void HandlingMovement()
	{
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = input* _speed;
		MoveAndSlide();
	}
	
	private void HandlingHunger(double delta)
	{
		_currentHunger -= _hungerDecayRate * (float)delta;
		
		if (_currentHunger <= 0)
		{
			_currentHunger = 0;
			TakeDamage(1);
		}
	}
	
	public void TakeDamge(int amount)
	{
		_currentHealth -= amount;
		if (_currentHealth <= 0)
			Die();
	}
	
	public void Eat(float amount)
	{
		_currentHunger = mathf.Min(_currentHunger + amount, _maxHunger);
	}
	
	private void Die()
	{
		GD.Print("Player is Dead")
	}
