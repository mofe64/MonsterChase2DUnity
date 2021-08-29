using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField]
    private GameObject[] players; //initialize via unity inspector
    private int _charIndex;
    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//ensures this game object when moving to a new scence (which is defaul behavior)
            //dontDestroyOnLoad creates a wrapper gameObject around whatever game objs we put in it as we are doing above, this wrapper game obj is transferred to the next scene
            //so whatever game objs are inside the wrapper are accessible
        }
        else { Destroy(gameObject); }//ensures we only ever have one instance of game obj
    }

    private void OnEnable() //OnEnable is the recommended function to subscribe to events, here we are subing to the sceneloaded event 
    //our OnLevelFinisedLoading  function has been tied to the event and will be called when the event executes
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()//to prevent memory leaks we unsub from event as well
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            Instantiate(players[CharIndex]);
        }
    }
}
