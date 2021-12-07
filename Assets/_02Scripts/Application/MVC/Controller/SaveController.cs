using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : Controller
{
    GameModel gm = MVC.GetModel<GameModel>();
    public override void Execute(object data)
    {
        Debug.Log("is saving");
        initModel initmodel = MVC.GetModel<initModel>();

        PlayerPrefs.SetString("username", initmodel.Username);

        if (initmodel.MaxMileage > PlayerPrefs.GetInt("maxMileage"))
        {
            PlayerPrefs.SetInt("maxMileage", initmodel.MaxMileage);
        }//改了

        PlayerPrefs.SetInt("coin", gm.M_Coin);//改了

        PlayerPrefs.SetFloat("music", initmodel.Music);
        PlayerPrefs.SetInt("difficulty", initmodel.Difficulty);
        Debug.Log("savingsetting" + initmodel.Difficulty);
        PlayerPrefs.SetInt("magenetNum", gm.M_Magnet);
        PlayerPrefs.SetInt("protectionNum", initmodel.ProtectionNum);
        PlayerPrefs.SetInt("invincibleNum", gm.M_Invincible);
        PlayerPrefs.SetInt("doublecoinNum", gm.M_Multiply);
        PlayerPrefs.SetInt("ownSkin1", initmodel.OwnSkin1);
        PlayerPrefs.SetInt("ownSkin2", initmodel.OwnSkin2);
        PlayerPrefs.SetInt("ownSkin3", initmodel.OwnSkin3);
        PlayerPrefs.SetInt("onSkin", initmodel.Onskin);
        PlayerPrefs.Save();
        string str = PlayerPrefs.GetString("username");
        Debug.Log("saved username" + str);
    }
}
