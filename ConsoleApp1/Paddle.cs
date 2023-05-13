using SFML.Graphics;
using SFML.System;

public class Paddle
{
    private bool ownPaddle;

    private int windowWidth;
    private int windowHeight;

    public RectangleShape paddle;
    private float paddleSpeed = 0.1f;

    public Paddle(bool ownPaddle, int windowWidth, int windowHeight)
    {
        this.ownPaddle = ownPaddle;
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;

        paddle = new RectangleShape(new Vector2f(100, 20));
        paddle.FillColor = Color.White;

    }
    public void MovePaddle(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                if (paddle.Position.X > 0)
                {
                    paddle.Position -= new Vector2f(paddleSpeed, 0);
                }
                break;
            case Direction.Right:
                if (paddle.Position.X < windowWidth - paddle.Size.X)
                {
                    paddle.Position += new Vector2f(paddleSpeed, 0);
                }
                break;
        }
    }
    public void SetStartValues()
    {
        if (ownPaddle)
        {
            paddle.Position = new Vector2f(windowWidth / 2 - paddle.Size.X / 2, windowHeight - paddle.Size.Y - 10);
        }
        else
        {
            paddle.Position = new Vector2f(windowWidth / 2 - paddle.Size.X / 2, 10);
        }
    }


}
