using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// An audio source for the entire game
public class GameAudioSource : MonoBehaviour
{
    bool playing = false;
	// Awake is called before Start
	void Awake()
	{
        // make sure we only have one of this game object
        // in the game
        if (!AudioManager.Initialized)
        {
            // initialize audio manager and persist audio source across scenes
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioSource audioSourcelong = gameObject.AddComponent<AudioSource>();
            AudioSource[] sources = new AudioSource[2] { audioSource, audioSourcelong };
            AudioManager.Initialize(sources);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // duplicate game object, so destroy
            Destroy(gameObject);
        }
    }

    //Main menu music
    private void Update()
    {
        //In menus play music
        if((SceneManager.GetActiveScene().name == "MainMenu" ||
            SceneManager.GetActiveScene().name == "HelpMenu" ||
            SceneManager.GetActiveScene().name == "LevelsMenu" ||
            SceneManager.GetActiveScene().name == "EndMenu"))
        {
            //Start once
            if(!playing)
            {
                if (AudioManager.Initialized)
                    AudioManager.PlayLong(AudioClipName.MainMenuMusic);
                playing = true;
            }
        }
        //If not in menus, then stop
        else
        {
            if (AudioManager.Initialized)
                AudioManager.StopLong();
            playing = false;
        }
    }
}
