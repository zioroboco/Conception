using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour {
    
    [SerializeField]
    GameObject Tick;

    [SerializeField]
    GameObject Cross;
    
    const string DEFAULT_FEEDBACK_CORRECT = "That's right!";
    const string DEFAULT_FEEDBACK_INCORRECT = "Nope. Try again!";
    
    public void DisplayFeedback(Concept parent, Concept child)
    {
        bool correct = child.parent == parent;
        Tick.SetActive(correct);
        Cross.SetActive(!correct);
        string feedback = child.GetFeedback(parent.name);
        if (feedback == null)
        {
            feedback = (correct)? DEFAULT_FEEDBACK_CORRECT : DEFAULT_FEEDBACK_INCORRECT;
        }
        GetComponentInChildren<Text>().text = feedback;
    }
    
    public void ResetFeedback()
    {
        Tick.SetActive(false);
        Cross.SetActive(false);
        GetComponentInChildren<Text>().text = "";
    }
}