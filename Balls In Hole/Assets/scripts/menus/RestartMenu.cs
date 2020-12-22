using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

// Pauses and unpauses the game. Listens for the clicks
// actions for the pause menu buttons
public class RestartMenu : MonoBehaviour
{

    //Canvas
    //GameObject canvas;
    void Start()
    {
        //Current Canvas
        //canvas = GameObject.Find("Canvas");
        //Set child of canvas so we preserve screen alignment
        //this.transform.SetParent(canvas.transform);
        // pause the game when added to the scene
        Time.timeScale = 0;
    }

    void Update()
    {
        //Check restart game button
        if (CrossPlatformInputManager.GetButtonDown("Restart"))
        {
            // unpause game and destroy menu
            Time.timeScale = 1;

            //Restart Scene
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

            //AudioManager.Play(AudioClipName.MenuButtonClick);
            Destroy(gameObject);
        }

        //Check quit game button
        if (CrossPlatformInputManager.GetButtonDown("QuitToMenu"))
        {
            // unpause game, destroy menu, and go to main menu
            Time.timeScale = 1;
            //AudioManager.Play(AudioClipName.MenuButtonClick);
            
            //Go to main menu
            MenuManager.GoToMenu(MenuName.Main);
            Destroy(gameObject);
        }
    }
}
