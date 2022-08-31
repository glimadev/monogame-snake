namespace SnakeGame.Entity;

public class SnakeBody
{
    public SnakeBody(int x, int y)
    {
        XPosition = x;
        YPosition = y;
    }

    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int LastXPosition { get; set; }
    public int LastYPosition { get; set; }
}