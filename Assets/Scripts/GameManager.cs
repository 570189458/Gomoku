﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Player> Player = new List<Player>();

    private void Awake()
    {
        int Player1 = PlayerPrefs.GetInt("Player1");
        int Player2 = PlayerPrefs.GetInt("Player2");
        for (int i = 0; i < Player.Count; i++)
        {
            if(Player1==i)
            {
                Player[i].chessColor = ChessType.Black;
            }
            else if(Player2==i)
            {
                Player[i].chessColor = ChessType.White;
            }
            else
            {
                Player[i].chessColor = ChessType.Watch;
            }
        }
    }

    public void SetPlayer1(int i)
    {
        PlayerPrefs.SetInt("Player1", i);
    }

    public void SetPlayer2(int i)
    {
        PlayerPrefs.SetInt("Player2", i);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void NetGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeChessColor()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            if(Player[i].chessColor==ChessType.Black)
            {
                SetPlayer2(i);
            }
            else if(Player[i].chessColor==ChessType.White)
            {
                SetPlayer1(i);
            }
            else
            {
                Player[i].chessColor = ChessType.Watch;
            }
        }
        SceneManager.LoadScene(1);
    }

    public void DoubleMode()
    {
        PlayerPrefs.SetInt("Double", 10);
    }

    public void OnReturnButton()
    {
        PlayerPrefs.SetInt("Double", 1);
        SceneManager.LoadScene(0);
    }

    public void onRealyBtn()
    {
        SceneManager.LoadScene(1);
    }
}
