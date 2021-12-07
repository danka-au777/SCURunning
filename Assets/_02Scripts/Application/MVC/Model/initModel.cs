using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initModel : Model
{
    public override string M_Name
    {
        get
        {
            return Consts.M_InitModel;
        }
    }
    private string username="hello";

    public string Username
    {
        get
        {
            return username;
        }

        set
        {
            username = value;
        }
    }

    private int maxMileage=0;
    public int MaxMileage
    {
        get
        {
            return maxMileage;
        }
        set
        {
            maxMileage = value;
        }
    }

    private int coin=5000;
    public int Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
        }
    }

    private float music=0.5f;
    public float Music
    {
        get
        {
            return music;
        }
        set
        {
            music = value;
        }
    }

    private static int difficulty=1;
    public int Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }

    private int magenetNum=0;
    public int MagenetNum
    {
        get
        {
            return magenetNum;
        }
        set
        {
            magenetNum = value;
        }
    }

    private int protectionNum=0;
    public int ProtectionNum
    {
        get
        {
            return protectionNum;
        }
        set
        {
            protectionNum = value;
        }
    }

    private int invincibleNum=0;
    public int InvincibleNum
    {
        get
        {
            return invincibleNum;
        }
        set
        {
            invincibleNum = value;
        }
    }

    private int doublecoinNum=0;
    public int DoublecoinNum
    {
        get
        {
            return doublecoinNum;
        }
        set
        {
            doublecoinNum = value;
        }
    }

    private int ownSkin1=1;
    public int OwnSkin1
    {
        get
        {
            return ownSkin1;
        }
        set
        {
            ownSkin1 = value;
        }
    }

    private int ownSkin2=0;
    public int OwnSkin2
    {
        get
        {
            return ownSkin2;
        }
        set
        {
            ownSkin2 = value;
        }
    }

    private int ownSkin3=0;
    public int OwnSkin3
    {
        get
        {
            return ownSkin3;
        }
        set
        {
            ownSkin3 = value;
        }
    }
    private int onSkin=0;
    public int Onskin
    {
        get
        {
            return onSkin;
        }
        set
        {
            onSkin = value;
        }
    }

    public void SaveAll()
    {
        Debug.Log("Saveall");
        MVC.SendEvent(Consts.E_SaveAll);
    }
}