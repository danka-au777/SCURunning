using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initController : Controller
{
    public override void Execute(object data)
    {
        initModel initmodel = MVC.GetModel<initModel>();
        GameModel gm = MVC.GetModel<GameModel>();
        if (!PlayerPrefs.HasKey("username"))
        {
            //Debug.Log("empty");
            //Debug.Log(initmodel.Username + "!!!");
            PlayerPrefs.SetString("username", initmodel.Username);
        //PlayerPrefs.Save();
        }
        //string aaa = PlayerPrefs.GetString("username");
        initmodel.Username = PlayerPrefs.GetString("username");
        Debug.Log("initusername" + initmodel.Username);
        Debug.Log(initmodel.Username+"???");

        if (!PlayerPrefs.HasKey("maxMileage"))
        {
            PlayerPrefs.SetInt("maxMileage", initmodel.MaxMileage);
        }
        initmodel.MaxMileage = PlayerPrefs.GetInt("maxMileage");

        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", initmodel.Coin);
        }
        initmodel.Coin = PlayerPrefs.GetInt("coin");
        gm.M_Coin = PlayerPrefs.GetInt("coin");

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", initmodel.Music);
        }
        initmodel.Music = PlayerPrefs.GetFloat ("music");

        if (!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetInt("difficulty", initmodel.Difficulty);
        }
        initmodel.Difficulty = PlayerPrefs.GetInt("difficulty");
        Debug.Log("initdifficulty" + initmodel.Difficulty);

        if (!PlayerPrefs.HasKey("magenetNum"))
        {
            PlayerPrefs.SetInt("magenetNum", initmodel.MagenetNum);
        }
        initmodel.MagenetNum = PlayerPrefs.GetInt("magenetNum");
        gm.M_Magnet = PlayerPrefs.GetInt("magenetNum");

        if (!PlayerPrefs.HasKey("protectionNum"))
        {
            PlayerPrefs.SetInt("protectionNum", initmodel.ProtectionNum);
        }
        initmodel.ProtectionNum = PlayerPrefs.GetInt("protectionNum");
        

        if (!PlayerPrefs.HasKey("invincibleNum"))
        {
            PlayerPrefs.SetInt("invincibleNum", initmodel.InvincibleNum);
        }
        initmodel.InvincibleNum = PlayerPrefs.GetInt("invincibleNum");
        gm.M_Invincible = PlayerPrefs.GetInt("invincibleNum");

        if (!PlayerPrefs.HasKey("doublecoinNum"))
        {
            PlayerPrefs.SetInt("doublecoinNum", initmodel.DoublecoinNum);
        }
        initmodel.DoublecoinNum = PlayerPrefs.GetInt("doublecoinNum");
        gm.M_Multiply = PlayerPrefs.GetInt("doublecoinNum");

        if (!PlayerPrefs.HasKey("ownSkin1"))
        {
            PlayerPrefs.SetInt("ownSkin1", initmodel.OwnSkin1);
        }
        initmodel.OwnSkin1 = PlayerPrefs.GetInt("ownSkin1");

        if (!PlayerPrefs.HasKey("ownSkin2"))
        {
            PlayerPrefs.SetInt("ownSkin2", initmodel.OwnSkin2);
        }
        initmodel.OwnSkin2 = PlayerPrefs.GetInt("ownSkin2");

        if (!PlayerPrefs.HasKey("ownSkin3"))
        {
            PlayerPrefs.SetInt("ownSkin3", initmodel.OwnSkin3);
        }
        initmodel.OwnSkin3 = PlayerPrefs.GetInt("ownSkin3");

        if (!PlayerPrefs.HasKey("onSkin"))
        {
            PlayerPrefs.SetInt("onSkin", initmodel.Onskin);
        }
        initmodel.Onskin = PlayerPrefs.GetInt("onSkin");
        gm.M_TakeOnSkin = PlayerPrefs.GetInt("onSkin") - 1;
        Debug.Log("initonskin" + initmodel.Onskin);

        PlayerPrefs.Save();
    }
}
