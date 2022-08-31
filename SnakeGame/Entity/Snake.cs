using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Entity;
public class Snake
{
    private int bodyLength;
    private List<SnakeBody> bodyParts;
    private Direction direction;

    public Snake()
    {
        bodyLength = 30;
        bodyParts = new List<SnakeBody>();
        direction = Direction.RIGHT;
    }

    public void Create()
    {
        //Head
        bodyParts.Add(new SnakeBody(30, 180));

        //Body
        for (int i = 0; i < 3; i++)
        {
            bodyParts.Add(new SnakeBody(bodyParts[0].XPosition - (bodyLength * i), bodyParts[0].YPosition - bodyLength));
        }
    }

    public List<SnakeBody> GetBodyParts() => bodyParts;

    public void Move()
    {
        bodyParts[0].LastXPosition = bodyParts[0].XPosition;
        bodyParts[0].LastYPosition = bodyParts[0].YPosition;

        if (direction is Direction.RIGHT)
        {
            bodyParts[0].XPosition += bodyLength;
        }
        else if (direction is Direction.LEFT)
        {
            bodyParts[0].XPosition -= bodyLength;
        }
        else if (direction is Direction.UP)
        {
            bodyParts[0].YPosition -= bodyLength;
        }
        else if (direction is Direction.DOWN)
        {
            bodyParts[0].YPosition += bodyLength;
        }

        for (int i = 1; i < bodyParts.Count; i++)
        {
            bodyParts[i].LastYPosition = bodyParts[i].YPosition;
            bodyParts[i].LastXPosition = bodyParts[i].XPosition;
            bodyParts[i].XPosition = bodyParts[i - 1].LastXPosition;
            bodyParts[i].YPosition = bodyParts[i - 1].LastYPosition;
        }
    }

    public void SetDirection(Direction direction) => this.direction = direction;

    public Direction GetDirection() => this.direction;

    public Rectangle GetHead() => new Rectangle(bodyParts[0].XPosition, bodyParts[0].YPosition, 30, 30);

    public void Grow()
    {
        var tail = bodyParts.Last();
        bodyParts.Add(new SnakeBody(tail.XPosition, tail.YPosition));
    }

    public bool Collided()
    {
        for (int i = 1; i < bodyParts.Count; i++)
        {
            bool collidedWithItSelf = bodyParts[0].XPosition == bodyParts[i].XPosition
                && bodyParts[0].YPosition == bodyParts[i].YPosition;

            bool collidedWithWall = bodyParts[0].XPosition < 0 || bodyParts[0].XPosition > 1200
                || bodyParts[0].YPosition < 0 || bodyParts[0].YPosition > 1200;

            if (collidedWithItSelf || collidedWithWall) return true;
        }

        return false;
    }
}