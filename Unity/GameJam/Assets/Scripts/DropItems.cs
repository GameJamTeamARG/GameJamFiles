using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DropItems : MonoBehaviour
{
    public Canvas canvas;
    public GameObject player;
    GrabItems grabItems;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            print("inventory");
            canvas.GetComponent<Image>().enabled = true;
            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<CamaraControll>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            print("Close inventory");
            canvas.GetComponent<Image>().enabled = false;
            player.GetComponent<Movement>().enabled = true;
            player.GetComponent<CamaraControll>().enabled = true;
            

        }
    }

    
}
