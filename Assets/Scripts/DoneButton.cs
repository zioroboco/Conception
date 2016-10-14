using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoneButton : MonoBehaviour {
	
	[SerializeField]
	Scorekeeper scorekeeper;
	[SerializeField]
	Scoreboard scoreboard;
	
	void Awake ()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(ClickDone);
	}
	
	void ClickDone ()
	{
		scorekeeper.AddScore(scoreboard.correct, scoreboard.attempts);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
