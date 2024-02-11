using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    string levelName;
    public int sceneIndex;
    public enum SceneName
    {
        Restart,
        NextIndex,
        BlackHolesTest,
        MainMenu,
        JP3Graveyard,
        GameplayDemo2,
        GameplayDemo3,
        GameplayDemo4,
        GameplayDemo5,
        GameplayDemo6,
        Menu,
        Gameplay1,
        MenuSettings,
        Quit,
        LevelProgressionTest,
        EndScreen,


    }
    public SceneName sceneName;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Start is called before the first frame update
    public void ChangeScene()
    {

        switch (sceneName)
        {
            case SceneName.Restart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case SceneName.NextIndex:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case SceneName.BlackHolesTest:
                levelName = "BlackHolesTest";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.MainMenu:
                levelName = "MainMenu";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.JP3Graveyard:
                levelName = "JP3Graveyard";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.GameplayDemo2:
                levelName = "GameplayDemo 2";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.GameplayDemo3:
                levelName = "GameplayDemo 3";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.GameplayDemo4:
                levelName = "GameplayDemo 4";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.GameplayDemo5:
                levelName = "GameplayDemo 5";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.GameplayDemo6:
                levelName = "GameplayDemo 6";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.Menu:
                levelName = "Menu";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.Gameplay1:
                levelName = "Gameplay1";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.MenuSettings:
                levelName = "MenuSettings";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.LevelProgressionTest:
                levelName = "LevelProgressionTest";
                SceneManager.LoadScene(levelName);
                break;
            case SceneName.Quit:
                Application.Quit();
                break;
            case SceneName.EndScreen:
                levelName = "EndScreen"; 
                SceneManager.LoadScene(levelName);
                break;
        }
    }
    public void GoToSceneByString(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
        
    }

}
