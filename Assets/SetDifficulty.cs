using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SetDifficulty : MonoBehaviour {

    public void Difficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        SceneManager.LoadScene(1);
    }
}
