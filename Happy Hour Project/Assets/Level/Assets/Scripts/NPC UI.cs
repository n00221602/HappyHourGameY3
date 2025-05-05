using UnityEngine;
using UnityEngine.UI;

public class FloatingUI : MonoBehaviour
{
    public Canvas worldSpaceCanvas;
    public Vector3 offset; 

    private Transform target; 
    public GameObject playerCamera;

    void Start()
    {
        target = this.transform;

        if (worldSpaceCanvas == null)
        {
            Debug.LogError("World Space Canvas is not assigned.");
        }

        playerCamera = GameObject.Find("PlayerCam");
    }

    void Update()
    {
        if (worldSpaceCanvas != null && target != null)
        {
            // Update the canvas position to follow the target with the specified offset
            worldSpaceCanvas.transform.position = target.position + offset;

            if (playerCamera != null)
            {
                worldSpaceCanvas.transform.LookAt(playerCamera.transform);
                worldSpaceCanvas.transform.rotation = Quaternion.LookRotation(worldSpaceCanvas.transform.position - playerCamera.transform.position);
            }
        }
    }
}
