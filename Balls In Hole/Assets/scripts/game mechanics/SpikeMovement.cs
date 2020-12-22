using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spikes back and forth movement 
public class SpikeMovement : MonoBehaviour
{
    //Upward speed
    public float Upscaler = 100;
    //Downward speed
    public float DownScaler = 10;
    //Hold up condition
    bool up = true;
    
    bool wait = true;
    bool oneTime = true;

    //Start movement position
    Vector3 startPos;
    //End movement position
    Vector3 endPos;
    //Start spike movement random
    float randomStart;
    private void Start()
    {
        //Starting position
        startPos = this.transform.position;
        //End position is where our height ends plus a offset value
        endPos = startPos + this.transform.up * (this.transform.localScale.y+0.1f);
        //Random start movement
        randomStart = Random.Range(1.0f, 3.0f);

    }
    // Update is called once per frame
    void Update()
    {
        startPos = this.transform.position;
        //Move up as long as it does not reaches its height
        if (Vector3.Distance(startPos, endPos) > this.transform.localScale.y || up)
        {
            //Wait for random time
            if (wait)
            {
                //Runs it once for each up so just waits once at each up
                if(oneTime)
                {
                    //Set wait false to start up movement
                    Invoke("FalseWait", randomStart);
                    //Spike sound at each up movement
                    Invoke("SpikeSound", randomStart);
                    //Runs this statment only once
                    oneTime = false;
                }
                //Returns means it waits
                return;
            }
            //Move upward
            this.transform.Translate(0, Time.deltaTime * Upscaler, 0);
            //Moving up
            up = true;
            //Only wait at start
            oneTime = true;
        }

        //Move down as long as it does not reaches start position
        if (Vector3.Distance(startPos, endPos) < 0.1f || !up)
        {
            //Move downward
            this.transform.Translate(0, -Time.deltaTime * DownScaler, 0);
            //Moving down
            up = false;
            //wait for some time to going up
            wait = true;
        }
        
    }

    //Set wait
    void FalseWait()
    {
        wait = false;
    }

    
    //Sound for spikes
    void SpikeSound()
    {
        if (AudioManager.Initialized)
            AudioManager.Play(AudioClipName.SpikeTrap);
    }

    //Handle if it collide with player or balls
    private void OnTriggerEnter(Collider other)
    {
        //If it is player or balls then destroy it, and go to gameover menu
        if (other.gameObject.tag == "Players")
        {
            if(AudioManager.Initialized)
            AudioManager.Play(AudioClipName.GameoverSound);
            //Destroy its Swipe gameobject
            Destroy(other.gameObject.GetComponent<ItsSwipe>().SwipeTest.gameObject);
            //Destroy it
            Destroy(other.gameObject);
            //Go to gameover menu
            MenuManager.GoToMenu(MenuName.GameOver);
        }
    }
}
