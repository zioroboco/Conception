using UnityEngine;
using System.Collections.Generic;
using System;

public class Concept : MonoBehaviour
{
	[SerializeField] GameObject Label;

	Dictionary<string,string> feedback;

	bool rooted = false;
	TextMesh label;

	void Awake ()
	{
		label = GetComponentInChildren<TextMesh>();
	}

	void Root ()
	{
		if (rooted) return;
		// todo: root me
		if (!this.IsMapRoot())
			this.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	public GameObject ParentGameObject () {
		return this.transform.parent.gameObject;
	}

	public Concept ParentConcept () {
		return this.ParentGameObject().GetComponent<Concept>();
	}

	public bool IsMapRoot ()
	{
		return !ParentConcept();
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

	public void SetParent(Concept parentConcept)
	{
		this.transform.parent = parentConcept.transform;
	}
}