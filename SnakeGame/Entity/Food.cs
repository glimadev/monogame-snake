using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace SnakeGame.Entity;

public class Food
{
    Random r = new Random();
    Rectangle spritePosition = new Rectangle(30, 0, 30, 30);
    Rectangle position;

    public Food()
    {

    }

    public void Create(Snake snake)
    {
        int x;
        int y;

        while (true)
        {
            x = r.Next(1, 1200 / 30) * 30;
            y = r.Next(1, 1200 / 30) * 30;

            if(!snake.GetBodyParts().Any(part => part.XPosition == x && part.YPosition == y))
            {
                position = new Rectangle(x, y, 30, 30);

                break;
            }
        }
    }

    public Rectangle GetPosition() => position;

    public Rectangle GetSpritePosition() => spritePosition;

    public bool WasEated(Rectangle head) => head.Contains(position);
}