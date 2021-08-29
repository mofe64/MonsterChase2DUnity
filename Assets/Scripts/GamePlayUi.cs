using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUi : MonoBehaviour
{
    public void restart()
    {
        // SceneManager.LoadScene("SampleScene"); one way of reloading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void home()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
