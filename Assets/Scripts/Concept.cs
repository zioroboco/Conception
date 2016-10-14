using UnityEngine;
using System.Collections.Generic;

public class Concept : MonoBehaviour
{
	public Dictionary<string,string> feedback {get; private set;}
	TextMesh label;
	
	[SerializeField]
	GameObject HighlightPrefab;
	
	GameObject highlight;
	
	public Concept parent;
	public new string name;
	public bool rooted = false;

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
	
	public void ApplyHighlight()
	{
		highlight = Instantiate(HighlightPrefab);
		highlight.transform.position = transform.position;
		highlight.transform.parent = transform.parent;
	}
	
	public void ClearHighlight()
	{
		if (highlight != null)
		{
			Destroy(highlight);
		}
	}
}