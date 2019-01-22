using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetWorkUI : NetworkBehaviour {

    public void StartHost()
    {
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.singleton.networkAddress = GameObject.Find("IP").GetComponent<InputField>().text;
        NetworkManager.singleton.StartClient();
    }

    public void Stop()
    {
        NetworkManager.singleton.StopHost();
    }

    public void OfflineSet()
    {
        GameObject.Find("Host").GetComponent<Button>().onClick.AddListener(StartHost);
        GameObject.Find("Client").GetComponent<Button>().onClick.AddListener(StartClient);
    }

    public void OnlineSet()
    {
        GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(Stop);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        //switch (scene.buildIndex)
        //{
        //    case 0:
        //        OfflineSet();
        //        break;
        //    case 1:
        //        OnlineSet();
        //        break;
        //    default:
        //        break;
        //}
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
