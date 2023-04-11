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

    public int mapSize = 40;
    public int citySize = 5;

    private GridCase[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        //Random.InitState(42);

        grid = new GridCase[mapSize, mapSize];

        //Fill grid with empty
        for(int x = 0; x < mapSize; x++)
        {
            for(int y = 0; y < mapSize; y++)
            {
                grid[x, y] = GridCase.Plain;//GridCase.Empty;
            }
        }

        int startPos = mapSize/2 - citySize/2;

        //Generate empty city
        for(int x = startPos; x < (startPos + citySize); x++)
        {
            for(int y = startPos; y < (startPos + citySize); y++)
            {
                grid[x, y] = GridCase.City;
            }
        }

        //River
        Vector2 riverDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        
        int side = Random.Range(0, 2);
        int pos = Random.Range(3, mapSize-3);

        Vector2Int riverPos = new Vector2Int(0, 0);

        Vector2Int[] riverMove = new Vector2Int[2];

        if(side == 0)
        {
            if(riverDir.x < 0)
            {
                riverPos.x = mapSize-1;
            }

            riverPos.y = pos;
        }else{
            if(riverDir.y < 0)
            {
                riverPos.y = mapSize-1;
            }

            riverPos.x = pos;
        }

        if(riverDir.x < 0)
        {
            riverMove[0] = new Vector2Int(-1, 0);
        }else{
            riverMove[0] = new Vector2Int(1, 0);
        }

        if(riverDir.y < 0)
        {
            riverMove[1] = new Vector2Int(0, -1);
        }else{
            riverMove[1] = new Vector2Int(0, 1);
        }

        grid[riverPos.x, riverPos.y] = GridCase.River;

        do{
            side = Random.Range(0, 2);

            riverPos += riverMove[side];

            if(riverPos.x >= 0 && riverPos.x < mapSize && riverPos.y >= 0 && riverPos.y < mapSize)
            {
                grid[riverPos.x, riverPos.y] = GridCase.River;
            }

        }while(riverPos.x > 0 && riverPos.x < mapSize && riverPos.y > 0 && riverPos.y < mapSize);

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
        for(int x = 0; x < mapSize; x++)
        {
            for(int y = 0; y < mapSize; y++)
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

                    case GridCase.River:
                        newInstant = River;
                        break;
                }

                Instantiate(newInstant, new Vector3(x - mapSize/2, 0, y - mapSize/2), new Quaternion());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
