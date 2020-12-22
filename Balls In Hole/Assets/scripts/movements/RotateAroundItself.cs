using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotate coins around itself
public class RotateAroundItself : MonoBehaviour
{
    //Rotation speed
    public int scaler = 1;

    // Update is called once per frame
    void Update()
    {
        //Rotate around all axes
        transform.Rotate(scaler * Time.deltaTime, scaler * Time.deltaTime, scaler * Time.deltaTime);
    }
}
