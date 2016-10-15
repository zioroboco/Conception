using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoneButton : MonoBehaviour {
	
	[SerializeField]
	Scorekeeper scorekeeper;
	[SerializeField]
	Scoreboard scoreboard;
	[SerializeField]
	GameObject progressWindow;
	[SerializeField]
	WMG_Axis_Graph progressGraph;
	[SerializeField]
	Text progressText;
	[SerializeField]
	ConceptMap map;
	
	public WMG_Series correctSeries;
	public WMG_Series attemptsSeries;
	
	void Awake ()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(ClickDone);
	}
	
	void ClickDone ()
	{
		progressText.text = GenerateProgressText();
		
		scorekeeper.AddScore(scoreboard.correct, scoreboard.attempts);

		progressWindow.SetActive(true);
		
		attemptsSeries = progressGraph.addSeries();
		attemptsSeries.pointValues.SetList(scorekeeper.AttemptsAsSeries());
		attemptsSeries.lineColor = Color.red;
		attemptsSeries.seriesName = "attempted connections";
		
		correctSeries = progressGraph.addSeries();
		correctSeries.pointValues.SetList(scorekeeper.CorrectAsSeries());
		correctSeries.lineColor = Color.green;
		correctSeries.seriesName = "correct connections";
	}
	
	string GenerateProgressText()
	{
		return "You scored " + CalculatePercentile() + "%!";
	}
	
	float CalculatePercentile()
	{
		float correctness = (float)scoreboard.correct / (float)scoreboard.attempts;
		float completeness = (float)scoreboard.correct / (float)map.ConceptCount;
		return correctness * completeness * 100f;
	}
}
