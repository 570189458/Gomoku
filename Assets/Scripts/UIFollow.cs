using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(ChessBoard.Instance.chessStack.Count>0)
        {
            transform.position = ChessBoard.Instance.chessStack.Peek().position;
        }
	}

    public void onRealyBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnBtn()
    {
        SceneManager.LoadScene(0);
    }
}
