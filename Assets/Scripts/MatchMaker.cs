using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MatchMaker : MonoBehaviour {

    NetworkManager manager;

    public string RoomName;

    List<GameObject> roomList = new List<GameObject>();

    public GameObject btn;

    public Transform parent;

    private void Start()
    {
        manager = NetworkManager.singleton;
        if (manager.matchMaker == null)
            manager.StartMatchMaker();
    }

    public void SetRoomName(string name)
    {
        RoomName = name;
    }

    public void CreatRoom()
    {
        manager.matchMaker.CreateMatch(RoomName, 3, true, "", "", "", 0, 0, manager.OnMatchCreate);
    }

    public void OnRefash()
    {
        manager.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
    }

    public void OnMatchList(bool success,string extendedInfo,List<MatchInfoSnapshot> matches)
    {
        if(!success)
        {
            return;
        }
        ClearList();
        foreach (var match in matches)
        {
            GameObject go = Instantiate(btn, parent);
            roomList.Add(go);
            go.GetComponent<JoinButton>().SetUp(match);
        }


    }

    void ClearList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }
}
