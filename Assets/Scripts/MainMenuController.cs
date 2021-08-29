using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        //Get name of clicked button
        string clickedButtonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;//when creating our buttons we set names to 0 and 1 so we can easily parse them to our index
        int selectedCharIndex = int.Parse(clickedButtonName);
        GameManager.instance.CharIndex = selectedCharIndex;
        SceneManager.LoadScene("SampleScene");
    }
}
