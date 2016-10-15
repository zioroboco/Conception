using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour {
	
	void Awake ()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(ClickRetry);
	}
	
	void ClickRetry ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
