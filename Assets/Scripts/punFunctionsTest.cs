using Photon.Pun;
using UnityEngine;

public class punFunctionsTest : MonoBehaviour
{
    // should be called when an item is grapped from the map.
    // change state of an item
    [PunRPC]
    private void SelectItem(int id)
    {}
    
    // should be called when an item is placed.
    // Add a new item on the master board
    [PunRPC]
    private void AddItem(int id)
    {}

    // should be called when an item is removed
    // removed an item from a map
    [PunRPC]
    private void RemoveItem(int id)
    {}
    
    // should be called when the map state is changed. (sun / night, rain / shin, ...)
    // Force every player to change the state of the map.
    [PunRPC]
    private void ChangeMapState(int id)
    {}
}
