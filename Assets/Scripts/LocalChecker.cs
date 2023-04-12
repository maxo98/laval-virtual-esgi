using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Hands.Samples.VisualizerSample;

public class LocalChecker : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Camera camera;
    [SerializeField] private bool isVr;
    [SerializeField] private GameObject leftHand, rightHand;
    [SerializeField] private TrackedPoseDriver _trackedPoseDriver;
    [SerializeField] private HandVisualizer _handVisualizer;
    
    void Start()
    {
        if (photonView.IsMine)
        {
            camera.enabled = true;
            _trackedPoseDriver.enabled = isVr;
            _handVisualizer.enabled = isVr;
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
