using SFML.Graphics;
using SFML.Window;

namespace AeroHockey;

public static class Window
{
    public const int WindowWidth = 800;
    public const int WindowHeight = 600;

    public static RenderWindow renderWindow;
    
    public static void DrawScene(IDrawable[] items)
    {
        renderWindow.Clear(Color.Black);

        foreach (IDrawable item in items)
        {
            renderWindow.Draw(item.GetDrawableObject());
        }
        renderWindow.Display();
    }
    public static void SetWindow()
    {
        renderWindow = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "Aero Hockey");
    }

    public static void DispatchEvents() => renderWindow.DispatchEvents();
    
}