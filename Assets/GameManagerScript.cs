using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    int[,] map;

    void PrintArray()
    {
        string debugText = "";

        for (int y = 0; y < map.GetLength(0); ++y)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";
        }

        Debug.Log(debugText);
    }

    int GetPlayerIndexX()
    {
        for (int y = 0; y < map.GetLength(0); ++y)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    return x;
                }
            }
        }

        return -1;
    }

    int GetPlayerIndexY()
    {
        for (int y = 0; y < map.GetLength(0); ++y)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    return y;
                }
            }
        }

        return -1;
    }

    bool MoveNumber(int number, int moveFromX, int moveToX, int moveFromY, int moveToY)
    {
        if (moveToY < 0 || moveToY >= map.GetLength(0) || moveToX < 0 || moveToX >= map.GetLength(1))
        {
            return false;
        }
        if (map[moveToX, moveToX] == 2)
        {
            int verocityX = moveToX - moveFromX;
            int verocityY = moveToY - moveFromY;

            bool sucess = MoveNumber(2, moveToX, moveToX + verocityX, moveToY, moveToY + verocityY);

            if (!sucess) { return false; }
        }

        map[moveToY, moveToX] = number;
        map[moveFromY, moveFromX] = 0;

        return true;
    }

    void SetPlayerPos(int x, int y)
    {
        map[y, x] = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new int[,] 
        { 
            { 0,0,0,0,0,0,0,0 },
            { 0,2,0,0,0,0,0,0 },
            { 0,0,0,0,2,0,0,0 },
            { 0,0,2,0,0,0,0,0 },
            { 0,0,0,0,2,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,2,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 }
        };

        SetPlayerPos(4, 4);

        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        int playerIndexX = GetPlayerIndexX();
        int playerIndexY = GetPlayerIndexY();

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveNumber(1, playerIndexX, playerIndexX + 1, playerIndexY, playerIndexY);
            PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveNumber(1, playerIndexX, playerIndexX - 1, playerIndexY, playerIndexY);
            PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveNumber(1, playerIndexX, playerIndexX, playerIndexY, playerIndexY+1);
            PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveNumber(1, playerIndexX, playerIndexX, playerIndexY, playerIndexY-1);
            PrintArray();
        }
    }
}
