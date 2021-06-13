using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GrabItems : MonoBehaviour
{
    public string[] inventoryItems = new string[5];

    private Inventory inventory;

    private Timer timer;

    private int itemsGrabed;

    public int logNumber, batteryNumber, cableNumber, copperNumber, gearNumber, mechNumber, rubberNumber;

    public float grabDistance;

    public Canvas canvas;

    public Image logSprite, batterySprite, cableSprite, copperSprite, gearSprite, mechSprite, rubberSprite;

    public GameObject logPrefab, batteryPrefab, cablePrefab, copperPrefab, gearPrefab, mechPrefab, rubberPrefab;

    public int generatorState;

    public GameObject fixUI, chargeUI, upgradeUI, craftingUI;

    public int generatorLevel;

    // Start is called before the first frame update
    void Start()
    {
        inventory = this.GetComponent<Inventory>();
        timer = this.GetComponent<Timer>();
        generatorState = 0;
        generatorLevel = 0;

        fixUI = canvas.transform.Find("GeneratorFix").gameObject;
        upgradeUI = canvas.transform.Find("GeneratorUpgrade").gameObject;
        chargeUI = canvas.transform.Find("GeneratorCharge").gameObject;
        craftingUI = canvas.transform.Find("Crafting").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (generatorState == 0 && mechNumber >= 1)
        {
            generatorState = 2;
        }
        else if (generatorState == 2 && mechNumber < 1)
        {
            generatorState = 0;
        }

    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance))
        {
            GameObject collider = hit.collider.gameObject;

            Image newItem = logSprite;

            
            if(collider.tag == "generator")
            {
                

                switch (generatorState)
                {
                    case (0):
                        print("generator");
                        chargeUI.transform.GetChild(1).GetComponent<Text>().text = logNumber + " / 2";
                        chargeUI.SetActive(true);
                        break;
                    case (1):
                        fixUI.SetActive(true);
                        fixUI.transform.Find("Amount of cables").GetComponent<Text>().text = cableNumber + " / 1";
                        fixUI.transform.Find("Amount of batteries").GetComponent<Text>().text = batteryNumber + " / 1";
                        fixUI.transform.Find("Amount of Gears").GetComponent<Text>().text = gearNumber + " / 1";
                        break;
                    case (2):
                        upgradeUI.SetActive(true);
                        break;
                    default:
                        
                        break;
                }
            }
            else if (collider.tag == "Crafting")
            {
                craftingUI.SetActive(true);
                craftingUI.transform.Find("Amount of rubber1").GetComponent<Text>().text = rubberNumber + " / 1";
                craftingUI.transform.Find("Amount of copper1").GetComponent<Text>().text = copperNumber + " / 1";

                craftingUI.transform.Find("Amount of cable").GetComponent<Text>().text = cableNumber + " / 1";
                craftingUI.transform.Find("Amount of rubber2").GetComponent<Text>().text = rubberNumber + " / 1";
                craftingUI.transform.Find("Amount of copper2").GetComponent<Text>().text = copperNumber + " / 1";

                craftingUI.transform.Find("Amount of mechKits").GetComponent<Text>().text = gearNumber + " / 3";

                
            }
            
            
            if (Input.GetKey("e"))
            { 
                if (collider.tag == "generator")
                {
                    switch (generatorState)
                    {

                        case (0):
                            if (logNumber >= 2)
                            {
                                int i = 0;

                                print("pass 1");
                                for (int l = 0; l < 6; l++)
                                {
                                    if (i < 2)
                                    {
                                        print("yesss");
                                        
                                        if (inventoryItems[l] == "Log")
                                        {
                                            print("pasaste" + l);

                                            inventoryItems[l] = null;
                                            inventory.isFull[l] = false;
                                            Destroy(inventory.slots[l].transform.Find("LogIcon(Clone)").gameObject);
                                            i++;
                                        }
                                        else
                                        {
                                            print("no");
                                        }
                                        
                                    }
                                
                                }

                                logNumber--;
                                logNumber--;

                                timer.gtime_left = (generatorLevel + 3) * 60;

                            }
                            break;

                        case (1):

                            int n = 0;
                            int c = 0;
                            int m = 0;

                            if (batteryNumber >= 1 && cableNumber >= 1 && gearNumber >= 1)
                            {
                                for (int l = 0; l < 6; l++)
                                {
                                    if (n == 0)
                                    {
                                        print("yesss");

                                        if (inventoryItems[l] == "Cable")
                                        {
                                            print("pasaste" + l);

                                            inventoryItems[l] = null;
                                            inventory.isFull[l] = false;
                                            Destroy(inventory.slots[l].transform.Find("CableIcon(Clone)").gameObject);
                                            n++;
                                            cableNumber--;
                                        }

                                    }

                                }
                                for (int l = 0; l < 6; l++)
                                {
                                    if (c == 0)
                                    {
                                        print("yesss");

                                        if (inventoryItems[l] == "Battery")
                                        {
                                            print("pasaste" + l);

                                            inventoryItems[l] = null;
                                            inventory.isFull[l] = false;
                                            Destroy(inventory.slots[l].transform.Find("BatteryIcon(Clone)").gameObject);
                                            c++;
                                            batteryNumber--;
                                        }

                                    }

                                }
                                for (int l = 0; l < 6; l++)
                                {
                                    if (m == 0)
                                    {
                                        print("yesss");

                                        if (inventoryItems[l] == "Gear")
                                        {
                                            print("pasaste" + l);

                                            inventoryItems[l] = null;
                                            inventory.isFull[l] = false;
                                            Destroy(inventory.slots[l].transform.Find("GearIcon(Clone)").gameObject);
                                            m++;
                                            gearNumber--;
                                        }
                                    }

                                }

                                timer.gtime_left = (generatorLevel + 3) * 60;

                            }
                            break;
                        case (2):
                            if (mechNumber >= 1)
                            {
                                int o = 0;

                                print("pass 1");
                                for (int l = 0; l < 6; l++)
                                {
                                    if (o == 0)
                                    {
                                        print("yesss");

                                        if (inventoryItems[l] == "MechKit")
                                        {
                                            print("pasaste" + l);

                                            inventoryItems[l] = null;
                                            inventory.isFull[l] = false;
                                            Destroy(inventory.slots[l].transform.Find("MechKitIcon(Clone)").gameObject);
                                            o++;
                                        }

                                    }

                                }

                                mechNumber--;

                                generatorLevel++;

                                timer.gtime_left = (generatorLevel + 3) * 60;
                                //timer.ptime_left = ((generatorLevel + 1) / 2) + 30f;

                            }
                            break;
                        default:
                            break;
                    }
                    
                }
                else if(collider.tag == "Crafting")
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else if (collider.transform.Find("Item").gameObject.tag == "Item")
                {

                    if (itemsGrabed < 6)
                    {
                        print(itemsGrabed);

                        switch (collider.tag)
                        {
                            case ("Log"):
                                print("Collected log");
                                newItem = logSprite;
                                logNumber++;
                                break;

                            case ("Battery"):
                                print("Collected Battery");
                                newItem = batterySprite;
                                batteryNumber++;
                                break;
                            case ("Cable"):
                                newItem = cableSprite;
                                cableNumber++;
                                break;
                            case ("Copper"):
                                newItem = copperSprite;
                                copperNumber++;
                                break;
                            case ("Gear"):
                                newItem = gearSprite;
                                gearNumber++;
                                break;
                            case ("MechKit"):
                                newItem = mechSprite;
                                mechNumber++;
                                break;
                            case ("Rubber"):
                                newItem = rubberSprite;
                                rubberNumber++;
                                break;
                            default:
                                break;
                        }

                        print(itemsGrabed);

                        itemsGrabed++;

                        Destroy(collider);

                        for (int i = 0; i <= 6; i++)
                        {
                            if (inventory.isFull[i] == false)
                            {
                                inventory.isFull[i] = true;
                                Instantiate(newItem, inventory.slots[i].transform, false);
                                inventoryItems[i] = collider.tag;
                                print("created");
                                break;
                            }
                        }
                    }
                }
                
            }
            
        }
        else
        {
            chargeUI.SetActive(false);
            fixUI.SetActive(false);
            upgradeUI.SetActive(false);

            craftingUI.SetActive(false);
            
        }
        //chargeUI.SetActive(false);
    }

    public void Craft1()
    {
        int i = 0;
        int n = 0;

        if (rubberNumber >= 1 && copperNumber >= 1)
        {
            for (int l = 0; l < 6; l++)
            {
                if (i == 0)
                {
                    print("yesss");

                    if (inventoryItems[l] == "Rubber")
                    {
                        print("pasaste" + l);

                        inventoryItems[l] = null;
                        inventory.isFull[l] = false;
                        Destroy(inventory.slots[l].transform.Find("RubberIcon(Clone)").gameObject);
                        i++;
                        rubberNumber--;
                    }

                }

            }
            for (int l = 0; l < 6; l++)
            {
                if (n == 0)
                {
                    print("yesss");

                    if (inventoryItems[l] == "Copper")
                    {
                        print("pasaste" + l);

                        inventoryItems[l] = null;
                        inventory.isFull[l] = false;
                        Destroy(inventory.slots[l].transform.Find("CopperIcon(Clone)").gameObject);
                        n++;
                        copperNumber--;
                    }

                }

            }

            for (int m = 0; i < 6; i++)
            {
                if (inventory.isFull[m] == false)
                {
                    inventory.isFull[m] = true;
                    Instantiate(cableSprite, inventory.slots[m].transform, false);
                    inventoryItems[m] = "Cable";
                    cableNumber++;
                    print("created");
                    break;
                }
            }

        }
    }

    public void Craft2()
    {
        int i = 0;
        int n = 0;
        int c = 0;

        if (rubberNumber >= 1 && copperNumber >= 1 && cableNumber >= 1)
        {
            for (int l = 0; l < 6; l++)
            {
                if (i == 0)
                {
                    print("yesss");

                    if (inventoryItems[l] == "Rubber")
                    {
                        print("pasaste" + l);

                        inventoryItems[l] = null;
                        inventory.isFull[l] = false;
                        Destroy(inventory.slots[l].transform.Find("RubberIcon(Clone)").gameObject);
                        i++;
                        rubberNumber--;
                    }

                }

            }
            for (int l = 0; l < 6; l++)
            {
                if (n == 0)
                {
                    print("yesss");

                    if (inventoryItems[l] == "Copper")
                    {
                        print("pasaste" + l);

                        inventoryItems[l] = null;
                        inventory.isFull[l] = false;
                        Destroy(inventory.slots[l].transform.Find("CopperIcon(Clone)").gameObject);
                        n++;
                        copperNumber--;
                    }

                }

            }
            for (int l = 0; l < 6; l++)
            {
                if (c == 0)
                {
                    print("yesss");

                    if (inventoryItems[l] == "Cable")
                    {
                        print("pasaste" + l);

                        inventoryItems[l] = null;
                        inventory.isFull[l] = false;
                        Destroy(inventory.slots[l].transform.Find("CableIcon(Clone)").gameObject);
                        c++;
                        cableNumber--;
                    }

                }

            }

            for (int m = 0; i < 6; i++)
            {
                if (inventory.isFull[m] == false)
                {
                    inventory.isFull[m] = true;
                    Instantiate(batterySprite, inventory.slots[m].transform, false);
                    inventoryItems[i] = "Battery";
                    batteryNumber++;
                    print("created");
                    break;
                }
            }

        }
    }
    public void Craft3()
    {
        int i = 0;

        if (gearNumber >= 3)
        {
            for (int l = 0; l < 6; l++)
            {
                if (i < 3)
                {
                    print("yesss");

                    if (inventoryItems[l] == "Gear")
                    {
                        print("pasaste" + l);

                        inventoryItems[l] = null;
                        inventory.isFull[l] = false;
                        Destroy(inventory.slots[l].transform.Find("GearIcon(Clone)").gameObject);
                        i++;
                        gearNumber--;
                    }

                }

            }
            

            for (int m = 0; m <= 6; m++)
            {
                if (inventory.isFull[m] == false)
                {
                    inventory.isFull[m] = true;
                    Instantiate(mechSprite, inventory.slots[m].transform, false);
                    inventoryItems[m] = "MechKit";
                    mechNumber++;
                    print("created");
                    break;
                }
            }

        }
    }

    public void SlotButtonPressed()
    {

        print("pressed");

        GameObject button = EventSystem.current.currentSelectedGameObject;
        string buttonName = button.name;
        int slot = int.Parse(buttonName) - 1;

        Vector3 dropPos = new Vector3(this.gameObject.transform.position.x, 0.17f, this.gameObject.transform.position.z);

        dropPos = dropPos + transform.forward * 3;

        print(inventoryItems[slot]);

        if(inventoryItems[slot] != null)
        {
            switch (inventoryItems[slot])
            {
                case ("Log"):
                    Instantiate(logPrefab, dropPos, logPrefab.transform.rotation);
                    print(inventory.slots[slot].name);
                    Destroy(inventory.slots[slot].transform.Find("LogIcon(Clone)").gameObject);
                    inventoryItems[slot] = null;
                    inventory.isFull[slot] = false;
                    itemsGrabed--;
                    logNumber--;
                    break;
                case ("Battery"):
                    Instantiate(batteryPrefab, dropPos, batteryPrefab.transform.rotation);
                    print(inventory.slots[slot].name);
                    Destroy(inventory.slots[slot].transform.Find("BatteryIcon(Clone)").gameObject);
                    inventoryItems[slot] = null;
                    inventory.isFull[slot] = false;
                    itemsGrabed--;
                    batteryNumber--;
                    break;
                case ("Cable"):
                    Instantiate(cablePrefab, dropPos, cablePrefab.transform.rotation);
                    print(inventory.slots[slot].name);
                    Destroy(inventory.slots[slot].transform.Find("CableIcon(Clone)").gameObject);
                    inventoryItems[slot] = null;
                    inventory.isFull[slot] = false;
                    itemsGrabed--;
                    cableNumber--;
                    break;
                case ("Copper"):
                    Instantiate(copperPrefab, dropPos, copperPrefab.transform.rotation);
                    print(inventory.slots[slot].name);
                    Destroy(inventory.slots[slot].transform.Find("CopperIcon(Clone)").gameObject);
                    inventoryItems[slot] = null;
                    inventory.isFull[slot] = false;
                    itemsGrabed--;
                    copperNumber--;
                    break;
                case ("Gear"):
                    Instantiate(gearPrefab, dropPos, gearPrefab.transform.rotation);
                    print(inventory.slots[slot].name);
                    Destroy(inventory.slots[slot].transform.Find("GearIcon(Clone)").gameObject);
                    inventoryItems[slot] = null;
                    inventory.isFull[slot] = false;
                    itemsGrabed--;
                    gearNumber--;
                    break;
                case ("MechKit"):
                    Instantiate(mechPrefab, dropPos, mechPrefab.transform.rotation);
                    print(inventory.slots[slot].name);
                    Destroy(inventory.slots[slot].transform.Find("MechKitIcon(Clone)").gameObject);
                    inventoryItems[slot] = null;
                    inventory.isFull[slot] = false;
                    itemsGrabed--;
                    mechNumber--;
                    break;
                case ("Rubber"):
                    Instantiate(rubberPrefab, dropPos, rubberPrefab.transform.rotation);
                    print(inventory.slots[slot].name);
                    Destroy(inventory.slots[slot].transform.Find("RubberIcon(Clone)").gameObject);
                    inventoryItems[slot] = null;
                    inventory.isFull[slot] = false;
                    itemsGrabed--;
                    rubberNumber--;
                    break;
                default:
                    break;
            }
        }
        

    }

}
