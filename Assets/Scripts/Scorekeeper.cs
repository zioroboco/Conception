using UnityEngine;
using System.Collections.Generic;

public class Scorekeeper : MonoBehaviour {
	
	public static List<int> correct;
	public static List<int> attempts;
	
	public int GetCorrect (int i)
	{
		return correct[i];
	}
	
	public int GetAttempts (int i)
	{
		return attempts[i];
	}
	
	public int Count ()
	{
		if (correct != null)
			return correct.Count;
		else
			return 0;
	}
	
	public List<Vector2> CorrectAsSeries ()
	{
		List<Vector2> v2 = new List<Vector2>();
		v2.Add(new Vector2(0,0));
		for (int i = 1; i <= Count(); i++)
		{
			v2.Add(new Vector2(i, correct[i-1]));
		}
		return v2;
	}

	public List<Vector2> AttemptsAsSeries ()
	{
		List<Vector2> v2 = new List<Vector2>();
		v2.Add(new Vector2(0,0));
		for (int i = 1; i <= Count(); i++)
		{
			v2.Add(new Vector2(i, attempts[i-1]));
		}
		return v2;
	}
	
	public void AddScore (int numberCorrect, int numberOfAttempts)
	{
		if (correct == null)
			correct = new List<int>();
		if (attempts == null)
			attempts = new List<int>();
			
		correct.Add(numberCorrect);
		attempts.Add(numberOfAttempts);
	}
}
