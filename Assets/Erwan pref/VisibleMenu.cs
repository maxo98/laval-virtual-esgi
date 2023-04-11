using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleMenu : MonoBehaviour
{

    [SerializeField] private LayerMask interractMask;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, interractMask))
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
    }
}
