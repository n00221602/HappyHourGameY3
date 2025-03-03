using UnityEngine;
using UnityEngine.UI;

public class FloatingUI : MonoBehaviour
{
    public Canvas worldSpaceCanvas; // Reference to the world-space canvas
    public Vector3 offset; // Offset from the GameObject's position

    private Transform target; // The target GameObject
    public GameObject playerCamera;

    void Start()
    {
        target = this.transform; // Set the target to the GameObject this script is attached to

        if (worldSpaceCanvas == null)
        {
            Debug.LogError("World Space Canvas is not assigned.");
        }
    }

    void Update()
    {
        if (worldSpaceCanvas != null && target != null)
        {
            // Update the canvas position to follow the target with the specified offset
            worldSpaceCanvas.transform.position = target.position + offset;

            // Optionally, make the canvas face the camera
            if (playerCamera != null)
            {
                worldSpaceCanvas.transform.LookAt(playerCamera.transform);
                worldSpaceCanvas.transform.rotation = Quaternion.LookRotation(worldSpaceCanvas.transform.position - playerCamera.transform.position);
            }
        }
    }
}
