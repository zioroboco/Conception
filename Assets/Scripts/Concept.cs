using UnityEngine;
using System.Collections;

public class Concept : MonoBehaviour
{
	[SerializeField] GameObject Label;

	Concept parentConcept;
	
	bool rooted = false;

	void Awake ()
	{
	}

	void Root ()
	{
		if (rooted) return;
	}
}