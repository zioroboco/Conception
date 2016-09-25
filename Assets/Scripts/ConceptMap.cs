﻿using System;
using UnityEngine;
using LitJson;
using System.Collections.Generic;

public class ConceptMap : MonoBehaviour
{
	[SerializeField]
	TextAsset SourceFile;
	[SerializeField]
	GameObject ConceptPrefab;

	JsonData feedback;
	Concept root;

	void Awake ()
	{
		Load(SourceFile);
	}

	/// Build a map from the given source file.
	void Load (TextAsset source)
	{
		JsonData json = JsonMapper.ToObject(source.text);

		this.feedback = json["feedback"];

		this.root = Populate(null, json["map"][0]);
		this.root.transform.parent = this.transform;
	}

	/// Build a concept map from the given partial tree.
	Concept Populate (Concept parent, JsonData treeNode)
	{
		// Create the topmost new concept.
		string name = (string) treeNode["name"];
		Concept concept = CreateConcept(name);

		// Parent the new concept.
		if (parent)
		{
			concept.SetParent(parent);
		}

		// Recursively build the subtrees in "children", with this new concept as parent.
		if (!IsLeaf(treeNode))
		{
			JsonData subtrees = treeNode["children"];
			for (int i = 0; i < subtrees.Count; i++)
			{
				Populate(concept, subtrees[i]);
			}
		}

		return concept;
	}

	/// Returns true if this node has no array of children.
	bool IsLeaf (JsonData node)
	{
		return node.Count == 1;
	}

	/// Instantiate and initialise a new Concept prefab.
	Concept CreateConcept (string name)
	{
		// Instantiate and name the gameobject.
		GameObject conceptGameObject = Instantiate(ConceptPrefab);
		conceptGameObject.name = ("Concept (" + name + ")");

		// Set the name on the label.
		Concept concept = conceptGameObject.GetComponent<Concept>();
		concept.SetName(name);

		// Create a feedback dictionary from this concept's "categories" array.
		JsonData categoriesArray = CategoriesArrayForChild(name);
		concept.SetFeedback(BuildFeedbackDictionary(categoriesArray));

		return concept;
	}

	/// Searches the "feedback" list for an entry with the given child, and returns its "categories" array.
	JsonData CategoriesArrayForChild(string child)
	{	
		for (int i = 0; i < feedback.Count; i++)
		{
			JsonData entry = feedback[i];
			if ((string) entry["child"] == child)
			{
				return entry["categories"];
			}
		}

		return null;
	}

	/// Returns a dictionary of parents to feedback messages from the given "categories" array.
	Dictionary<string,string> BuildFeedbackDictionary (JsonData feedbackArray)
	{
		Dictionary<string,string> feedbackDictionary = new Dictionary<string,string>();

		if (feedbackArray != null)
		{
			for (int i = 0; i < feedbackArray.Count; i++)
			{
				JsonData entry = feedbackArray[i];
				feedbackDictionary.Add((string) entry[0], (string) entry[1]);
			}
		}

		return feedbackDictionary;
	}
}