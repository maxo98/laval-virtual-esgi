using System;
using UnityEngine;

namespace DragAndDropObjects
{
    public class GeneratorBehavior : MonoBehaviour
    {
        public ComputerPlayerController controller;
        public DragAndDropManager dragAndDropManager;
        public Generator mapGenerator;
        public GeneratorType type;
        public float EnergyProduced { get; }
        public bool IsPlaced { private get; set; }

        private Generator.GridCase currentTile;
        
        // Start is called before the first frame update
        void Start()
        {
            IsPlaced = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsPlaced) return;
            CanBePlaced();
        }

        void CanBePlaced()
        {
            switch (type)
            {
                case GeneratorType.Nuclear:
                {
                    var position = gameObject.transform.position;
                    var tileType = mapGenerator.grid[(int)position.x, (int)position.z];
                    if (tileType != Generator.GridCase.Plain) break;
                    controller.DraggedObjectCanBePlaced = true;
                    // var startX = 0;
                    // var startZ = 0;
                    // var endX = 0;
                    // var endZ = 0;
                    // if ((int)position.x != 0)
                    // {
                    //     startX = (int)position.x - 1;
                    //     if ((int)position.x == mapGenerator.mapSize)
                    //         endX = mapGenerator.mapSize;
                    //     else
                    //         endX = (int)position.x + 1;
                    // }
                    // else
                    // {
                    //     startX = 0;
                    //     endX = (int)position.x + 1;
                    // }
                    // if ((int)position.z != 0)
                    // {
                    //     startZ = (int)position.z - 1;
                    //     if ((int)position.z == mapGenerator.mapSize)
                    //         endZ = mapGenerator.mapSize;
                    //     else
                    //         endZ = (int)position.z + 1;
                    // }
                    // else
                    // {
                    //     startZ = 0;
                    //     endZ = (int)position.z + 1;
                    // }
                    //
                    // var isNextToRiver = false;
                    // for (var i = startX; i < endX; i++)
                    // {
                    //     for (var j = startZ; i < endZ; j++)
                    //     {
                    //         if (mapGenerator.grid[(int)position.x, (int)position.z] != Generator.GridCase.River)
                    //             continue;
                    //         isNextToRiver = true;
                    //         break;
                    //     }
                    // }
                    // controller.DraggedObjectCanBePlaced = isNextToRiver;
                    break;
                }
            }
        }
    }
}
