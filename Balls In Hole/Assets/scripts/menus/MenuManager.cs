using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Manages navigation through the menu system
public static class MenuManager
{

    // Goes to the menu with the given name
    public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.Help:

                // go to HelpMenu scene
                SceneManager.LoadScene("HelpMenu");
                break;
            case MenuName.Main:

                // go to MainMenu scene
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.GameOver:
                
                // instantiate prefab
                Object.Instantiate(Resources.Load("GameOverMenu"));
                break;
            case MenuName.Quit:

                // instantiate prefab
                Object.Instantiate(Resources.Load("QuitMenu"));
                break;
        }
    }
}
