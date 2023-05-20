using SFML.System;

public static class PaddleMovementExtensions
{
    public static void TryMoveAIPaddle(this Paddle paddle, Ball ball)
    {
        Vector2f ballPosition = ball.GetBallPosition();
        Vector2f paddlePosition = paddle.GetPosition();

        if (ballPosition.X < paddlePosition.X)
        {
            paddle.MovePaddle(Direction.Left);
        }
        else if (ballPosition.X > paddlePosition.X)
        {
            paddle.MovePaddle(Direction.Right);
        }
    }

    public static void TryMoveOwnPaddle(this Paddle paddle)
    {
        switch (Input.paddleLastMoveDirection)
        {
            case Direction.Right:
                paddle.MovePaddle(Direction.Right);
                Input.paddleLastMoveDirection = Direction.None;
                break;
            case Direction.Left:
                paddle.MovePaddle(Direction.Left);
                Input.paddleLastMoveDirection = Direction.None;
                break;
        }
    }

}