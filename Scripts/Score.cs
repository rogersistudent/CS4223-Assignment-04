using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static int CurrentScore = 0;

	public Text scoreText;
	public Text lives;

	public static int currentLives = 3;

	void Start ()
	{
		scoreText.text = CurrentScore.ToString();
		lives.text = "Lives: " + currentLives.ToString();
	}

}
