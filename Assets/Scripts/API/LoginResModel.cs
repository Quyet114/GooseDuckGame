using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginResModel : MonoBehaviour
{
    public LoginResModel(int status, string notification, string username, string score, string positionX, string positionY, string positionZ)
    {
        this.status = status;
        this.notification = notification;
        this.username = username;
        this.score = score;
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;
    }

    public int status { get; set; }
    public string notification { get; set; }
    public string username { get; set; }
    public string score { get; set; }
    public string positionX { get; set; }
    public string positionY { get; set; }
    public string positionZ { get; set; }
}
