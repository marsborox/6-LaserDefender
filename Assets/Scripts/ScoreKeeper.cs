using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{//doesnt has to be SF
 //but sometimes we might want to test this one day
    [SerializeField] int score;

    public void ModifyScore(int value)
    {
        score +=  value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }
    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    { 
        return score;
    }
}
