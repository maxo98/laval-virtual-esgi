using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HandDisable : MonoBehaviour
{
    [SerializeField] private PhotonView pv;

    [SerializeField] private GameObject handToAttach;

// Start is called before the first frame update
    void Start()
    {
        if (!pv.IsMine)
        {
            handToAttach.transform.SetParent(transform);
        }
        else
        {
            handToAttach.SetActive(false);

        }
    }
}
