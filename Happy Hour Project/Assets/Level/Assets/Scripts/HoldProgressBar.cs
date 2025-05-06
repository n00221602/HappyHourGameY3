using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldProgressBar : MonoBehaviour
{
    private float progressTime;
    public Image mask;
    public GameObject progressBar;

    private PhysicsRayCast physicsRayCast;
    void Start()
    {
        physicsRayCast = GameObject.Find("PlayerCam").GetComponent<PhysicsRayCast>();
        progressBar = this.gameObject;
        progressBar.SetActive(false);
    }
    void Update()
    {
        FillProgressBarHold();
    }

    public void FillProgressBarHold()
    {
        if (physicsRayCast.isMessyTable == true)
        {
            progressBar.SetActive(true);
            progressTime += Time.deltaTime;
            float fillAmount = progressTime / physicsRayCast.progressInterval;
            mask.fillAmount = fillAmount;

            if (progressTime >= physicsRayCast.progressInterval)
            {
                progressTime = 0f;
                mask.fillAmount = 0f;
                progressBar.SetActive(false);
            }
        }
        else
        {
            progressBar.SetActive(false);
            progressTime = 0f;
            mask.fillAmount = 0f;
        }
    }
}
