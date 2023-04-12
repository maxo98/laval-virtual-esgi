using System;
using UnityEngine;

namespace DragAndDropObjects
{
    public class GeneratorBehavior : MonoBehaviour
    {
        public ComputerPlayerController controller;
        public DragAndDropManager dragAndDropManager;
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
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (type == GeneratorType.Nuclear && other.gameObject.CompareTag("Plain"))
            {
                controller.DraggedObjectCanBePlaced = true;
            }
            else if(type == GeneratorType.Hydro && other.gameObject.CompareTag("River"))
                if(dragAndDropManager.HasObjectOfType(GeneratorType.Hydro))
                    controller.DraggedObjectCanBePlaced = true;
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (type == GeneratorType.Nuclear && other.gameObject.CompareTag("Plain"))
            {
                controller.DraggedObjectCanBePlaced = false;
            }
            else if(type == GeneratorType.Hydro && other.gameObject.CompareTag("River"))
                if(dragAndDropManager.HasObjectOfType(GeneratorType.Hydro))
                    controller.DraggedObjectCanBePlaced = false;
        }
    }
}
