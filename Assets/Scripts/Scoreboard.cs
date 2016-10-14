using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {
    
    [SerializeField]
    Text ScoreDisplay;
    
    public int correct { get; private set; }
    public int attempts { get; private set; }
    
    public void IncrementCorrect ()
    {
        correct++;
    }

    public void IncrementAttempts ()
    {
        attempts++;
    }
    
    public void UpdateScoreDisplay()
    {
        ScoreDisplay.text = FormatScore(correct, attempts);
    }
    
    private string FormatScore(int correct, int attempts)
    {
        return correct.ToString() + "/" + attempts.ToString();
    }
}