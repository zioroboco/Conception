using UnityEngine;
using System.Collections.Generic;

public class Concept : MonoBehaviour
{
	public Dictionary<string,string> feedback {get; private set;}
	TextMesh label;
	
	public Concept parent;
	public new string name;

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
		this.name = name;
		gameObject.name = "Concept (" + name + ")";
    }

	public void SetParentConcept(Concept parentConcept)
	{
		this.parent = parentConcept;
	}
}