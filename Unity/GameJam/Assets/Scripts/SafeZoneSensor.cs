using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneSensor : MonoBehaviour
{

    public bool isOnSafeZone;

    private void Start()
    {
        isOnSafeZone = true;
    }
    void OnTriggerEnter(Collider collider)
    {
        

        if (collider.tag == "safezone")
        {
            print("enter");
            isOnSafeZone = true;
        }

        //isOnSafeZone = true;
    }
    void OnTriggerExit(Collider collider)
    {
        

        if (collider.tag == "safezone")
        {
            print("exit");
            isOnSafeZone = false;
        }

        //isOnSafeZone = false;
    }

}
