using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolkit : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject toolkitPre;

    void Start()
    {
        GameObject h = Instantiate(toolkitPre) as GameObject;
        h.transform.position = new Vector3(Random.Range(this.transform.position.x-5, this.transform.position.x+5),50, Random.Range(this.transform.position.z - 5, this.transform.position.z + 5));
    }

    // Update is called once per frame

}
