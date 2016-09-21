using System;
using UnityEngine;
using LitJson;

public class ConceptMap : MonoBehaviour
{
	[SerializeField] TextAsset SourceFile;
	[SerializeField] GameObject ConceptPrefab;

	void Awake ()
	{
		JsonData data = JsonMapper.ToObject(SourceFile.text);
	}
}