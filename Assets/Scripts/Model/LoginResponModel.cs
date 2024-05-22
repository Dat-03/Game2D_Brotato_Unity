using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoginResponModel
{
    public LoginResponModel(string userName, int status, string notification, int id_mission, int coin)
    {
        this.userName = userName;
        this.status = status;
        this.notification = notification;
        this.id_mission = id_mission;
        this.coin = coin;
    }

    public string userName {  get; set; }

    public int status { get; set; }
    public string notification { get; set; }
    public int id_mission { get; set; }
    public int coin { get; set; }
    /*
    public string map { get; set; }
    public string mission { get; set; }
    public int score { get; set; }
    public int coin { get; set; }
    */


}
