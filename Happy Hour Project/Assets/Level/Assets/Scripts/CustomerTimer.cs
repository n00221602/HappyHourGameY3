using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTimer : MonoBehaviour
{
    private float maximum = 30f * 2;
    private float customerTime;
    private GameObject customerTimer;
    public Image mask;
    public Color[] colorSwatches; // Array of colors to cycle through
    private Image timerImage; // Reference to the Image component of customerTimer

    void Start()
    {
        customerTimer = this.gameObject;
        customerTimer.SetActive(false);
        timerImage = customerTimer.GetComponent<Image>(); // Get the Image component
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        customerTimer.SetActive(true);
        customerTime += Time.deltaTime;
        float fillAmount = customerTime / maximum;
        mask.fillAmount = fillAmount;

        // Update the color based on the elapsed time
        if (colorSwatches.Length > 0)
        {
            float interval = maximum / colorSwatches.Length; // Calculate the interval for each color
            int colorIndex = Mathf.FloorToInt(customerTime / interval) % colorSwatches.Length; // Determine the current color index
            timerImage.color = colorSwatches[colorIndex];
        }
    }
}
