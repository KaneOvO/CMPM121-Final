using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Savedata
{
    public LandArea landArea;
    public int currentTurn;

    public int numberOfCarrot;
    public int numberOfCabbage;
    public int numberOfOnion;

    public Savedata(LandArea landArea, int currentTurn, int numberOfCarrot, int numberOfCabbage, int numberOfOnion)
    {
        this.landArea = new LandArea(landArea);

        this.currentTurn = currentTurn;
        this.numberOfCarrot = numberOfCarrot;
        this.numberOfCabbage = numberOfCabbage;
        this.numberOfOnion = numberOfOnion;
    }

    public Savedata(Savedata other)
    {
        this.landArea = new LandArea(other.landArea);

        this.currentTurn = other.currentTurn;
        this.numberOfCarrot = other.numberOfCarrot;
        this.numberOfCabbage = other.numberOfCabbage;
        this.numberOfOnion = other.numberOfOnion;
    }
   
}
