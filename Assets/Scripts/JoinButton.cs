using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour {

    NetworkManager manager;

    public MatchInfoSnapshot info;

    public Text nametext;

    private void Start()
    {
        manager = NetworkManager.singleton;
        if (manager.matchMaker == null)
            manager.StartMatchMaker();
    }

    public void SetUp(MatchInfoSnapshot _info)
    {
        info = _info;
        nametext.text = info.name;
    }

    public void OnJoinButton()
    {
        manager.matchMaker.JoinMatch(info.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
    }
}
