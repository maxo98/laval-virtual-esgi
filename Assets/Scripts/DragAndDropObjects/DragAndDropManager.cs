using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DragAndDropObjects
{
    public class DragAndDropManager : MonoBehaviour
    {
        public List<GeneratorBehavior> placedGenerators;
        // Start is called before the first frame update
        void Start()
        {
            placedGenerators = new List<GeneratorBehavior>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public bool HasObjectOfType(GeneratorType type)
        {
            return placedGenerators.Any(item => item.type == type);
        }
    }
}
