using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagToolController : Controller
{
    public override void Execute(object data)
    {
        initModel initmodel = MVC.GetModel<initModel>();
        GameModel gm = MVC.GetModel<GameModel>();
        BagToolArgs args = new BagToolArgs();
        args.doublecoinNum = gm.M_Multiply;
        args.invincibleNum = gm.M_Invincible;
        args.protectionNum = initmodel.ProtectionNum;
        args.magenetNum = gm.M_Magnet;
        UIBagTool uibagtool = MVC.GetView<UIBagTool>();
        uibagtool.UpDateTool(args);

    }
}
