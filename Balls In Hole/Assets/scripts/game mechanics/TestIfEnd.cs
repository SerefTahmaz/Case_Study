using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Test if player dies, then go to gameover menu
public class TestIfEnd : MonoBehaviour
{

    //Hold player
    GameObject player;
    //Instantiate once gameover menu so we need a bool value to check
    bool gameOverMenu =true;
    private void Start()
    {
        
        //Get player
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //Check player is not alive
        if(player == null)
        {
            //Instantiate once gameover menu
            if (gameOverMenu)
            {
                //Going to gameover menu
                MenuManager.GoToMenu(MenuName.GameOver);
                gameOverMenu = false;
            }
        }

    }
}
