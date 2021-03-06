using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalShowUIController : Controller
{
    public override void Execute(object data)
    {
        initModel initmodel = MVC.GetModel<initModel>();//改了
        UIBoard board = MVC.GetView<UIBoard>();
        board.Hide();

        UIFinalScore final = MVC.GetView<UIFinalScore>();
        final.Show();
        Game.M_Instance.M_GM.M_Exp += board.M_Coin + board.M_Distance * (board.M_GoalCount + 1);
        final.UpdateUI(board.M_Distance, board.M_Coin, board.M_GoalCount, Game.M_Instance.M_GM.M_Exp, Game.M_Instance.M_GM.M_Grage);

        if (initmodel.MaxMileage < board.M_Distance)
        {
            initmodel.MaxMileage = board.M_Distance;//改了
        }
        

        UIDead dead = MVC.GetView<UIDead>();
        dead.Hide();
        Game.M_Instance.M_GM.M_Coin += board.M_Coin;
    }
}
