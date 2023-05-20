using SFML.System;

public enum Position
{
    Top, 
    Bot, 
    Right,
    Left,
}

public static class BallMovementExtension
{
    public static void TryBounceFromPaddle(this Ball ball, Paddle paddle, Position paddlePosition)
    {
        Vector2f ballPosition = ball.GetBallPosition();
        float ballRadius = ball.GetBallRadius();
        bool directionIsRightForBounce = false;

        switch (paddlePosition)
        {
            case Position.Top:
                ballPosition.Y -= ballRadius;
                directionIsRightForBounce = ball.GetBallDirection().Y < 0;
                break;
            case Position.Bot:
                ballPosition.Y += ballRadius;
                directionIsRightForBounce = ball.GetBallDirection().Y > 0;
                break;
            case Position.Right:
                ballPosition.X += ballRadius;
                directionIsRightForBounce = ball.GetBallDirection().X > 0;
                break;
            case Position.Left:
                ballPosition.X -= ballRadius;
                directionIsRightForBounce = ball.GetBallDirection().X < 0;
                break;
        }
        
        if(!directionIsRightForBounce)
            return;
        
        if(paddle.IfSmthHit(new Vector2f(ballPosition.X, ballPosition.Y)))
        {
            ball.OnBounce(Direction.Vertical);
        }
    }

}