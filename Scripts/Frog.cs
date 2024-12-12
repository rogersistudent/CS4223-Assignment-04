using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System;

public class Frog : MonoBehaviour {

	public Text playerName;

	public Rigidbody2D rb;

	public AudioSource audioSource;
	public AudioClip clip;

	private void Start()
    {
		playerName.text = PlayerPrefs.GetString("username");
		if(Score.currentLives != 3)
			audioSource.PlayOneShot(clip, 0.5f);
	}

    void Update () {

		if (Input.GetKeyDown(KeyCode.RightArrow))
			rb.MovePosition(rb.position + Vector2.right);
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			rb.MovePosition(rb.position + Vector2.left);
		else if (Input.GetKeyDown(KeyCode.UpArrow))
			rb.MovePosition(rb.position + Vector2.up);
		else if (Input.GetKeyDown(KeyCode.DownArrow))
			rb.MovePosition(rb.position + Vector2.down);

    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Car")
		{
            if(Score.CurrentScore > PlayerPrefs.GetInt("score"))
            {
                PlayerPrefs.SetInt("score", Score.CurrentScore);
            }
            Debug.Log("WE LOST!");
			Score.currentLives = Score.currentLives - 1;
			Score.CurrentScore = 0;
            if (Score.currentLives > 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else
            {
                AddNewScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
		}
	}
    public int num_scores = 10;

    public void AddNewScore()
    {
        string path = "Assets/Assets/scores.txt";
        string line;
        string[] fields;
        int scores_written = 0;
        string newName = "don't forget to input";
        string newScore = "999";
        bool newScoreWritten = false;
        string[] writeNames = new string[10];
        string[] writeScores = new string[10];

        newName = PlayerPrefs.GetString("username");
        newScore = PlayerPrefs.GetInt("score").ToString();

        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            fields = line.Split(',');
            if (!newScoreWritten && scores_written < num_scores) // if new score has not been written yet
            {
                //check if we need to write new higher score first
                if (Convert.ToInt32(newScore) > Convert.ToInt32(fields[1]))
                {
                    writeNames[scores_written] = newName;
                    writeScores[scores_written] = newScore;
                    newScoreWritten = true;
                    scores_written += 1;
                }

            }
            if (scores_written < num_scores) // we have not written enough lines yet
            {
                writeNames[scores_written] = fields[0];
                writeScores[scores_written] = fields[1];
                scores_written += 1;
            }
        }
        reader.Close();

        // now we have parallel arrays with names and scores to write
        StreamWriter writer = new StreamWriter(path);

        for (int x = 0; x < scores_written; x++)
        {
            writer.WriteLine(writeNames[x] + ',' + writeScores[x]);
        }
        writer.Close();

        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("scores");

    }
}
