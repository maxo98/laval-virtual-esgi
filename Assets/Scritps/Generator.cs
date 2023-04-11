using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject City;
    public List<GameObject> buildingsPrefab;
    public List<GridCase> buildingsEnum;
    public List<int> buildingsMax;
    public List<int> buildingsMin;
    public GameObject Mountain;
    public GameObject Plain;
    public GameObject River;

    public enum GridCase
    {
        Empty,
        City,
        Hospital,
        Fireman,
        Mountain,
        Plain,
        River
    }

    public int size = 40;
    public int citySize = 5;

    private GridCase[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        //Random.InitState(42);

        grid = new GridCase[size, size];

        //Fill grid with empty
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                grid[x, y] = GridCase.Plain;//GridCase.Empty;
            }
        }

        int startPos = size/2 - citySize/2;

        //Generate empty city
        for(int x = startPos; x < (startPos + citySize); x++)
        {
            for(int y = startPos; y < (startPos + citySize); y++)
            {
                grid[x, y] = GridCase.City;
            }
        }

        //Place buildings
        for(int buildingIndex = 0; buildingIndex < buildingsPrefab.Count; buildingIndex++)
        {
            int n = Random.Range(buildingsMin[buildingIndex], buildingsMax[buildingIndex] + 1);

            for(int i = 0; i < n; i++)
            {
                int x = 0;
                int y = 0;

                int tryouts = 6;//Gives up when it reaches 0

                do{

                    x = Random.Range(0, citySize) + startPos;
                    y = Random.Range(0, citySize) + startPos;
                    tryouts--;
                    
                }while((grid[x, y] != GridCase.City || grid[x, y + 1] == buildingsEnum[buildingIndex]
                || grid[x, y - 1] == buildingsEnum[buildingIndex] || grid[x + 1, y] == buildingsEnum[buildingIndex]
                || grid[x - 1, y] == buildingsEnum[buildingIndex]) && tryouts > 0);

                if(tryouts > 0)
                {
                    grid[x, y] = buildingsEnum[buildingIndex];
                }
            }
        }

        //Generate instances
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                GameObject newInstant = Mountain;

                //Search type in buildings list
                for(int i = 0; i < buildingsEnum.Count; i++)
                {
                    if(grid[x, y] != buildingsEnum[i])
                    {
                        newInstant = buildingsPrefab[i];
                        break;
                    }
                }

                //Search prefab in others
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
