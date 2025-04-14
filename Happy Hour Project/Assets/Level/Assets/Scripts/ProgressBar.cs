using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private float progressTime;
    //public float progressInterval;
    public Image mask;
    public GameObject progressBar;

    private PhysicsRayCast physicsRayCast;
    //private CustomerNPC customerNPC;
    void Start()
    {
        physicsRayCast = GameObject.Find("PlayerCam").GetComponent<PhysicsRayCast>();
        progressBar = this.gameObject;
        progressBar.SetActive(false);
    }
    void Update()
    {
        FillProgressBar();
    }

    public void FillProgressBar()
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
}
