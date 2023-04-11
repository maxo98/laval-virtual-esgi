using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Scenes
    {
        Default = 0,
        Game = 1,
        Lobby = 2
    }

    private int _currentScene;
    private GameManagerScriptableObject _gameManagerScriptableObjectScriptableObject;
    
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameManagerScriptableObjectScriptableObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {
        if (PhotonNetwork.IsMasterClient)
            _currentScene = (int)Scenes.Game;

        SceneManager.LoadScene((int)Scenes.Game);
    }
    
    public void LoadLobby()
    {
        if (PhotonNetwork.IsMasterClient)
            _currentScene = (int)Scenes.Lobby;
        
        SceneManager.LoadScene((int)Scenes.Lobby);
    }
    
    public void LoadDefaultScene()
    {
        if (PhotonNetwork.IsMasterClient)
            _currentScene = (int)Scenes.Default;
        
        SceneManager.LoadScene((int)Scenes.Default);
    }
    
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(_currentScene);
    }
    
    public void CreateScriptableObject()
    {
        _gameManagerScriptableObjectScriptableObject = ScriptableObject.CreateInstance<GameManagerScriptableObject>();
    }
}
