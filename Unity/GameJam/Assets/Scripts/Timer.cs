using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public SafeZoneSensor sensor;

    //public Canvas canvas;

    public float totalTime;
    
    public Text timer;

    public Text gtimer;

    //public GameObject timerGameObject;

    //public GameObject gtimerGameobject;

    public float ptime_left = 0;

    public float gtime_left = 0;

    public GameObject player;

    public BoxCollider safe_zone;

    private GrabItems grabItems;

    public float timeAcceleration;

    public int countdown = 60;
    // Start is called before the first frame update
    void Start()
    {
        //timerGameObject = canvas.transform.Find("PTimer").gameObject;
        //gtimerGameobject = canvas.transform.Find("GTimer").gameObject;

        //timer = timerGameObject.GetComponent<Text>();
        //gtimer = gtimerGameobject.GetComponent<Text>();

        totalTime = 0;
        timeAcceleration = 1;
        ptime_left = 60f;
        gtime_left = 90;
        grabItems = player.GetComponent<GrabItems>();
        InvokeRepeating("BreakDown", 0.0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (gtime_left <= 0 || ptime_left <= 0)
        {
            SceneManager.LoadScene(2);
        }

        gtime_left = gtime_left - timeAcceleration * Time.deltaTime;

        float gminutes = Mathf.Floor(gtime_left / 60);

        string gsecString;

        float gseconds = gtime_left % 60;

        if (gseconds == 0)
        {
            gsecString = "00";
        }
        else if (gseconds <= 9)
        {
            gsecString = "0" + Mathf.Floor(gseconds);
        }
        else
        {
            gsecString = Mathf.Floor(gseconds).ToString();
        }

        gtimer.text = gminutes + ":" + gsecString;
        
        if (sensor.isOnSafeZone)
        {
            ptime_left = ((grabItems.generatorLevel + 1f) * 30f) + 30f;
            timer.text = Mathf.Floor(ptime_left/60f) + " : " + Mathf.Floor(ptime_left % 60f);
        }
        else
        {
            ptime_left = ptime_left - Time.deltaTime;

            float minutes = Mathf.Floor(ptime_left / 60f);

            string secString;

            float seconds = ptime_left % 60f;

            if (seconds == 0f)
            {
                secString = "00";
            }
            else if (seconds <= 9f)
            {
                secString = "0" + Mathf.Floor(seconds);
            }
            else
            {
                secString = Mathf.Floor(seconds).ToString();
            }

            timer.text = minutes + ":" + secString;
        }
    }

    public void BreakDown()
    {
        float probability;

        countdown--;

        if(countdown == 0)
        {
            probability = Mathf.Floor(Random.Range(0, 100));

            if(probability < 34f)
            {
                timeAcceleration = 1.25f;

                grabItems.generatorState = 1;
            }

            countdown = 60;
        }
        totalTime++;
    } 
}
