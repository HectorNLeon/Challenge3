using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public int m;
    public void LoadByIndex(int sceneIndex)
    {
        ModeData.Mode = m;
        SceneManager.LoadScene(sceneIndex);
    }
}