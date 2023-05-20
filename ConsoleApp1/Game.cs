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
    private bool _isPlaying;
    
    private Paddle ownPaddle;
    private Paddle enemyPaddle;

    private Ball ball;

    private List<IDrawable> _drawableObjects = new ();

    public void Start()
    {
        Window.SetWindow();
        Window.renderWindow.Closed += WindowClosed;
        
        SetPaddles(); 
        SetBall();
        SetObjectsStartPosition();
        
        _isPlaying = true;
        
        Time.Start();
        GameLoop();
    }
    private void GameLoop()
    {
        while(_isPlaying)
        {
            Time.UpdateTimer();
            Window.DispatchEvents();

            MoveInputProcessing();
            enemyPaddle.TryMoveAIPaddle(ball);

            MoveBall();
            CheckIfSomeoneWon();

            Window.DrawScene(_drawableObjects.ToArray());
            
        }

        Stop();
    }

    private void Stop()
    {
        _drawableObjects = new ();
        Window.Close();
        Start();
    }

    private void MoveBall()
    {
        ball.Move();
        ball.TryBounceFromPaddle(ownPaddle, Position.Bot);
        ball.TryBounceFromPaddle(enemyPaddle, Position.Top);
    }
    private void SetPaddles()
    {
        ownPaddle = new Paddle(Window.renderWindow.Size);
        _drawableObjects.Add(ownPaddle);
        
        enemyPaddle = new Paddle(Window.renderWindow.Size);
        _drawableObjects.Add(enemyPaddle);
    }
    private void SetBall()
    {
        ball = new Ball(Window.renderWindow.Size);
        _drawableObjects.Add(ball);
    } 
    private void WindowClosed(object? sender, EventArgs e) => _isPlaying = false;
    
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
    private void CheckIfSomeoneWon()
    {
        Vector2f ballPosition = ball.GetBallPosition();

        if (ballPosition.Y < 0)
        {
            Console.WriteLine("Player scores!");
            _isPlaying = false;
        }
        else if (ballPosition.Y + 2 * ball.GetBallRadius() > Window.WindowHeight)
        {
            Console.WriteLine("Opponent scores!");
            _isPlaying = false;
        }
    }
    private void SetObjectsStartPosition()
    {
        SetStartPaddlesPosition();
        SetStartBallPosition();
    }
    private void SetStartBallPosition()
    {
        ball.SetBallPosition(new Vector2f(Window.GetWindowCenter().X, Window.GetWindowCenter().Y));
    }
    private void SetStartPaddlesPosition()
    {
        ownPaddle.SetPosition(new Vector2f(Window.GetWindowCenter().X, Window.WindowHeight - ownPaddle.GetSize().Y));

        enemyPaddle.SetPosition(new Vector2f(Window.GetWindowCenter().X, enemyPaddle.GetSize().Y));
    }
}
