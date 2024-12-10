using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Linq;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    public List<GameObject> PlacedObject;


    private ARSessionOrigin arOrigin;
    private ARRaycastManager ARRayCastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;



    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        ARRayCastManager = FindObjectOfType<ARRaycastManager>();
        PlacedObject.Clear();
        Debug.Log("AR place Object init");
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();


        if (PlacedObject.Count >= 1 )
        {
            placementIndicator.SetActive(false);
            return;
        }
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
            Debug.Log("Ar Place Object:");
        }
    }

    public void PlaceObject()
    {
        if (placementIndicator.activeSelf)
        {
            PlacedObject.Add(Instantiate(objectToPlace, placementPose.position, placementPose.rotation));
        }
        
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        ARRayCastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    public void DeleteObject()
    {
        if (PlacedObject.Count > 0)
        {
            GameObject obj = PlacedObject.Last();
            string ObjName = obj.name;
            
            PlacedObject.Remove(obj);
            Destroy(obj);
            Debug.Log("AR place Object Delete object:" + ObjName);
        }
    }


}
