using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionManager : MonoBehaviour
{
    public float[] racer_Positions;
    public GameObject player;
    public float playerPosition;
    public GameObject[] ai;
    public int currentPos, currentPoint;
    public Text posText;


    void Update()
    {
        PositionCalculate();
        posText.text = currentPos.ToString() + " / " + racer_Positions.Length;
    }

    public void PositionCalculate()
    {
        racer_Positions[0] = player.GetComponent<CharecterMove>().playerDistance;
        racer_Positions[1] = ai[0].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[2] = ai[1].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[3] = ai[2].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[4] = ai[3].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[5] = ai[4].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[6] = ai[5].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[7] = ai[6].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[8] = ai[7].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[9] = ai[8].GetComponent<OpponentMove>().opponentDistance;
        racer_Positions[10] = ai[9].GetComponent<OpponentMove>().opponentDistance;

        playerPosition = player.GetComponent<CharecterMove>().playerDistance;

        System.Array.Sort(racer_Positions);

        int x = System.Array.IndexOf(racer_Positions, playerPosition);

        switch (x)
        {
            case 0:
                currentPos = 1;
                break;
            case 1:
                currentPos = 2;
                break;
            case 2:
                currentPos = 3;
                break;
            case 3:
                currentPos = 4;
                break;
            case 4:
                currentPos = 5;
                break;
            case 5:
                currentPos = 6;
                break;
            case 6:
                currentPos = 7;
                break;
            case 7:
                currentPos = 8;
                break;
            case 8:
                currentPos = 9;
                break;
            case 9:
                currentPos = 10;
                break;
            case 10:
                currentPos = 11;
                break;
        }
    }
}
