using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingChangedController : Controller
{
    public override void Execute(object data)
    {
        SettingArgs args = data as SettingArgs;

        initModel initmodel = MVC.GetModel<initModel>();

        initmodel.Music = args.music;
        initmodel.Difficulty = args.difficulty;
        Debug.Log("settingchange"+ args.difficulty);
        initmodel.SaveAll();
    }
}
