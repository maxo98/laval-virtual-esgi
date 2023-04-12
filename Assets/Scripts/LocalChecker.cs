using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LocalChecker : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private GameObject camera;
    
    void Start()
    {
        if (photonView.IsMine)
        {
            camera.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
