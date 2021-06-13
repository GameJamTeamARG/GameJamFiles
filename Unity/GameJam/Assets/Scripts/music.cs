using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    public AudioSource audioSourceOut; 
    public AudioSource audioSourceIn;
  
  
    private void Start()
    {
        audioSourceIn.Play();
        audioSourceOut.Play();
        audioSourceIn.mute = false;
        audioSourceOut.mute = true;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSourceIn.mute = false;
            audioSourceOut.mute = true;

        }
        

         else 
        {
            //Debug.Log(collisionInfo.gameObject.name);
            Destroy(collider.gameObject);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSourceIn.mute = true;
            audioSourceOut.mute = false;


        }
    }

}
