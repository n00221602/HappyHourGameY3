using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    float elapsedTime = 10f;

    public GameObject timecube;
    void Start()
    {
      
        timecube.SetActive(true);
    }

    void Update()
    {
       
        if (!timecube.activeSelf)
        {
            startTimer();
        }
    }

    public void startTimer()
    {
        if (elapsedTime >= 0f)
        {
            elapsedTime -= Time.deltaTime; 
            int seconds = Mathf.FloorToInt(elapsedTime % 60f); 
            Debug.Log("Time: " + seconds); 
        }
        else
        {
            noTimer(); 
        }
    }

   
   public void noTimer()
    {
        Debug.Log("Timer ended");
        timecube.SetActive(true);
    }
}
