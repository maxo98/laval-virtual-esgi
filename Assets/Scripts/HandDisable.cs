using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HandDisable : MonoBehaviour
{
    [SerializeField] private PhotonView pv;

    public bool isLeft;
// Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine)
        {
            if (isLeft)
            {
                transform.Find("LeftHand").gameObject.SetActive(false);

            }
            else
            {
                transform.Find("RightHand").gameObject.SetActive(false);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
