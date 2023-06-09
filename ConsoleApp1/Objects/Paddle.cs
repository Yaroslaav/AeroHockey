﻿using SFML.Graphics;
using SFML.System;

public class Paddle : GameObject
{

    private Vector2u _windowSize;

    private RectangleShape _shape;
    private float _paddleSpeed = 0.5f;

    public Paddle(Vector2u windowSize)
    {
        _windowSize = windowSize;

        Start();
    }

    public override void Start()
    {
        base.Start();
        _shape = new RectangleShape(new Vector2f(100, 20));
        _shape.FillColor = Color.White;
        _shape.Origin = new Vector2f(_shape.Size.X / 2, _shape.Size.Y / 2);

        transformable = _shape;
    }
    
    public void MovePaddle(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                if (_shape.Position.X - _shape.Size.X / 2 > 0)
                {
                    _shape.Position -= new Vector2f(_paddleSpeed, 0) * Time.deltaTime;
                }
                break;
            case Direction.Right:
                if (_shape.Position.X + _shape.Size.X / 2 < _windowSize.X)
                {
                    _shape.Position += new Vector2f(_paddleSpeed, 0) * Time.deltaTime;
                }
                break;
        }
    }
    public void SetPosition(Vector2f newPosition) => _shape.Position = newPosition;
    public Vector2f GetPosition() => _shape.Position;

    public bool IfSmthHit(Vector2f objPosition) => _shape.GetGlobalBounds().Contains(objPosition.X, objPosition.Y);
    public Vector2f GetSize() => _shape.Size;
}
