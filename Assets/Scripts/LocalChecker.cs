using Photon.Pun;
using UnityEngine;

public class LocalChecker : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Camera camera;
    [SerializeField] private bool isVr;
    [SerializeField] private GameObject leftHand, rightHand;
    void Start()
    {
        if (photonView.IsMine)
        {
            camera.enabled = true;
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
