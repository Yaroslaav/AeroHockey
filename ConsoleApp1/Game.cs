using SFML.System;
using SFML.Window;

public enum Direction 
{
    None,
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

    private List<GameObject> _gameObjects = new ();

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

            Input.CheckMovePaddleInput();
            
            UpdateGameObjects();

            ownPaddle.TryMoveOwnPaddle();
            enemyPaddle.TryMoveAIPaddle(ball);

            ball.TryBounceFromPaddle(ownPaddle, Position.Bot);
            ball.TryBounceFromPaddle(enemyPaddle, Position.Top);

            CheckIfSomeoneWon();
            
            Window.DrawScene(_gameObjects.ToArray());
            
        }

        Stop();
    }

    private void Stop()
    {
        _gameObjects = new ();
        Window.Close();
        Start();
    }

    private void UpdateGameObjects()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Update();
        }
    }

    private void SetPaddles()
    {
        ownPaddle = new Paddle(Window.renderWindow.Size);
        _gameObjects.Add(ownPaddle);
        
        enemyPaddle = new Paddle(Window.renderWindow.Size);
        _gameObjects.Add(enemyPaddle);
    }
    private void SetBall()
    {
        ball = new Ball(Window.renderWindow.Size);
        _gameObjects.Add(ball);
    } 
    private void WindowClosed(object? sender, EventArgs e) => _isPlaying = false;
    
    /*private void MoveInputProcessing()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            ownPaddle.MovePaddle(Direction.Left);
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            ownPaddle.MovePaddle(Direction.Right);
        }
    }*/
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
