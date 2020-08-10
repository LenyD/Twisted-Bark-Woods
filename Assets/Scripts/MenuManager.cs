using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    private SceneManager sceneManager = new SceneManager();
    private void Awake() {
        instance = this;
    }
    public void LoadGame(){
        SceneManager.LoadScene("GymSim");
    }
    public void GameOver(){
        SceneManager.LoadScene("GameOver");
    }
    public void ExitGame(){
        Application.Quit();
    }
    public void Ending(){
        SceneManager.LoadScene("Ending");
    }

}
