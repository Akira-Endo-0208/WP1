using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Canvas gameClearCanvas;
    bool isGameClear = false;
    bool isGameOver = false;

    public void GameClear()
    {
        if (isGameClear || isGameOver)
        {
            return;
        }
        gameClearCanvas.enabled = true;

        isGameClear = true;

    }
}
