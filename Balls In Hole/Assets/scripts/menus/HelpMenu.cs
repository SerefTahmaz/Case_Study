using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// Actions for the button clicks for the help menu button
public class HelpMenu : MonoBehaviour
{
    // Handles the on click event from the back button
    void Update()
    {
        // Check if clicks back button
        if(CrossPlatformInputManager.GetButtonDown("Back"))
        {
            // Click sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.MenuButtonClick);
            // Go to main menu
            MenuManager.GoToMenu(MenuName.Main);
        }
        
    }
} 
