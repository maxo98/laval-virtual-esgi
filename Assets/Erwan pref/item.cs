using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class item : MonoBehaviourPunCallbacks
{
    [SerializeField] private Material mat;
    public Transform originSpawn;
    public bool inPlaceHolder = false;
    public Transform lastPlaceHolder;
    public GameObject bigModel;


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
            GameObject temp = PhotonNetwork.Instantiate(this.name.Replace("(Clone)", ""), transform.position, transform.rotation);
            temp.transform.parent = lastPlaceHolder;
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localRotation = quaternion.identity;
            originSpawn.GetComponent<placeHolder>().SpawnItem();
            Destroy(gameObject);
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
