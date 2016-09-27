using UnityEngine;
using System.Collections.Generic;

public class Concept : MonoBehaviour
{
	public Dictionary<string,string> feedback {get; private set;}
	TextMesh label;
	Concept parent;

	void Awake ()
	{
		label = GetComponentInChildren<TextMesh>();
	}

	public void SetFeedback(Dictionary<string, string> feedback)
	{
		this.feedback = feedback;
	}

	public string GetFeedback(string parent)
	{
		string message;
		feedback.TryGetValue(parent, out message);
		return message;
	}

    public void SetName(string name)
    {
		label.text = name;
		gameObject.name = "Concept (" + name + ")";
    }

	public void SetParentConcept(Concept parentConcept)
	{
		this.parent = parentConcept;
	}
}