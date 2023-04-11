using Network;
using UnityEngine;

namespace Scenes
{
    public class MainManager : MonoBehaviour
    {

        public void OnClickToLobby()
        {
            NetworkManager.Instance.Connect();
        }

        public void OnClickToQuit()
        {
            
        }
    }
}
