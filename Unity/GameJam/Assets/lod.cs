using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lod : MonoBehaviour
{
    public GameObject Canvas1;
    public GameObject Canvas2;

    float timeLeft = 5;


    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Canvas1.SetActive(false);
            Canvas2.SetActive(true);
        }
    }
}
