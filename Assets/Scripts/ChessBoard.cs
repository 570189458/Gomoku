using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoard : MonoBehaviour {

    static ChessBoard _instance;

    public ChessType turn = ChessType.Black;
    public int[,] grid;
    public GameObject[] prefabs;
    public float timer = 0;
    public bool GameStart = false;
    Transform parent;
    public Stack<Transform> chessStack = new Stack<Transform>();
    public Text winner;

    public static ChessBoard Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(Instance==null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        parent = GameObject.Find("Parent").transform;
        grid = new int[15, 15];
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    public bool PlayerChess(int[] pos)
    {


        if (!GameStart)
            return false;
        pos[0] = Mathf.Clamp(pos[0], 0, 14);
        pos[1] = Mathf.Clamp(pos[1], 0, 14);

        if (grid[pos[0], pos[1]] != 0)
            return false;
        if(turn==ChessType.Black)
        {
            GameObject go = Instantiate(prefabs[0], new Vector2(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(go.transform);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 1;
            //判断胜利
            if(CheckWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.White;
        }
        else if(turn==ChessType.White)
        {
            GameObject go = Instantiate(prefabs[1], new Vector2(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(go.transform);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 2;
            //判断胜利
            if (CheckWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.Black;
        }
        return true;
    }

    void GameEnd()
    {
        winner.transform.parent.parent.gameObject.SetActive(true);
        switch (turn)
        {
            case ChessType.Watch:
                break;
            case ChessType.Black:
                winner.text = "黑棋胜";
                break;
            case ChessType.White:
                winner.text = "白棋胜";
                break;
        }
        GameStart = false;
        Debug.Log(turn + "win");
    }

    public bool CheckWinner(int[] pos)
    {
        if (CheckOneLine(pos, new int[2] { 1, 0 })) return true;
        if (CheckOneLine(pos, new int[2] { 0, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, -1 })) return true;
        return false;
    }

    public bool CheckOneLine(int[] pos,int[] offset)
    {
        int linkNum = 1;
        //右边
        for(int i=offset[0],j=offset[1];(pos[0]+i>=0&&pos[0]+i<15)&& (pos[1] + j >= 0 && pos[1] + j < 15);i+=offset[0],j+=offset[1])
        {
            if(grid[pos[0]+i,pos[1]+j]==(int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        }
        //左边
        for (int i = -offset[0], j = -offset[1]; (pos[0] + i >= 0 && pos[0] + i < 15) && (pos[1] + j >= 0 && pos[1] + j < 15); i -= offset[0], j -= offset[1])
        {
            if (grid[pos[0] + i, pos[1] + j] == (int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        }
        if (linkNum > 4)
            return true;
        return false;
    }

    public void RetractChess()
    {
        if(chessStack.Count>1)
        {
            Transform pos = chessStack.Pop();
            grid[(int)pos.position.x + 7, (int)pos.position.y + 7] = 0;
            Destroy(pos.gameObject);
            pos = chessStack.Pop();
            grid[(int)pos.position.x + 7, (int)pos.position.y + 7] = 0;
            Destroy(pos.gameObject);
        }
    }
}

public enum ChessType
{
    Watch,
    Black,
    White
}
