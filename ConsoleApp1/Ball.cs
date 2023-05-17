using SFML.Graphics;
using SFML.System;

public class Ball : IDrawable
{
    private Random rand = new Random();

    private CircleShape shape;
    private float ballSpeed;
    private Vector2f ballDirection;

    private Vector2u windowSize;
    private int bouncesCount = 0;

    public Ball(Vector2u windowSize) 
    {
        this.windowSize = windowSize;

        shape = new CircleShape(10);
        shape.FillColor = Color.Green;
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        ballSpeed = 0.07f;

        SetRandomBallDirection();

    }

    public Shape GetDrawaleObject() => shape;
    public void SetRandomBallDirection()
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

        ballDirection = new Vector2f((float)xDirection, (float)yDirection);
    }
    public void Move()
    {
        shape.Position += ballDirection * ballSpeed;
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
    public CircleShape GetDrawableObject() => shape;
    public Vector2f GetBallPosition() => shape.Position;
    public float GetBallRadius() => shape.Radius;
    public void SetBallPosition(Vector2f newPosition) => shape.Position = newPosition;
    public void OnBounce(Direction bounceDirection)
    {
        ballSpeed += 0.001f;
        switch (bounceDirection)
        {
            case Direction.Vertical:
                ballDirection.Y = -ballDirection.Y;
                bouncesCount++;
                Console.WriteLine(bouncesCount);
                break;
            case Direction.Horizontal:
                ballDirection.X = -ballDirection.X;
                bouncesCount++;
                Console.WriteLine(bouncesCount);
                break;
            default:
                throw new NotImplementedException();
        }

    }
    private void TryBounceFromWindowBorders()
    {
        if (shape.Position.X < shape.Radius || shape.Position.X + shape.Radius > windowSize.X)
        {
            OnBounce(Direction.Horizontal);
        }
    }

}
