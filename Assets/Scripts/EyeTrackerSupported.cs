using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EyeTrackerSupported : MonoBehaviour
{
    public TMP_Text eyeTrackerSupportedText;
    
    private void OnEnable()
    {
        ARFaceManager faceManager = FindObjectOfType<ARFaceManager>();
        
        if (faceManager != null && faceManager.subsystem != null &&
            faceManager.subsystem.subsystemDescriptor.supportsEyeTracking)
        {
            eyeTrackerSupportedText.text = "Eye tracking is supported in this device";
        }
        else
        {
            eyeTrackerSupportedText.text = "Eye tracking is not supported in this device";
        }
    }
}
