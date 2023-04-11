using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject City;
    public GameObject Hospital;
    public GameObject Mountain;
    public GameObject Plain;
    public GameObject Fireman;
    public GameObject River;

    enum GridCase
    {
        Empty,
        City,
        Hospital,
        Fireman,
        Mountain,
        Plain,
        River
    }

    public int size = 20;
    public int citySize = 5;

    private GridCase[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GridCase[size, size];

        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                grid[x, y] = GridCase.Plain;//GridCase.Empty;
            }
        }

        int startPos = size/2 - citySize/2;

        for(int x = startPos; x < (startPos + citySize); x++)
        {
            for(int y = startPos; y < (startPos + citySize); y++)
            {
                grid[x, y] = GridCase.City;
            }
        }

        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                GameObject newInstant = Mountain;

                switch(grid[x, y])
                {
                    case GridCase.City:
                        newInstant = City;
                        break;

                    case GridCase.Plain:
                        newInstant = Plain;
                        break;

                }

                Instantiate(newInstant, new Vector3(x - size/2, 0, y - size/2), new Quaternion());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
