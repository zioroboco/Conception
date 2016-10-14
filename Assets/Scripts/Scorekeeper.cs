using UnityEngine;
using System.Collections.Generic;

public class Scorekeeper : MonoBehaviour {
	
	public static List<int> correct;
	public static List<int> attempts;
	
	public void AddScore(int numberCorrect, int numberOfAttempts)
	{
		correct.Add(numberCorrect);
		attempts.Add(numberOfAttempts);
	}
}
