using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Player and balls control script
public class SwipeTest : MonoBehaviour
{
    //Getting swipe
    public Swipe swipeControls;
    public Transform player;
    [HideInInspector]
    //Main Navigation Position
    public Vector3 desiredPosition;


    //For no movement during swiping
    [HideInInspector]
    public bool moving = true;
    //For calculating nearest grid position
    public GridSpawn gridSpawn;


    //Which direction we are going for balls rotation
    [HideInInspector]
    public Direction direction;

    //Waiting for animation at start ends
    bool controlStart = false;

    //Putting ball in hole when one ball enter the hole
    public GameObject ballInHole;

    //Starting animation for balls at start
    Vector3 initialSize;
    //Lerping variable
    float t = 0;
    private void Start()
    {
        //At start no movement
        desiredPosition = player.transform.position;

        //At start we wait for level cubes animation ends
        Invoke("ControlStart", 0.3f + gridSpawn.gridX / 64f + gridSpawn.gridZ / 17f);

        //If it is a ball, then up to down animation at start
        if(player.name != "Player")
        player.transform.position += new Vector3(0, 15f, 0);
        
    }
    private void Update()
    {
        //If it is a ball, then up to down animation at start via lerping
        if (!controlStart && player.name != "Player")
        {
            player.transform.position = Vector3.Lerp(desiredPosition + new Vector3(0, 15f, 0), desiredPosition, t);
            t += Time.deltaTime / 1.5f;
            return;
        }
        

        //If it stops and start animation ends, then you can swipe
        if (moving && controlStart)
        {
            if (swipeControls.SwipeLeft)
            {
                //Player goes all along so I put a big number
                desiredPosition += Vector3.left * 100;
                //Player not swipe during movement
                moving = false;
                //Direction for balls rotation
                direction = Direction.Left;

                if (AudioManager.Initialized)
                    AudioManager.Play(AudioClipName.PlayerSlide);
            }
            if (swipeControls.SwipeRight)
            {
                //Player goes all along so I put a big number
                desiredPosition += Vector3.right * 100;
                //Player not swipe during movement
                moving = false;
                //Direction for balls rotation
                direction = Direction.Right;

                if (AudioManager.Initialized)
                    AudioManager.Play(AudioClipName.PlayerSlide);
            }
            if (swipeControls.SwipeUp)
            {
                //Player goes all along so I put a big number
                desiredPosition += Vector3.forward * 100;
                //Player not swipe during movement
                moving = false;
                //Direction for balls rotation
                direction = Direction.Up;

                if (AudioManager.Initialized)
                    AudioManager.Play(AudioClipName.PlayerSlide);
            }
            if (swipeControls.SwipeDown)
            {
                //Player goes all along so I put a big number
                desiredPosition += Vector3.back * 100;
                //Player not swipe during movement
                moving = false;
                //Direction for balls rotation
                direction = Direction.Down;

                if (AudioManager.Initialized)
                    AudioManager.Play(AudioClipName.PlayerSlide);
            }
                
        }

        
        
        //Moving desired position
        player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition, 15f * Time.deltaTime);

        

        //Detecting if there is anything in our route
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, desiredPosition.normalized, out hit, 100))
        {
            //If distance is close, then take action
            if(Vector3.Distance(hit.transform.position, player.transform.position)< 0.9f)
            {
                //Actions
                Choice(hit);
                
            }
        }
        

        
    }

    public void Stop()
    {
        //Player stops, so we can give permission to swipe
        moving = true;
        //We want closest point, so we start with farthest place to comparision. Simple finding min loop
        Vector3 temp = Vector3.positiveInfinity;
        for (int x = 0; x < gridSpawn.gridX; x++)
        {
            for (int z = 0; z < gridSpawn.gridZ; z++)
            {
                //Getting grid pos
                Vector3 spawnPosition = new Vector3(x * gridSpawn.gridSpacingOffset, 1, z * gridSpawn.gridSpacingOffset) + gridSpawn.gridOrigin;
                //If closer, then assign
                if (Vector3.Distance(spawnPosition, player.transform.position) < Vector3.Distance(temp, player.transform.position))
                {
                    temp = spawnPosition;
                }
            }
        }
        //Stops at nearnest grid pos for perfect alignment
        desiredPosition = temp;
    }


    //Taking action when collide
    void Choice(RaycastHit hit)
    {
        //If player hits a ball, players tag gameobjects are player and balls
        if (player.name == "Player" && hit.transform.gameObject.tag == "Players")
        {
            //Play ball collected sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.BallCollected);
            //Checks if all balls are taken
            int numberOfBalls = GameObject.FindGameObjectsWithTag("Players").Length - 1;
            //All balls are taken, then go next level, and wait for player go to the balls position
            if (numberOfBalls == 1)
                Invoke("NextLevel", 0.1f);

            //Putting a ball in hole
            ballInHole.SetActive(true);

            //Destroy ball SwipeTest gameobject
            Destroy(hit.transform.gameObject);
            //Destroy ball itself
            Destroy(hit.transform.gameObject.GetComponent<ItsSwipe>().SwipeTest.gameObject);
            return;
        }
        //If ball hits player
        else if (LayerMask.LayerToName(player.gameObject.layer) == "Ball" && hit.transform.gameObject.name == "Player")
        {
            //Play ball collected sound
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.BallCollected);
            //Checks if all balls are taken
            int numberOfBalls = GameObject.FindGameObjectsWithTag("Players").Length - 1;
            //All balls are taken, then go next level
            if (numberOfBalls == 1)
                NextLevel();

            //Putting a ball in hole
            hit.transform.gameObject.GetComponent<ItsSwipe>().SwipeTest.ballInHole.SetActive(true);
            //Destroy ball SwipeTest gameobject
            Destroy(player.gameObject);
            //Destroy ball itself
            Destroy(this.gameObject);
            return;
        }
        //Ball hits another ball
        else if (LayerMask.LayerToName(hit.transform.gameObject.layer) == "Ball")
        {
            Stop();
            return;
        }
        //Player or ball hit a level cubes
        else if (hit.transform.tag == "Level")
        {
            Stop();
        }
        //Hits LightObstacle
        else if (hit.transform.gameObject.tag == "LightObstacle")
        {
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.LightObstacleImpact);
            //If player hits, stops player
            if (player.name == "Player")
            {
                Stop();
            }
            //If ball hits, stop ball and destroy lightobstacle
            else
            {
                Stop();
                Destroy(hit.transform.gameObject);
            }
        }
        //Hits trap
        else if (hit.transform.gameObject.tag == "Trap")
        {
            //If player hits, stops player
            if (player.name == "Player")
            {
                Stop();
            }
            //If ball hits, then go to gameover menu
            else
            {
                if (AudioManager.Initialized)
                    AudioManager.Play(AudioClipName.GameoverSound);

                MenuManager.GoToMenu(MenuName.GameOver);
                //Destroy ball
                desiredPosition = hit.transform.position;
                Destroy(player.gameObject);
                Destroy(this.gameObject);
            }
        }
        //Hits enemy, AIControl script handles actions so we just ignore here
        else if (hit.transform.gameObject.tag == "Enemy")
        {
            return;
        }
        //Hits teleporter, RandomTeleporter script handles actions so we just ignore here
        else if (hit.transform.gameObject.tag == "Teleporter")
        {
        }
        //Anything else is destroyed, actually just coins comes here
        else
        {
            if (AudioManager.Initialized)
                AudioManager.Play(AudioClipName.CoinPickup);
            Destroy(hit.transform.gameObject);
        }
    }

    //Loading next level
    void NextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
    
    //When start animation ends, we start swiping
    void ControlStart()
    {
        controlStart = true;
    }
}
