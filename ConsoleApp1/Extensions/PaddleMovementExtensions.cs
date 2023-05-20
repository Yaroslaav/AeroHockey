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

}