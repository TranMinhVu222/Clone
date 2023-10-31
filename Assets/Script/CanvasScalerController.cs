using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerController : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    private float previousWidthOrientation, previousHeightOrientation ;

    void Start()
    {
        // Get the CanvasScaler component of the GameObject containing the Canvas
        canvasScaler = GetComponent<CanvasScaler>();

        // Update the match mode when the game starts
        UpdateMatchMode();

        // Listen for screen orientation changes
        previousHeightOrientation = UnityEngine.Screen.height;
        previousWidthOrientation = UnityEngine.Screen.width;
    }

    void Update()
    {
        // Check if the screen orientation has changed
        if (UnityEngine.Screen.height != previousHeightOrientation || 
            UnityEngine.Screen.width != previousWidthOrientation)
        {
            // Update the match mode when the screen orientation changes
            UpdateMatchMode();

            // Save the new screen orientation
            previousHeightOrientation = UnityEngine.Screen.height;
            previousWidthOrientation = UnityEngine.Screen.width;
        }
    }

    // Function to update the match mode based on the current screen aspect ratio
    void UpdateMatchMode()
    {
        // Calculate the aspect ratio of the screen (height / width)
        float screenAspectRatio = (float)UnityEngine.Screen.height / (float)UnityEngine.Screen.width;

        // Set the matchWidthOrHeight based on the screen aspect ratio
        // Reference: Match (iPhone 12 Mini) = 0.7 and Match (iPad Pro 9.7") = 1.06
        canvasScaler.matchWidthOrHeight = 1.48f - screenAspectRatio * 0.36f;
    }
}