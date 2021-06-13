using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gravity : MonoBehaviour
{

    public Rigidbody rigidbody;
    public Vector3 killZone;


    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "land")
        {
            //Debug.Log(collisionInfo.gameObject.name);
            this.rigidbody.useGravity = false;
        }

    }
    void Update()
    {
        if (transform.position.y < killZone.y)
        {
            Destroy(this.gameObject);

        }




    }
}
