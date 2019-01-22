using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILevelOne : Player
{

    protected Dictionary<string, float> toScore = new Dictionary<string, float>();

    protected float[,] Score = new float[15, 15];

    protected override void Start()
    {
        toScore.Add("_aa_", 100);
        toScore.Add("aa_", 50);
        toScore.Add("_aa", 50);

        toScore.Add("_aaa_", 1000);
        toScore.Add("aaa_", 500);
        toScore.Add("_aaa", 500);

        toScore.Add("_aaaa_", 10000);
        toScore.Add("aaaa_", 5000);
        toScore.Add("_aaaa", 5000);

        toScore.Add("aaaaa", float.MaxValue);
        toScore.Add("aaaaa_", float.MaxValue);
        toScore.Add("_aaaaa", float.MaxValue);
        toScore.Add("_aaaaa_", float.MaxValue);
    }

    public virtual void CheckOneLine(int[] pos, int[] offset, int chess)
    {
        string str = "a";
        //右边
        for (int i = offset[0], j = offset[1]; pos[0] + i >= 0 && pos[0] + i < 15 && pos[1] + j >= 0 && pos[1] + j < 15; i += offset[0], j += offset[1])
        {
            if (ChessBoard.Instance.grid[pos[0] + i, pos[1] + j] == chess)
            {
                str += "a";
            }
            else if (ChessBoard.Instance.grid[pos[0] + i, pos[1] + j] == 0)
            {
                str += "_";
                break;
            }

            else
            {
                break;
            }
        }
        //左边
        for (int i = -offset[0], j = -offset[1]; (pos[0] + i >= 0 && pos[0] + i < 15) && (pos[1] + j >= 0 && pos[1] + j < 15); i -= offset[0], j -= offset[1])
        {
            if (ChessBoard.Instance.grid[pos[0] + i, pos[1] + j] == chess)
            {
                str = "a" + str;
            }
            else if (ChessBoard.Instance.grid[pos[0] + i, pos[1] + j] == 0)
            {
                str = "_" + str;
                break;
            }
            else
            {
                break;
            }
        }

        if (toScore.ContainsKey(str))
        {
            Score[pos[0], pos[1]] += toScore[str];
        }
    }

    public void SetScore(int[] pos)
    {
        Score[pos[0], pos[1]] = 0;

        CheckOneLine(pos, new int[2] { 1, 0 }, 1);
        CheckOneLine(pos, new int[2] { 1, 1 }, 1);
        CheckOneLine(pos, new int[2] { 1, -1 }, 1);
        CheckOneLine(pos, new int[2] { 0, 1 }, 1);

        CheckOneLine(pos, new int[2] { 1, 0 }, 2);
        CheckOneLine(pos, new int[2] { 1, 1 }, 2);
        CheckOneLine(pos, new int[2] { 1, -1 }, 2);
        CheckOneLine(pos, new int[2] { 0, 1 }, 2);
    }

    public override void PlayChess()
    {
        if (ChessBoard.Instance.chessStack.Count == 0)
        {
            if (ChessBoard.Instance.PlayerChess(new int[2] { 7, 7 }))
            {
                ChessBoard.Instance.timer = 0;
            }
            return;
        }

        float maxScore = 0;
        int[] maxPos = new int[2] { 0, 0 };
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (ChessBoard.Instance.grid[i, j] == 0)
                {
                    SetScore(new int[2] { i, j });
                    if (Score[i, j] >= maxScore)
                    {
                        maxPos[0] = i;
                        maxPos[1] = j;
                        maxScore = Score[i, j];
                    }
                }
            }
        }

        if (ChessBoard.Instance.PlayerChess(maxPos))
        {
            ChessBoard.Instance.timer = 0;
        }
    }

    protected override void ChangeButtonColor()
    {
        
    }
}
