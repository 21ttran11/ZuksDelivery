using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    private float extendAmount;

    // Update is called once per frame
    void Update()
    {
        //scaling from one side (bottom half of y)
        //scale y up/down, position moves half as much as scaling
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }


    }

    private void FishingLineExtend(float extendAmosunt)
    {
        
    }

    private void FishingLineShorten(float extendAmount)
    {

    }

}