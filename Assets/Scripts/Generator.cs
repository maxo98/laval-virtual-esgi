using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject City;

    public List<GameObject> buildingsPrefab;
    public List<GridCase> buildingsEnum;
    public List<int> buildingsConso;

    public List<int> buildingsSmallMax;
    public List<int> buildingsSmallMin;
    public List<int> buildingsMediumMax;
    public List<int> buildingsMediumMin;
    public List<int> buildingsBigMax;
    public List<int> buildingsBigMin;
    public int consoSmallMax;
    public int consoSmallMin;
    public int consoMediumMax;
    public int consoMediumMin;
    public int consoBigMax;
    public int consoBigMin;

    public GameObject River;

    public List<GameObject> biomePrefab;
    public List<GridCase> biomeEnum;

    public CitySizeType cityType;

    public int consomation = 0;

    public enum CitySizeType
    {
        Small,
        Medium,
        Big
    }

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

    private int mapSize;
    private int citySize;

    private GridCase[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        //Random.InitState(42);

        int cityMin = 0;
        int cityMax = 0;
        int consoMax = 0;
        int consoMin = 0;
        List<int> buildingsMin = new List<int>();
        List<int> buildingsMax = new List<int>();

        switch(cityType)
        {
            case CitySizeType.Small:
                cityMin = 2;
                cityMax = 11;
                buildingsMin = buildingsSmallMin;
                buildingsMax = buildingsSmallMax;
                consoMin = consoSmallMin;
                consoMax = consoSmallMax;
                break;

            case CitySizeType.Medium:
                cityMin = 11;
                cityMax = 21;
                buildingsMin = buildingsMediumMin;
                buildingsMax = buildingsMediumMax;
                consoMin = consoMediumMin;
                consoMax = consoMediumMax;
                break;

            case CitySizeType.Big:
                cityMin = 21;
                cityMax = 30;
                buildingsMin = buildingsBigMin;
                buildingsMax = buildingsBigMax;
                consoMin = consoBigMin;
                consoMax = consoBigMax;
                break;
        }

        
        citySize = Random.Range(cityMin, cityMax);
        mapSize = (int)((double)citySize * 2.0);
        consomation = consoMin + (citySize - cityMin) * (consoMax - consoMin) / (cityMax - cityMin);

        grid = new GridCase[mapSize, mapSize];

        //Fill grid with empty
        for(int x = 0; x < mapSize; x++)
        {
            for(int y = 0; y < mapSize; y++)
            {
                grid[x, y] = GridCase.Empty;
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

        //Generate biomes
        List<Queue<Vector2Int>> biomeGrowth = new List<Queue<Vector2Int>>();

        for(int i = 0; i < biomeEnum.Count; i++)
        {
            biomeGrowth.Add(new Queue<Vector2Int>());

            Vector2Int posBiome = new Vector2Int();

            do{
                posBiome.x = Random.Range(0, mapSize);
                posBiome.y = Random.Range(0, mapSize);
            }while(grid[posBiome.x, posBiome.y] != GridCase.Empty);
            
            biomeGrowth[biomeGrowth.Count - 1].Enqueue(posBiome);
            grid[posBiome.x, posBiome.y] = biomeEnum[i];
        }

        int empty = 0;

        do{

            for(int i = 0; i < biomeEnum.Count; i++)
            {
                if(biomeGrowth[i].Count != 0)
                {
                    Vector2Int posBiome = biomeGrowth[i].Dequeue();

                    for(int x = -1; x <= 1; x++)
                    {
                        for(int y = -1; y <= 1; y++)
                        {
                            Vector2Int newPos = new Vector2Int(posBiome.x + x, posBiome.y + y);

                            if(Mathf.Abs(x) != Mathf.Abs(y) && newPos.x >= 0 && newPos.x < mapSize && newPos.y >= 0 && newPos.y < mapSize && grid[newPos.x, newPos.y] == GridCase.Empty)
                            {
                                grid[newPos.x, newPos.y] = biomeEnum[i];
                                biomeGrowth[i].Enqueue(newPos);
                            }
                        }
                    }

                    if(biomeGrowth[i].Count == 0)
                    {
                        empty++;
                    }
                }
            }

        }while(empty != biomeEnum.Count);

        //River
        Vector2 riverDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        while(riverDir.x == 0 || riverDir.x == 0)
        {
            riverDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
        
        int side = Random.Range(0, 2);
        int pos = Random.Range(2, mapSize-2);

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

        }while(riverPos.x >= 0 && riverPos.x < mapSize && riverPos.y >= 0 && riverPos.y < mapSize);

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
                    consomation += buildingsConso[buildingIndex];
                }
            }
        }

        //Generate instances
        for(int x = 0; x < mapSize; x++)
        {
            for(int y = 0; y < mapSize; y++)
            {
                GameObject newInstant = River;

                //Search type in buildings list
                for(int i = 0; i < buildingsEnum.Count; i++)
                {
                    if(grid[x, y] == buildingsEnum[i])
                    {
                        newInstant = buildingsPrefab[i];
                        break;
                    }
                }

                //Search type in biome list
                for(int i = 0; i < biomeEnum.Count; i++)
                {
                    if(grid[x, y] == biomeEnum[i])
                    {
                        newInstant = biomePrefab[i];
                        break;
                    }
                }

                //Search prefab in others
                switch(grid[x, y])
                {
                    case GridCase.City:
                        newInstant = City;
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
