using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{
    public float interactDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void GeneratorCharge(string[] items)
    {
        int logCount = 0;

        for (int i = 0; i < 5; i++)
        {
            if(items[i] == "Log")
            {
                logCount++;
            }
        }

        if (logCount >= 2)
        {

        }
    }
}
