using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        private static NetworkManager instance = null;
        public static NetworkManager Instance => instance;

        #region Private Fields

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "0.1";
        #endregion


        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
        
            instance = this;
            PhotonNetwork.AutomaticallySyncScene = true;
            DontDestroyOnLoad(instance);
        }
        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
                GameManager.Instance.LoadLobby();
            else
                GameManager.Instance.LoadCurrentScene();
            //photonView.RPC("TurnOnLight",RpcTarget.All,"vrai");
        }

        private void LeftRoom()
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LeaveRoom();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            if (PhotonNetwork.CreateRoom("Test", new RoomOptions()))
            {
                
            }
#endif
        }
    
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}",
                cause);
        }

        #endregion

        #region Public Methods


    
        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        #endregion

        [PunRPC]
        private void TurnOnLight(string value)
        {
            //en fonction de la valeur, allumer ou eteindre la lumiere 
        }

        public void ChangeSceneToMain()
        {
            // if (PhotonNetwork.IsMasterClient)
            //     PhotonNetwork.LeaveRoom();
            GameManager.Instance.LoadDefaultScene();
        }

        public void ChangeSceneToLobby()
        {
            GameManager.Instance.LoadLobby();
        }

        public void ChangeSceneToGame()
        {
            GameManager.Instance.LoadGameScene();
        }
        
    }
}