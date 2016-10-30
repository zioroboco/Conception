using System.IO;
using UnityEngine;
using LitJson;
using System.Collections.Generic;

public class ConceptMap : MonoBehaviour
{
	[SerializeField]
	GameObject ConceptPrefab;
	[SerializeField]
	Palette palette;
	
	public string SourceUrl;

	public int Count { get; private set; }

	public int ConceptCount;

	JsonData feedback;
	public Concept root;

	void Awake ()
	{
		string inputFilePath = Application.dataPath + "/input.json";
		string inputFileContents = new StreamReader(inputFilePath).ReadToEnd();
		
		Load(inputFileContents);
		palette.Arrange();
	}

	/// Build a map from the given source file.
	void Load (string source)
	{
		JsonData json = JsonMapper.ToObject(source);

		this.feedback = json["feedback"];

		this.root = Populate(null, json["map"][0]);
		
		this.SourceUrl = (string) json["source"];

		this.root.transform.parent = this.transform;
		this.root.transform.position += Vector3.up * Camera.main.orthographicSize * 0.5f;
		palette.ConceptTransforms.RemoveAt(0);
		
		this.root.transform.parent = this.transform;
		
		this.root.rooted = true;
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
			concept.SetParentConcept(parent);
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

		Count++;
		ConceptCount++;
		return concept;
	}

	/// Returns true if this node has no array of children.
	bool IsLeaf (JsonData node)
	{
		return node.Count == 1;
	}

	/// Instantiate and initialise a new concept.
	Concept CreateConcept (string name)
	{
		// Instantiate and name the concept.
		GameObject conceptGameObject = Instantiate(ConceptPrefab);
		Concept concept = conceptGameObject.GetComponent<Concept>();
		concept.SetName(name);

		// Create a feedback dictionary from this concept's "categories" array.
		JsonData categoriesArray = CategoriesArrayForChild(name);
		concept.SetFeedback(BuildFeedbackDictionary(categoriesArray));

		conceptGameObject.transform.SetParent(palette.gameObject.transform);
		palette.ConceptTransforms.Add(conceptGameObject.transform);

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
				
				if (entry[0].GetJsonType() == JsonType.Array)
				{
					for (int j = 0; j < entry[0].Count; j++)
					{
						string feedbackStringParent = (string) entry[0][j];
						string feedbackStringMessage = (string) entry[1];
						feedbackDictionary.Add(feedbackStringParent.ToLower(), feedbackStringMessage);
					}
				}
				else
				{
					string feedbackStringParent = (string) entry[0];
					string feedbackStringMessage = (string) entry[1];
					feedbackDictionary.Add(feedbackStringParent.ToLower(), feedbackStringMessage);
				}
			}
		}
		return feedbackDictionary;
	}
}