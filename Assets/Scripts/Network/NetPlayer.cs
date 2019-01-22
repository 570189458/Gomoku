using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class NetPlayer : NetworkBehaviour
{

    [SyncVar]
    public ChessType chessColor = ChessType.Black;
    public bool isDoubleMode = false;
    private Button btn;

    void Start()
    {
        if (isLocalPlayer)
        {
            CmdSetPlayer();
            if (chessColor == ChessType.Watch)
                return;
            btn = GameObject.Find("BTN_RetractChess").GetComponent<Button>();
            btn.onClick.AddListener(RetractBtn);
        }
    }

    void FixedUpdate()
    {
        if (chessColor == NetChessBoard.Instance.turn && NetChessBoard.Instance.timer > 0.2f && isLocalPlayer && NetChessBoard.Instance.PlayerNumber > 1)
        {
            PlayChess();
        }
        if (chessColor != ChessType.Watch && isLocalPlayer && !NetChessBoard.Instance.GameStart)
        {
            NetChessBoard.Instance.GameEnd();
        }
        if(chessColor!=ChessType.Watch&&isLocalPlayer)
        {
            ChangeButtonColor();
        }
    }

    public virtual void PlayChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log((int)(pos.x + 7.5f) + " " + (int)(pos.y + 7.5f));
            CmdChess(pos);
        }
    }

    [Command]
    public void CmdChess(Vector2 pos)
    {
        if (NetChessBoard.Instance.PlayerChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) }))
        {
            NetChessBoard.Instance.timer = 0;
        }
    }

    protected virtual void ChangeButtonColor()
    {
        if (chessColor == ChessType.Watch)
            return;
        if (NetChessBoard.Instance.turn == chessColor)
            btn.interactable = true;
        else
            btn.interactable = false;
    }

    [Command]
    public void CmdSetPlayer()
    {
        NetChessBoard.Instance.PlayerNumber++;
        if (NetChessBoard.Instance.PlayerNumber == 1)
            chessColor = ChessType.Black;
        else if (NetChessBoard.Instance.PlayerNumber == 2)
            chessColor = ChessType.White;
        else
            chessColor = ChessType.Watch;
    }

    public void RetractBtn()
    {
        CmdRetractButton();
    }

    [Command]
    public void CmdRetractButton()
    {
        NetChessBoard.Instance.RetractChess();
    }
}
