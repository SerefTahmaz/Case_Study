using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

// Actions for the main menu buttons
public class MainMenu : MonoBehaviour
{
    //Hold ui for loading screen
    public GameObject loadingScreen;
    public Slider slider;

   
    // Handles the on clicks from the buttons
    void Update()
    {
        //Go to level menu
        if (CrossPlatformInputManager.GetButtonDown("Play"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level menu with loading screen
            LoadLevel("LevelsMenu");
        }
        //Go to help menu if clicks help button
        if (CrossPlatformInputManager.GetButtonDown("Help"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Go to help menu
            MenuManager.GoToMenu(MenuName.Help);
        }
        //Quit app if clicks quit button
        if (CrossPlatformInputManager.GetButtonDown("Quit"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Close app
            Application.Quit();
        }

    }


    //Loading screen
    public void LoadLevel(string sceneIndex)
    {
        //Start coroutine
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    //Coroutine paralel with level menu loading so we can see much left to load
    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        //Async to get info about loading
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        //Set background more white, therefore loading bar is more distinct
        loadingScreen.SetActive(true);

        //Loop when loading level menu
        while(!operation.isDone)
        {
            //operation.progress return value between 0 and .9f
            //so dividing it with .9f
            //make it between 0 and 1
            //that way progress is can be seen in slider
            float progress = Mathf.Clamp01(operation.progress / .9f);

            //assign progress value to slider so we can see it
            slider.value = progress;
            //Debuging propose
            Debug.Log(slider.value);
            yield return null;
        }
    }


    
} 
