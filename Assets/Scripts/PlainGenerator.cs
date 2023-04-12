using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainGenerator : MonoBehaviour
{
    public GameObject tree;
    public Vector3 scale;
    public int nTrees;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nTrees; i++)
        {
            Quaternion quat = new Quaternion();

            quat.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

            Instantiate(tree, new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f)), quat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
