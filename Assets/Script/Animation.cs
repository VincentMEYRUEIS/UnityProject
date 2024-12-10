using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Animation : MonoBehaviour
{

    public PlayableDirector PlayableDirector;
    
    public void AnimationLaunch(bool Play)
    {
        if (Play)
        {
            PlayableDirector.Play();
        }
        else
        {
            PlayableDirector.Stop();

        }
     
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
