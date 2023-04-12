using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private Material mat;
    public Transform originSpawn;
    public bool inPlaceHolder = false;
    public Transform lastPlaceHolder;


    public void OnPickUp()
    {
        GetComponent<Renderer>().material.color = Color.green;
        transform.parent = null;
        originSpawn.GetComponent<placeHolder>().spawned = false;
    }

    public void OnDrop()
    {
        if (inPlaceHolder)
        {
            transform.parent = lastPlaceHolder;
            transform.localPosition = Vector3.zero;
            transform.localRotation = quaternion.identity;
            originSpawn.GetComponent<placeHolder>().SpawnItem();
        }
        else
        {
            Destroy(gameObject);
            originSpawn.GetComponent<placeHolder>().SpawnItem();
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("placeHolder"))
        {
            GetComponent<Renderer>().material.color = Color.blue;
            inPlaceHolder = true;
            lastPlaceHolder = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("placeHolder"))
        {
            GetComponent<Renderer>().material.color = Color.black;
            inPlaceHolder = false;
            lastPlaceHolder = null;
        }
    }


}
