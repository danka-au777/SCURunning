using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : Controller
{
    public override void Execute(object data)
    {
        initModel initmodel = MVC.GetModel<initModel>();
        BagArgs args = data as BagArgs;
        initmodel.Onskin = args.onSkin;
        Debug.Log("onskin" + initmodel.Onskin);
    }
}
