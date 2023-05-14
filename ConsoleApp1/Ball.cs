using SFML.Graphics;
using SFML.System;

public class Ball
{
    private Random rand = new Random();

    private int windowWidth;
    private int windowHeight;

    public CircleShape ball;
    private float ballSpeed = 0.06f;
    private Vector2f ballDirection = new Vector2f(-1.0f, 1.0f);

    private Paddle ownPaddle;
    private Paddle enemyPaddle;


    public Ball(int windowWidth, int windowHeight, Paddle ownPaddle, Paddle enemyPaddle) 
    {
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;
        this.ownPaddle = ownPaddle;
        this.enemyPaddle = enemyPaddle;

        ball = new CircleShape(10);
        ball.FillColor = Color.Green;
        ballSpeed = 0.1f;
        SetRandomBallDirection();

    }
    public void SetRandomBallDirection()
    {
        double xDirection = rand.NextDouble();
        double yDirection = rand.NextDouble();

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
        ball.Position += ballDirection * ballSpeed;
        TryBounce();
    }
    private void TryBounce()
    {
        if (ball.Position.X < 0 || ball.Position.X + 2 * ball.Radius > windowWidth)
        {
            ballSpeed += 0.0007f;
            ballDirection.X = -ballDirection.X;
        }

        if (ownPaddle.paddle.GetGlobalBounds().Contains(ball.Position.X + ball.Radius, ball.Position.Y + ball.Radius))
        {
            ballSpeed += 0.0007f;
            ballDirection.Y = -ballDirection.Y;
        }
        if (enemyPaddle.paddle.GetGlobalBounds().Contains(ball.Position.X + ball.Radius, ball.Position.Y + ball.Radius))
        {
            ballSpeed += 0.0007f;
            ballDirection.Y = -ballDirection.Y;
        }

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
    public void SetStartValues()
    {
        ball.Position = new Vector2f(windowWidth / 2 - ball.Radius, windowHeight / 2 - ball.Radius);
    }


}
