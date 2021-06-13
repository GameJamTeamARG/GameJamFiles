using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animP : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        
    }
}
