using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
 
[RequireComponent(typeof(ARFace))]
public class EyeTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject eyePrefab;
 
    private GameObject leftEye;
    private GameObject rightEye;
 
    private ARFace arFace;
   
    private void Awake()
    {
        arFace = GetComponent<ARFace>();
    }
 
    void SetVisibility(bool isVisible)
    {
        if (leftEye != null && rightEye != null)
        {
            leftEye.SetActive(isVisible);
            rightEye.SetActive(isVisible);
        }
    }

    private void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        if (arFace.leftEye != null && leftEye == null)
        {
            leftEye = Instantiate(eyePrefab, arFace.leftEye);
            leftEye.SetActive(false);
        }
        if (arFace.rightEye != null && rightEye == null)
        {
            rightEye = Instantiate(eyePrefab, arFace.rightEye);
            rightEye.SetActive(false);
        }
       
        bool shouldBeVisible = (arFace.trackingState == TrackingState.Tracking) &&
                               (ARSession.state > ARSessionState.Ready);
        SetVisibility(shouldBeVisible);
    }

    private void OnEnable()
    {
        ARFaceManager faceManager = FindObjectOfType<ARFaceManager>();
        if (faceManager != null && faceManager.subsystem != null &&
            faceManager.subsystem.subsystemDescriptor.supportsEyeTracking)
        {
            arFace.updated += OnUpdated;
            Debug.Log("Eye tracking is supported in this device");
        }
        else
        {
            Debug.LogError("Eye tracking is not supported in this device");
        }
    }
 
    void OnDisable()
    {
        arFace.updated -= OnUpdated;
        SetVisibility(false);
    }
}
