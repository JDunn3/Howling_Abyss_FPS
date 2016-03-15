using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

    public static string getOpposingTeam(string team)
    {
        if(team == "Red")
            return "Blue";
        else if(team == "Blue")
            return "Red";
        else
            Debug.LogError("Dunno.. something happened with the teams tags.");
        return "Uh oh";
    }

}
