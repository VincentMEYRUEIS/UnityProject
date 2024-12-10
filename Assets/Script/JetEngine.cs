using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetEngine : MonoBehaviour
{
    public List<GameObject> Burners;
    public GameObject Smoke;
    public RotateAround Axis;
    public AudioSource Sound;

    public bool MotorStart = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    public void SetEngineState(bool Start)
    {
        if(Start)
        {
            EngineStart();
        }
        else
        {
            EngineStop();
        }


    }



    void EngineStart()
    {
        MotorStart = true;

        for (int i = 0; i < Burners.Count; i++)
        {
            Burners[i].SetActive(true);
        }

        Smoke.SetActive(true);
        Sound.Play();

        Axis.Speed = 200.0f;
    }


    void EngineStop()
    {
        MotorStart = false;

        for (int i = 0; i < Burners.Count; i++)
        {
            Burners[i].SetActive(false);
        }

        Smoke.SetActive(false);
        Sound.Stop();
        Axis.Speed = 0.0f;
    }


}
