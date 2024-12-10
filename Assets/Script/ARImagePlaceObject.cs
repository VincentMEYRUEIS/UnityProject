using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARImagePlaceObject : MonoBehaviour
{

    [SerializeField]
    private GameObject[] PlaceablePrefabs;
    private Dictionary<string,GameObject> spawedPrefabs = new Dictionary<string,GameObject>();

    private ARTrackedImageManager TrackedImageManager;


    private void Awake()
    {
        TrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject Prefab in PlaceablePrefabs)
        {
            GameObject newPrefab = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = Prefab.name;
            spawedPrefabs.Add(Prefab.name, newPrefab);
        }
    }



    private void OnEnable()
    {
        TrackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        TrackedImageManager.trackedImagesChanged -= ImageChanged;

    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawedPrefabs[trackedImage.name].SetActive(false);
        }

    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefabs = spawedPrefabs[name];
        prefabs.transform.position = position;
        prefabs.SetActive(true);

        foreach(GameObject go in spawedPrefabs.Values)
        {
            if(go.name != name)
            {
                go.SetActive(false);
            }
        }
    }
}
