using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBagSkin : View
{
    public Image skin1;
    public Image skin2;
    public Image skin3;
    public Image onskin1;
    public Image onskin2;
    public Image onskin3;
    GameModel gm = MVC.GetModel<GameModel>();

    public void Start()
    {
        BagArgs args = new BagArgs();
        //skin1.gameObject.SetActive(true);
        MVC.SendEvent(Consts.E_BagSkin, args) ;
    }

    public override string M_Name
    {
        get
        {
            return Consts.V_UIBagSkin;
        }
    }
    public override void RegisterAttentionEvent()
    {
    }
    public override void HandleEvent(string eventName, object data = null)
    {
        //switch (eventName)
        //{
            //case Consts.E_BagSkin:
                //Debug.Log("click222");
                //Show();
                //break;
       // }
           
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void SkinHide()
    {
        gameObject.SetActive(false);
    }
    public void OnClickBack()
    {
        Debug.Log("click");
        SkinHide();
    }
    public void onClickButton()
    {
        BagArgs args = new BagArgs();
        string businestr = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(businestr);
        switch (businestr)
        {
            case "skin1Button":
                args.onSkin = 1;
                gm.M_TakeOnSkin=0;
                break;
            case "skin2Button":
                args.onSkin = 2;
                gm.M_TakeOnSkin=1;
                break;
            case "skin3Button":
                args.onSkin = 3;
                gm.M_TakeOnSkin=2;
                break;

        }
        MVC.SendEvent(Consts.E_BagOnSkin, args);
        MVC.SendEvent(Consts.E_BagSkin, args);


    }
    public void UpdateInfo(BagArgs data)
    {
        Debug.Log("skin1" + data.ownSkin1);
        Debug.Log("skin2" + data.ownSkin2);
        Debug.Log("skin3" + data.ownSkin3);
        Debug.Log(data.ownSkin2 == 0);
        Debug.Log(data.ownSkin2 == 1);
        if (data.ownSkin1 == 0)
        {
            skin1.gameObject.SetActive(false);
        }
        if (data.ownSkin2 == 0)
        {
            Debug.Log("if21");
            skin2.gameObject.SetActive(false);
            Debug.Log("if22");
        }
        if (data.ownSkin3 == 0)
        {
            skin3.gameObject.SetActive(false);
            Debug.Log("if3");
        }
        //}
        switch (data.onSkin)
        {
            case 1:
                Debug.Log("he");
                skin1.gameObject.SetActive(false);
                onskin1.gameObject.SetActive(true);
                onskin2.gameObject.SetActive(false);
                onskin3.gameObject.SetActive(false);
                if (data.ownSkin2 == 1)
                {
                    skin2.gameObject.SetActive(true);
                }
                if (data.ownSkin3 == 1)
                {
                    skin3.gameObject.SetActive(true);
                }
                break;
            case 2:
                onskin1.gameObject.SetActive(false);
                onskin2.gameObject.SetActive(true);
                onskin3.gameObject.SetActive(false);
                skin2.gameObject.SetActive(false);
                if (data.ownSkin1 == 1)
                {
                    skin1.gameObject.SetActive(true);
                }
                if (data.ownSkin3 == 1)
                {
                    skin3.gameObject.SetActive(true);
                }
                break;
            case 3:
                onskin1.gameObject.SetActive(false);
                onskin2.gameObject.SetActive(false);
                onskin3.gameObject.SetActive(true);
                skin3.gameObject.SetActive(false);
                if (data.ownSkin1 == 1)
                {
                    skin1.gameObject.SetActive(true);
                }
                if (data.ownSkin2 == 1)
                {
                    skin2.gameObject.SetActive(true);
                }
                break;
        }
            

    }

}

