using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleHandler : MonoBehaviour
{

    public Text playerName;
    public Slider carSpeed;
    public Slider spawnSpeed;
    public Text currentCarSpeed;
    public Text currentSpawnSpeed;

    void Update()
    {
        float tempspeed = spawnSpeed.value * 5;
        currentCarSpeed.text = carSpeed.value.ToString() + "%";
        currentSpawnSpeed.text = tempspeed.ToString() + "%";
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("username", playerName.text);
        PlayerPrefs.SetFloat("carSpeed", carSpeed.value);
        PlayerPrefs.SetFloat("spawnSpeed", spawnSpeed.value);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
