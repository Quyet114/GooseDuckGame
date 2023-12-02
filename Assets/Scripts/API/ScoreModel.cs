using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel 
{
    public ScoreModel(string email, int score)
    {
        this.email = email;
        this.score = score;
    }

    public string email { get; set; }
    public int score { get; set; }
}
