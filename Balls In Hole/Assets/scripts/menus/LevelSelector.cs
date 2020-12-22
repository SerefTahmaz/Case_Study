using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

// Listens for the OnClick events for the main menu buttons
public class LevelSelector : MonoBehaviour
{
    //Hold ui for loading screen
    public GameObject loadingScreen;
    public Slider slider;

    // Handles the on clicks from the buttons
    void Update()
    {
        //Go to Level-1 if clicks level-1 button
        if (CrossPlatformInputManager.GetButtonDown("Level-1"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_1");
        }
        //Go to Level-2 if clicks level-2 button
        if (CrossPlatformInputManager.GetButtonDown("Level-2"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_2");
        }
        //Go to Level-3 if clicks level-3 button
        if (CrossPlatformInputManager.GetButtonDown("Level-3"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_3");
        }
        //Go to Level-4 if clicks level-4 button
        if (CrossPlatformInputManager.GetButtonDown("Level-4"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_4");
        }
        //Go to Level-5 if clicks level-5 button
        if (CrossPlatformInputManager.GetButtonDown("Level-5"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_5");
        }
        //Go to Level-6 if clicks level-6 button
        if (CrossPlatformInputManager.GetButtonDown("Level-6"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_6");
        }
        //Go to Level-7 if clicks level-7 button
        if (CrossPlatformInputManager.GetButtonDown("Level-7"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_7");
        }
        //Go to Level-8 if clicks level-8 button
        if (CrossPlatformInputManager.GetButtonDown("Level-8"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_8");
        }
        //Go to Level-9 if clicks level-9 button
        if (CrossPlatformInputManager.GetButtonDown("Level-9"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load level with loading screen
            LoadLevel("Level_9");
        }
        //Quit app if clicks quit button
        if (CrossPlatformInputManager.GetButtonDown("Quit"))
        {
            //Play click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            //Load main menu with loading screen
            LoadLevel("MainMenu");
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
        while (!operation.isDone)
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
