using UnityEngine;
using UnityEngine.UI;

public class SourceButton : MonoBehaviour {
	
	[SerializeField]
	ConceptMap map;
	
	void Awake ()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(ClickSource);
	}
	
	void ClickSource ()
	{
		Application.OpenURL(map.SourceUrl);
	}
}
