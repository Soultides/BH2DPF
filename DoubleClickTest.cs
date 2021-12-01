using UnityEngine;
using System.Collections;

public class DoubleClickTest : MonoBehaviour 
{
    private float doubleClickTimeLimit = 0.25f;

    protected void Start()
    {
        StartCoroutine(InputListener());
    }

// Update is called once per frame
    private IEnumerator InputListener() 
    {
        while(enabled)
        { //Run as long as this is active

            if(Input.GetMouseButtonDown(0))
                yield return ClickEvent();

            yield return null;
        }
    }

    private IEnumerator ClickEvent()
    {
        //pause a frame so you don't pick up the same mouse down event.
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while(count < doubleClickTimeLimit)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //Double click
                DoubleClick();
                yield break;
            }
            count += Time.deltaTime;// increment counter by change in time between frames
            yield return null; // wait for the next frame
        }
        //Single Click
        SingleClick();
    }


    private void SingleClick()
    {
        Debug.Log("Single Click");
    }

    private void DoubleClick()
    {
        Debug.Log("Double Click");
    }

}