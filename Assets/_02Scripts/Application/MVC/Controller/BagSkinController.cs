using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSkinController : Controller
{
    public override void Execute(object data)
    {
        initModel initmodel = MVC.GetModel<initModel>();
        GameModel gm = MVC.GetModel<GameModel>();
        Debug.Log("controller11");
        BagArgs args = data as BagArgs;
        args.coin = gm.M_Coin;
        args.maxMileage = initmodel.MaxMileage;
        args.username = initmodel.Username;
        args.invincibleNum = initmodel.InvincibleNum;
        args.doublecoinNum = initmodel.DoublecoinNum;
        args.magenetNum = initmodel.MagenetNum;
        args.protectionNum = initmodel.ProtectionNum;
        args.ownSkin1 = initmodel.OwnSkin1;
        args.ownSkin2 = initmodel.OwnSkin2;
        args.ownSkin3 = initmodel.OwnSkin3;
        args.onSkin = initmodel.Onskin;
        UIBagSkin UIbagskin = MVC.GetView<UIBagSkin>();
        //UIbagskin.Show();
        UIbagskin.UpdateInfo(args);
        
    }
}
