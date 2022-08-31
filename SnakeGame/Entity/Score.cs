namespace SnakeGame.Entity;
public class Score
{
    private int currentScore;

    public Score()
    {
        currentScore = 0;
    }

    public void Increase() => currentScore += 10;
    public int GetScore() => currentScore;
}