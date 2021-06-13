using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControll : MonoBehaviour
{
    public float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("HorizontalCamara") * mouseSensitivity * Time.deltaTime;

        this.transform.Rotate(Vector3.up * mouseX);
    }
}
