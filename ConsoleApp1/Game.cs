using SFML.Graphics;
using SFML.System;
using SFML.Window;

public enum Direction 
{
    Left,
    Right,
    Vertical, 
    Horizontal,
}

public class Game
{
    public bool isPlaying;

    public const int windowWidth = 800;
    public const int windowHeight = 600;

    public RenderWindow window;

    Paddle ownPaddle;
    Paddle enemyPaddle;

    Ball ball;


    public void Start()
    {
        SetWingow();
        SetPaddles();
        SetBall();

        SetObjectsStartPosition();

        isPlaying = true;
        GameLoop();
    }
    public void GameLoop()
    {
        while(isPlaying)
        {
            window.DispatchEvents();

            MoveInputProcessing();
            TryMoveOpponentPaddle();

            ball.Move();

            TryBounceFromPaddle();

            CheckIfSomeoneWon();

            DrowScene();
        }
    }
    private void DrowScene()
    {
        window.Clear(Color.Black);

        window.Draw(ownPaddle.GetDrawableObject());
        window.Draw(enemyPaddle.GetDrawableObject());
        window.Draw(ball.GetDrawableObject());

        window.Display();
    }
    private void SetWingow()
    {
        window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Aero Hockey");
        window.Closed += WindowClosed;

    }
    private void SetPaddles()
    {
        ownPaddle = new Paddle(window.Size);
        enemyPaddle = new Paddle(window.Size);
    }
    private void SetBall() => ball = new Ball(window.Size);

    private void WindowClosed(object sender, EventArgs e)
    {
        isPlaying = false;
    }
    private void MoveInputProcessing()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            ownPaddle.MovePaddle(Direction.Left);
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            ownPaddle.MovePaddle(Direction.Right);
        }
    }
    private void TryMoveOpponentPaddle()
    {
        Vector2f ballPosition = ball.GetBallPosition();
        Vector2f paddlePosition = enemyPaddle.GetPosition();

        if (ballPosition.X < paddlePosition.X)
        {
            enemyPaddle.MovePaddle(Direction.Left);
        }
        else if (ballPosition.X > paddlePosition.X)
        {
            enemyPaddle.MovePaddle(Direction.Right);
        }
    }

    private void CheckIfSomeoneWon()
    {
        Vector2f ballPosition = ball.GetBallPosition();
        float ballRadius = ball.GetBallRadius();

        if (ballPosition.Y < 0)
        {
            Console.WriteLine("Player scores!");
            isPlaying = false;
        }
        else if (ballPosition.Y + 2 * ballRadius > windowHeight)
        {
            Console.WriteLine("Opponent scores!");
            isPlaying = false;
        }

    }
    private void SetObjectsStartPosition()
    {
        SetStartPaddlesPosition();
        SetStartBallPosition();
    }
    private void TryBounceFromPaddle()
    {
        Vector2f ballPosition = ball.GetBallPosition();
        float ballRadius = ball.GetBallRadius();

        if(ownPaddle.IfSmthHit(new Vector2f(ballPosition.X + ballRadius, ballPosition.Y + ballRadius)))
        {
            ball.OnBounce(Direction.Vertical);
        }
        if (enemyPaddle.IfSmthHit(new Vector2f(ballPosition.X - ballRadius, ballPosition.Y - ballRadius)))
        {
            ball.OnBounce(Direction.Vertical);
        }

    }
    public void SetStartBallPosition()
    {
        float ballRadius = ball.GetBallRadius();
        ball.SetBallPosition(new Vector2f(windowWidth / 2 - ballRadius, windowHeight / 2 - ballRadius));
    }
    public void SetStartPaddlesPosition()
    {
        Vector2f paddleSize = ownPaddle.GetSize();
        ownPaddle.SetPosition(new Vector2f(windowWidth / 2 - paddleSize.X / 2, windowHeight - paddleSize.Y));

        paddleSize = enemyPaddle.GetSize();
        enemyPaddle.SetPosition(new Vector2f(windowWidth / 2 - paddleSize.X / 2, paddleSize.Y));
    }


}
