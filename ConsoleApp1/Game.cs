using SFML.Graphics;
using SFML.System;
using SFML.Window;
public enum Direction 
{
    Left,
    Right,
}

public class Game
{
    Random rand = new Random();

    public bool isPlaying;

    public const int windowWidth = 800;
    public const int windowHeight = 600;

    public RenderWindow window;

    Paddle ownPaddle;
    Paddle enemyPaddle;

    Ball ball;


    public void Start()
    {
        window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Aero Hockey");
        window.Closed += WindowClosed;

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

            CheckIfSomeoneWon();

            window.Clear(Color.Black);

            window.Draw(ownPaddle.paddle);
            window.Draw(enemyPaddle.paddle);
            window.Draw(ball.ball);

            window.Display();

        }
    }
    private void SetPaddles()
    {
        ownPaddle = new Paddle(true, windowWidth, windowHeight);
        enemyPaddle = new Paddle(false, windowWidth, windowHeight);
    }
    private void SetBall() => ball = new Ball(windowWidth, windowHeight, ownPaddle, enemyPaddle);

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
        if (ball.ball.Position.X < enemyPaddle.paddle.Position.X)
        {
            enemyPaddle.MovePaddle(Direction.Left);
        }
        else if (ball.ball.Position.X > enemyPaddle.paddle.Position.X)
        {
            enemyPaddle.MovePaddle(Direction.Right);
        }
    }

    private void CheckIfSomeoneWon()
    {
        if (ball.ball.Position.Y < 0)
        {
            Console.WriteLine("Player scores!");
            isPlaying = false;
            return;
        }
        else if (ball.ball.Position.Y + 2 * ball.ball.Radius > windowHeight)
        {
            Console.WriteLine("Opponent scores!");
            isPlaying = false;
            return;
        }

    }
    private void SetObjectsStartPosition()
    {
        ownPaddle.SetStartValues();
        enemyPaddle.SetStartValues();
        ball.SetStartValues();
    }


}
