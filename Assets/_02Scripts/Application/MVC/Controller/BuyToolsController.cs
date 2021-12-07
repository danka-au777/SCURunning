using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyToolsController : Controller
{
    public override void Execute(object data)
    {
        BuyToolsArgs e = data as BuyToolsArgs;
        UIBuyTools tool = MVC.GetView<UIBuyTools>();
        GameModel gm = Game.M_Instance.M_GM;
        initModel im = MVC.GetModel<initModel>();

        switch (e.M_ItemKind)
        {
            case ItemKind.ItemMagnet:
                if (gm.GetMoney(e.M_Coin))
                {
                    im.Coin = gm.M_Coin;
                    gm.M_Magnet++;
                }
                break;
            case ItemKind.ItemMultiply:
                if (gm.GetMoney(e.M_Coin))
                {
                    im.Coin = gm.M_Coin;
                    gm.M_Multiply++;
                }
                break;
            case ItemKind.ItemInvincible:
                if (gm.GetMoney(e.M_Coin))
                {
                    im.Coin = gm.M_Coin;
                    gm.M_Invincible++;
                }
                break;
        }
        
        im.MagenetNum = gm.M_Magnet;
        Debug.Log(im.MagenetNum);
        im.InvincibleNum = gm.M_Invincible;
        im.DoublecoinNum = gm.M_Multiply;

        tool.InitUI();
    }
}
