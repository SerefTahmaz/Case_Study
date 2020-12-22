using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//Enemy AI behaviour
public class AIControl : MonoBehaviour
{

    //Hold navmeshagent component
    NavMeshAgent agent;
    //Hold player
    Transform player;
    //State when agent patrol along waypoints
    string state = "patrol";
    //Hold Waypoints 
    public GameObject[] waypoints;
    //Count which waypoint we going
    int currentWP = 0;
    //Hold when we change current waypoint
    float accuracyWP = 1.5f;
    //Hold player and balls
    GameObject[] players;
    //Hold Agent material
    public Material patrolMat;
    // Start is called before the first frame update
    void Start()
    {
        //Get player and balls
        players = GameObject.FindGameObjectsWithTag("Players");
        //Get player
        player = GameObject.Find("Player").transform;
        //Get navmeshagent component
        agent = this.GetComponent<NavMeshAgent>();
        //Set agent destination to first waypoint
        agent.SetDestination(waypoints[currentWP].transform.position);
    }

    private void Update()
    {
        //Player dies so no null reference
        if (player == null) return;

        //Is agent looking player
        Vector3 direction = player.position - this.transform.position;
        //No altitude
        direction.y = 0;
        //Agent looking direction and player position according to agent
        //Angle difference gives us if agent looking us or not
        float angle = Vector3.Angle(direction, this.transform.forward);

        //If there is waypoints, then patrol
        if (state == "patrol" && waypoints.Length > 0)
        {
            //Distance between agent and waypoint is low enough, then switch to other waypoints
            if (Vector3.Distance(waypoints[currentWP % waypoints.Length].transform.position, transform.position) < accuracyWP)
            {
                currentWP++;
            }
            //Set destination
            agent.SetDestination(waypoints[currentWP%waypoints.Length].transform.position);
            //Change color to green if we are patroling
            if (this.GetComponent<MeshRenderer>().material.color != Color.green)
                this.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        //If distance between agent and player lower than 10 and angle is lower than 30, then pursue
        if (Vector3.Distance(player.position, this.transform.position) < 10 && (angle < 30 || state == "pursuing"))
        {
            //state is pursuing so when snap to back agent, agent doesnt forget us immediately
            state = "pursuing";
              
            //Giving chance to player to escape
            if(Random.Range(0,1000)<10)
            agent.SetDestination(player.position);

            //Change color to red if we are pursuing
            if (this.GetComponent<MeshRenderer>().material.color != Color.red)
                this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            //Not pursuing, then patrol
            state = "patrol";
        }


        //Get player and balls
        players = GameObject.FindGameObjectsWithTag("Players");
        //Loop over player and balls
        foreach (GameObject g in players)
        {
            //If they close enough, destroy them and go to gameover menu
            if (Vector3.Distance(this.transform.position, g.transform.position) < 0.8f)
            {
                if (AudioManager.Initialized)
                    AudioManager.Play(AudioClipName.GameoverSound);
                //destory its swipetest script
                Destroy(g.GetComponent<ItsSwipe>().SwipeTest.gameObject);
                //destory it
                Destroy(g.gameObject);
                //go to gameover menu
                MenuManager.GoToMenu(MenuName.GameOver);
            }
        }
    }
}
