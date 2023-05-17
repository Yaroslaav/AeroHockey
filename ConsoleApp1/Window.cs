using SFML.Graphics;
using SFML.Window;

namespace AeroHockey;

public static class Window
{
    public const int windowWidth = 800;
    public const int windowHeight = 600;

    public static RenderWindow renderWindow;
    
    public static void DrawScene(IDrawable[] items)
    {
        renderWindow.Clear(Color.Black);

        foreach (IDrawable item in items)
        {
            renderWindow.Draw(item.GetDrawaleObject());
        }
        renderWindow.Display();
    }
    public static void SetWindow()
    {
        renderWindow = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Aero Hockey");
    }

    public static void DispatchEvents() => renderWindow.DispatchEvents();
    
}