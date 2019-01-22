using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public ChessType chessColor = ChessType.Black;
    bool isDoubleMode = false;
    private Button btn;

    protected virtual void Start()
    {
        btn = GameObject.Find("BTN_RetractChess").GetComponent<Button>();
        if (PlayerPrefs.GetInt("Double") == 10)
            isDoubleMode = true;
        else
            isDoubleMode = false;
    }

    protected virtual void FixedUpdate()
    {
        if(chessColor==ChessBoard.Instance.turn&&ChessBoard.Instance.timer>0.2f)
        {
            PlayChess();
        }
        if(!isDoubleMode)
            ChangeButtonColor();
    }

    public virtual void PlayChess()
    {
        if(Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log((int)(pos.x + 7.5f) + " " + (int)(pos.y + 7.5f));
            if (ChessBoard.Instance.PlayerChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) }))
            {
                ChessBoard.Instance.timer = 0;
            }
        }
    }

    protected virtual void ChangeButtonColor()
    {
        if (chessColor == ChessType.Watch)
            return;
        if (ChessBoard.Instance.turn == chessColor)
            btn.interactable = true;
        else
            btn.interactable = false;
    }
}
