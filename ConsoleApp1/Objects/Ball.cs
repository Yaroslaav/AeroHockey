using SFML.Graphics;
using SFML.System;

public class Ball : IDrawable
{
    private Random rand = new Random();

    private CircleShape _shape;
    private float _ballSpeed;
    private Vector2f _ballDirection;

    private Vector2u _windowSize;

    public Ball(Vector2u windowSize) 
    {
        this._windowSize = windowSize;
        
        SetShapeSettings();
        _ballSpeed = 0.05f;

        SetRandomBallDirection();

    }

    private void SetShapeSettings()
    {
        _shape = new CircleShape(10);
        _shape.FillColor = Color.Green;
        _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);
    }

    public Shape GetDrawableObject() => _shape;
    private void SetRandomBallDirection()
    {
        int xDirection = 1;
        int yDirection = 1;

        if (Probability(50))
        {
            xDirection = -xDirection;
        }
        if (Probability(50))
        {
            yDirection = -yDirection;
        }

        _ballDirection = new Vector2f(xDirection, yDirection);
    }
    public void Move()
    {
        _shape.Position += _ballDirection * _ballSpeed;
        TryBounceFromWindowBorders();
    }

    private bool Probability(int procent)
    {
        if (procent < 0)
        {
            procent = 0;
        }
        else if (procent > 100)
        {
            procent = 100;
        }
        return rand.Next(1, 101) <= procent;
    }
    public Vector2f GetBallPosition() => _shape.Position;
    public float GetBallRadius() => _shape.Radius;
    public void SetBallPosition(Vector2f newPosition) => _shape.Position = newPosition;
    public void OnBounce(Direction bounceDirection)
    {
        _ballSpeed += 0.001f;
        switch (bounceDirection)
        {
            case Direction.Vertical:
                _ballDirection.Y = -_ballDirection.Y;
                break;
            case Direction.Horizontal:
                _ballDirection.X = -_ballDirection.X;
                break;
            default:
                throw new NotImplementedException();
        }

    }
    private void TryBounceFromWindowBorders()
    {
        if (_shape.Position.X < _shape.Radius || _shape.Position.X + _shape.Radius > _windowSize.X)
        {
            OnBounce(Direction.Horizontal);
        }
    }

}
