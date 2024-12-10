using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    public Vector3 Axis = Vector3.right;

    [Range(0, 5000)]
    public float Speed = 0.0f; //RPM

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Axis, Speed * - 360/60 * Time.deltaTime);
    }
}
