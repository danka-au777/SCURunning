using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitItemController : Controller //用于判断人物撞击何道具并回调执行对应函数实现道具作用
{
    public override void Execute(object data)
    {
        ItemArgs e = data as ItemArgs;
        PlayerMove player = MVC.GetView<PlayerMove>();
        UIBoard uiBoard = MVC.GetView<UIBoard>();
        switch (e.M_Kind)
        {
            case ItemKind.ItemMagnet://磁铁
                player.HitMagnet();//作用
                Game.M_Instance.M_GM.M_Magnet -= e.M_HitCount;//更新model中道具数
                uiBoard.HitMagnet();//场景做相应展示
                break;
            case ItemKind.ItemMultiply://双倍金币
                player.HitMultiply();
                Game.M_Instance.M_GM.M_Multiply -= e.M_HitCount;
                uiBoard.HitMultiply();
                break;
            case ItemKind.ItemInvincible://道具
                player.HitInvincible();
                Game.M_Instance.M_GM.M_Invincible -= e.M_HitCount;
                uiBoard.HitInvincible();
                break;
        }
        uiBoard.UpdateUI();//更新UI面板
    }
}
