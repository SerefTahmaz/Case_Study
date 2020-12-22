using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotating ball according to direction its moving
public class Ball_Rotate : MonoBehaviour
{
    // Hold ball
    public GameObject itsBall;
    // Scaler for how fast is rotate
    public int scaler;

    // Update is called once per frame
    void Update()
    {
       
        //If we are moving and moving to left
        //Not moving because moving variable means if we are able to swipe to move, not physical moving
        if (!this.GetComponent<SwipeTest>().moving && this.GetComponent<SwipeTest>().direction==Direction.Left)
        {
            //Rotate left
            itsBall.transform.Rotate(0, 0, scaler * Time.deltaTime, Space.World);
        }
        //If we are moving and moving to right
        //Not moving because moving variable means if we are able to swipe to move, not physical moving
        if (!this.GetComponent<SwipeTest>().moving && this.GetComponent<SwipeTest>().direction == Direction.Right)
        {
            //Rotate right
            itsBall.transform.Rotate(0, 0, -scaler * Time.deltaTime, Space.World);
        }
        //If we are moving and moving to up
        //Not moving because moving variable means if we are able to swipe to move, not physical moving
        if (!this.GetComponent<SwipeTest>().moving && this.GetComponent<SwipeTest>().direction == Direction.Up)
        {
            //Rotate up
            itsBall.transform.Rotate(scaler * Time.deltaTime, 0, 0, Space.World);
        }
        //If we are moving and moving to down
        //Not moving because moving variable means if we are able to swipe to move, not physical moving
        if (!this.GetComponent<SwipeTest>().moving && this.GetComponent<SwipeTest>().direction == Direction.Down)
        {
            //Rotate down
            itsBall.transform.Rotate(-scaler * Time.deltaTime, 0, 0, Space.World);
        }
    }
}
