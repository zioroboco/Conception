using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoneButton : MonoBehaviour {
	
	void Awake ()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(ReloadScene);
	}
	
	void ReloadScene ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
