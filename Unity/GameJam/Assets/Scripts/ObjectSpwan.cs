using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpwan : MonoBehaviour
{

    public GameObject tree;
    public GameObject wood;
    public GameObject rock;
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject House;
    public GameObject Player;
    public GameObject Car1;
    public GameObject Car2;
    public GameObject Car3;
    public GameObject Car4;
    public GameObject copper;
    public GameObject tire;
    public int numof_trees;
    public int numof_rock;
    public int Carnum;
    public int numof_copper;
    public int numof_tire;
    int x;
    int y;
    public int numPlanes;
    int plane = 10;





    // Start is called before the first frame update
    private void AllMapGen(GameObject prefab, int num, int h)
    {
        for (int i = 0; i < (num * 10); i++)
        {
            GameObject a = Instantiate(prefab) as GameObject;
            a.transform.position = new Vector3(Random.Range(0, numPlanes * plane), h, Random.Range(0, numPlanes * plane));

        }

    }
    private void HouseGen(int x, int y)
    {

        int xmin = (x * plane) - 10;
        int xmax = (x * plane) + 10;
        int ymin = (y * plane) - 10;
        int ymax = (y * plane) + 10;
        int Corx = Random.Range(xmin, xmax);
        int Corz = Random.Range(ymin, ymax);
        GameObject h = Instantiate(House) as GameObject;
        h.transform.position = new Vector3(Corx, 20, Corz);

        Player.transform.position = new Vector3(Corx, 40, Corz);
        
        //GameObject p = Instantiate(Player) as GameObject;
        //p.transform.position = new Vector3(Corx, 40, Corz);
    }
    private void DependGen(int x, int y, GameObject prefab, float num)
    {

        int xmin = (x * plane) - 10;
        int xmax = (x * plane) + 10;
        int ymin = (y * plane) - 10;
        int ymax = (y * plane) + 10;
        for (int i = 0; i < (num * 10); i++)
        {

            GameObject h = Instantiate(prefab) as GameObject;
            h.transform.position = new Vector3(Random.Range(xmin, xmax), 40, Random.Range(ymin, ymax));


        }

    }
    private void Spwan(GameObject prefab, float num)
    {
        for (int x = 0; x < numPlanes; x++)
        {
            for (int y = 0; x < numPlanes; y++)
            {
               float yes = Random.Range(1, num);
                float corX = x - numPlanes / 2;
                float corY = y - numPlanes / 2;
                if (yes <= 1) { 
                DependGen(x, y, prefab, 1);
                }
            }
        }
    }
    void Start()
    {
        HouseGen(numPlanes / 2, numPlanes / 2);
        AllMapGen(tree, numof_trees,100);
        AllMapGen(wood, numof_trees/4, 40);
        AllMapGen(rock, numof_rock,40);
        AllMapGen(rock1, numof_rock, 40);
        AllMapGen(rock2, numof_rock, 40);
        AllMapGen(rock3, numof_rock, 40);
        AllMapGen(copper, numof_copper, 40);

        AllMapGen(tire, numof_tire, 40);


        AllMapGen(Car1, Carnum, 40);

    }
}
