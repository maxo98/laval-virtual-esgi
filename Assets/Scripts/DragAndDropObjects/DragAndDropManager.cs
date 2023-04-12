using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DragAndDropObjects
{
    public class DragAndDropManager : MonoBehaviour
    {
        int score = 0;

        public List<GeneratorBehavior> placedGenerators;
        // Start is called before the first frame update
        void Start()
        {
            placedGenerators = new List<GeneratorBehavior>();
        }

        // Update is called once per frame
        void Update()
        {
            score = 0;

            for(int i = 0; i < placedGenerators.Count; i++)
            {
                score += (int)placedGenerators[i].EnergyProduced;
            }
        }

        public bool HasObjectOfType(GeneratorType type)
        {
            return placedGenerators.Any(item => item.type == type);
        }
    }
}
