using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particuleControler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particule;

    public void StartParticule()
    {
        particule.Play();
    }
    
    public void StopParticule()
    {
        particule.Stop();
    }
}
