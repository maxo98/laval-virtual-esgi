using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class placeHolder : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject actualObj;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private bool isSpawn;
    public bool spawned = false;
    void Start()
    {
        if (isSpawn)
        {
            SpawnItem();
        }
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("item"))
    //     {
    //         item _item = other.GetComponent<item>();
    //         other.GetComponent<Renderer>().material.color = Color.blue;
    //         _item.inPlaceHolder = true;
    //         _item.lastPlaceHolder = transform;
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("item"))
    //     {
    //         item _item = other.GetComponent<item>();
    //         other.GetComponent<Renderer>().material.color = Color.black;
    //         _item.inPlaceHolder = false;
    //         _item.lastPlaceHolder = null;
    //     }
    // }

    public void SpawnItem()
    {
        if (!spawned)
        {
            GameObject temp = Instantiate(itemPrefab);
            temp.transform.parent = spawnPos;
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localRotation = quaternion.identity;
            temp.GetComponent<item>().originSpawn = transform;
            spawned = true;
        }

    }
    
    
}
