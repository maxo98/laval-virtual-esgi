using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> playerPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE_WIN
        PhotonNetwork.Instantiate(playerPrefabs[0].name, new Vector3(0, 0, 0), new Quaternion());
#else
            PhotonNetwork.Instantiate(playerPrefabs[1].name, new Vector3(0, 0, 0), new Quaternion());
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
