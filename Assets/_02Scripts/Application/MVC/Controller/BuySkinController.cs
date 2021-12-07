using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySkinController : Controller
{
    public override void Execute(object data)
    {
        BuySkinClothFootballArgs e = data as BuySkinClothFootballArgs;
        GameModel gm = Game.M_Instance.M_GM;
        initModel im = MVC.GetModel<initModel>();

        UIShop shop = MVC.GetView<UIShop>();
        if (gm.GetMoney(e.M_Coin))
        {
            im.Coin = gm.M_Coin;
            gm.M_BuySkin.Add(e.M_ID);
            //shop.UpdateUI();
        }

        for (int i = 0; i < gm.M_BuySkin.Count ; i++)
        {
            int s = gm.M_BuySkin[i];
            switch (s)
            {
                case 0:
                    im.OwnSkin1 = 1;
                    break;
                case 1:
                    im.OwnSkin2 = 1;
                    break;
                case 2:
                    im.OwnSkin3 = 1;
                    break;
            }
        }
    }
}
