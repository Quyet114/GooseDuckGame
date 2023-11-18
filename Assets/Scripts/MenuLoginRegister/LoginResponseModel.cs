using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginResponseModel 
{
    public int status { get; set; }
    public string notification { get; set; }
    public string username { get; set;}
    public int score { get; set; }
    public string postitionX { get; set; }
    public string postitionY { get; set; }
    public string postitionZ { get; set; }

    public LoginResponseModel(int status, string notification, string username, int score, string postitionX, string postitionY, string postitionZ)
        {
            this.status = status;
            this.notification = notification;
            this.username = username;
            this.score = score;
            this.postitionX = postitionX;
            this.postitionY = postitionY;
            this.postitionZ = postitionZ;
        }
}
