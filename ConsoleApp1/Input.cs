using SFML.Window;

public static class Input
{
    public static Direction paddleLastMoveDirection = Direction.None;

    public static void CheckMovePaddleInput()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            paddleLastMoveDirection = Direction.Right;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            paddleLastMoveDirection = Direction.Left;
        }
    }
}