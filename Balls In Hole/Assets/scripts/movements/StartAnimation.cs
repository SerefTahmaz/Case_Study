using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Animation for level cubes at start
public class StartAnimation : MonoBehaviour
{
    // Initial size
    Vector3 initialSize;
    // Holds anim state
    bool anim = false;
    // Lerping variable
    float t = 0;
    void Start()
    {
        //Get initial size
        initialSize = this.transform.localScale;
        //Set size to zero
        this.transform.localScale = Vector3.zero;
        //Invoke animation according to position so upper the position, later the animation start
        //and more right the position, later the animation as well
        Invoke("Anim", 0.3f+this.transform.position.x / 64f + this.transform.position.z/17f);
    }

    void Update()
    {
        //Start animation
        if (anim)
        {
            //Lerp zero size to initial size
            this.transform.localScale = Vector3.Lerp(Vector3.zero, initialSize, t);
            //Increment lerp variable
            t += 5f*Time.deltaTime;
        }
    }
    //Set start animation
    void Anim()
    {
        anim = true;
    }
}
