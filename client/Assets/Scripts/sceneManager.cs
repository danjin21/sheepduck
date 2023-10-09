using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMain()
    {
        //SceneManager.LoadScene("Main");

        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }

    public void GoToStart()
    {
        SceneManager.LoadScene("Start");

    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
