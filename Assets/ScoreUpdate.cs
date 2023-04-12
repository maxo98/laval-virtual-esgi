using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshPro text;


    [PunRPC]
    public void ShareConso(int conso)
    {
        text.text = conso.ToString();
    }
}
