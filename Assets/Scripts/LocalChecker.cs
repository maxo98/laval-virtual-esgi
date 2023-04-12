using Photon.Pun;
using UnityEngine;

public class LocalChecker : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private GameObject camera;
    [SerializeField] private bool isVr;
    [SerializeField] private GameObject leftHand, rightHand;
    void Start()
    {
        if (photonView.IsMine)
        {
            camera.SetActive(true);
        }
        else
        {
            leftHand.SetActive(isVr);
            rightHand.SetActive(isVr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
