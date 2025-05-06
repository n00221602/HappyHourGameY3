using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayUI : MonoBehaviour
{
    public CustomerSpawner customerSpawner;
    public float transitionTime = 0f;
    Color currentColor;
    public TextMeshProUGUI dayText;
    public bool transition = false;
    void Start()
    {
        dayText = this.gameObject.GetComponent<TextMeshProUGUI>();
        currentColor = dayText.color;
        //currentColor.a refers to the alpha value, which controls the opactiy
        currentColor.a = 0f;
        dayText.color = currentColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentColor != null) 
        { 
            Debug.Log(currentColor);
        }
        if (customerSpawner.timerRunning && !transition)
        {
           UpdateDay();
        }
    }

    ///Updates the day UI text. Appears on screen for 3 seconds and then fades out.
    void UpdateDay()
    {
        dayText.text = "Day " + customerSpawner.currentDay;
        transitionTime += Time.deltaTime;
        Debug.Log(currentColor);
        currentColor.a = 0.3f;
        dayText.color = currentColor;
        if (transitionTime >= 3f)
        {
            currentColor.a = 0f;
            dayText.color = currentColor;
            transitionTime = 0f;
            transition = true;
        }
    }
}
