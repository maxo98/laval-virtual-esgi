using System;
using Network;
using UnityEngine;

namespace Scenes
{
    public class LobbyManager : MonoBehaviour
    {
        public void OnClickToMainMenu()
        {
            NetworkManager.Instance.ChangeSceneToMain();
        }
        
        public void OnClickToGame()
        {
            NetworkManager.Instance.ChangeSceneToGame();
        }
    }
}
