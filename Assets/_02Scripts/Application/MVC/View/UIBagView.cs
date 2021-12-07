using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBagView : View
{
    public Text coin;
    public Text maxMileage;
    public InputField username;
    public GameModel m_gm;

    private void Awake()
    {
        m_gm = Game.M_Instance.M_GM;
    }

    public void Start()
    {
     
        BagArgs args = new BagArgs();
        MVC.SendEvent(Consts.E_BagInit,args);
    }

    public override void RegisterAttentionEvent()
    {
        M_AttentionList.Add(Consts.E_BagSkin);
        M_AttentionList.Add(Consts.E_BagTool);
    }


    public override string M_Name
    {
        get
        {
            return Consts.V_UIBagBoard;
        }
    }
    public override void HandleEvent(string eventName, object data = null)
    {

    }
   
    public void onClickSkin()
    {
        BagArgs args = new BagArgs();
        //SendEvent(Consts.E_BagSkin);
        //UIBagSkin newskin = new UIBagSkin();
        //newskin.Show();
        Debug.Log("click11");
        UIBagSkin UIbagskin = MVC.GetView<UIBagSkin>();
        UIbagskin.Show();
        //MVC.SendEvent(Consts.E_BagSkin,args);
    }
    public void onClickTool()
    {
        Debug.Log("tool");
        MVC.SendEvent(Consts.E_BagTool);
    }

    public void Home()
    {
        initModel initmodel = MVC.GetModel<initModel>();
        Debug.Log("home username"+initmodel.Username);
        MVC.SendEvent(Consts.E_SaveAll);
        Game.M_Instance.LoadLevel(1);
    }

    public void UpdateInfo(BagArgs data)
    {
        //coin = GameObject.Find("UIBag/Canvas/Panel/information/coininfo/coinnum").GetComponent<Text>();
        //maxMileage = GameObject.Find("UIBag/Canvas/Panel/information/maxMile/miles").GetComponent<Text>();
        //username = GameObject.Find("UIBag/Canvas/Panel/information/nameinfo/yourname").GetComponent<Text>();
        coin.text = data.coin.ToString();
        maxMileage.text = data.maxMileage.ToString();
        username.text = data.username.ToString();
        //username.text = "Î´ÃüÃû".ToString();

    }
    public void TextChange(string str)
    {
        initModel initmodel = MVC.GetModel<initModel>();
        initmodel.Username = username.text;
        Debug.Log("newname" + initmodel.Username);
    }

}
