using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionModel 
{
    public PositionModel(string email, string positionX, string positionY, string positionZ)
    {
        this.email = email;
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;
    }

    public string email { get; set; }
    public string positionX { get; set; }
    public string positionY { get; set; }
    public string positionZ { get; set; }

}
