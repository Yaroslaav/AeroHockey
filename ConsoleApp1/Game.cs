﻿using SFML.System;
using SFML.Window;
using Window = AeroHockey.Window;

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

    readonly List<IDrawable> _drawableObjects = new ();

    public void Start()
    {
        Window.SetWindow();
        Window.renderWindow.Closed += WindowClosed;
        
        SetPaddles(); 
        SetBall();
        SetObjectsStartPosition();
        
        _isPlaying = true;
        GameLoop();
    }
    private void GameLoop()
    {
        while(_isPlaying)
        {
            Window.DispatchEvents();

            MoveInputProcessing();
            TryMoveOpponentPaddle();

            ball.Move();
            TryBounceFromPaddle();
            CheckIfSomeoneWon();

            Window.DrawScene(_drawableObjects.ToArray());
            
        }
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
        ball.SetBallPosition(new Vector2f(Window.WindowWidth / 2 - ballRadius, Window.WindowHeight / 2 - ballRadius));
    }
    public void SetStartPaddlesPosition()
    {
        Vector2f paddleSize = ownPaddle.GetSize();
        ownPaddle.SetPosition(new Vector2f(Window.WindowWidth / 2 - paddleSize.X / 2, Window.WindowHeight - paddleSize.Y));

        paddleSize = enemyPaddle.GetSize();
        enemyPaddle.SetPosition(new Vector2f(Window.WindowWidth / 2 - paddleSize.X / 2, paddleSize.Y));
    }


}
