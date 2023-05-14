using SFML.Graphics;
using SFML.System;

public class Paddle
{

    private Vector2u windowSize;

    private RectangleShape shape;
    private float paddleSpeed = 0.1f;

    public Paddle(Vector2u windowSize)
    {
        this.windowSize = windowSize;

        shape = new RectangleShape(new Vector2f(100, 20));
        shape.FillColor = Color.White;
        shape.Origin = new Vector2f(shape.Size.X / 2, shape.Size.Y / 2);

    }
    public void MovePaddle(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                if (shape.Position.X - shape.Size.X / 2 > 0)
                {
                    shape.Position -= new Vector2f(paddleSpeed, 0);
                }
                break;
            case Direction.Right:
                if (shape.Position.X + shape.Size.X / 2 < windowSize.X)
                {
                    shape.Position += new Vector2f(paddleSpeed, 0);
                }
                break;
        }
    }
    public void SetPosition(Vector2f newPosition) => shape.Position = newPosition;
    public Vector2f GetPosition() => shape.Position;

    public bool IfSmthHit(Vector2f objPosition) => shape.GetGlobalBounds().Contains(objPosition.X, objPosition.Y);
    public Vector2f GetSize() => shape.Size;
    public RectangleShape GetDrawableObject() => shape;

}
