using SFML.Graphics;
using SFML.System;

public class Ball : IDrawable
{

    private CircleShape _shape;
    private float _ballSpeed;
    private Vector2f _ballDirection;

    private Vector2u _windowSize;

    public Ball(Vector2u windowSize) 
    {
        _windowSize = windowSize;
        
        SetShapeSettings();
        _ballSpeed = 0.3f;

        SetRandomBallDirection();

    }

    private void SetShapeSettings()
    {
        _shape = new CircleShape(10);
        _shape.FillColor = Color.Green;
        _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);
    }

    public void Draw() => Window.renderWindow.Draw(_shape);
    private void SetRandomBallDirection()
    {
        int xDirection = 1;
        int yDirection = 1;
        
        if (Probability.Next(50))
        {
            xDirection = -xDirection;
        }
        if (Probability.Next(50))
        {
            yDirection = -yDirection;
        }

        _ballDirection = new Vector2f(xDirection, yDirection);
    }
    public void Move()
    {
        _shape.Position += _ballDirection * _ballSpeed * Time.deltaTime;
        TryBounceFromWindowBorders();
    }

    public Vector2f GetBallPosition() => _shape.Position;
    public float GetBallRadius() => _shape.Radius;
    public Vector2f GetBallDirection() => _ballDirection;
    public void SetBallPosition(Vector2f newPosition) => _shape.Position = newPosition;
    public int bounces = 0;
    public void OnBounce(Direction bounceDirection)
    {
        _ballSpeed += 0.001f;
        switch (bounceDirection)
        {
            case Direction.Vertical:
                _ballDirection.Y = -_ballDirection.Y;
                bounces++;
                Console.WriteLine(bounces);
                break;
            case Direction.Horizontal:
                _ballDirection.X = -_ballDirection.X;
                bounces++;
                Console.WriteLine(bounces);
                break;
            default:
                throw new NotImplementedException();
        }

    }
        private void TryBounceFromWindowBorders()
        {
            if (_shape.Position.X < _shape.Radius && _ballDirection.X < 0)
            {
                OnBounce(Direction.Horizontal);
            }else if (_shape.Position.X + _shape.Radius > _windowSize.X && _ballDirection.X > 0)
            {
                OnBounce(Direction.Horizontal);
            }
        }

}
