using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBagTool : View
{
    public Text magenetNum;
    public Text protectionNum;
    public Text invincibleNum;
    public Text doublecoinNum;
    public override string M_Name
    {
        get
        {
            return Consts.V_UIBagTool;
        }
    }
    public override void RegisterAttentionEvent()
    {

        M_AttentionList.Add(Consts.E_BagTool);
    }
    public override void HandleEvent(string eventName, object data = null)
    {
        switch (eventName)
        {
            case Consts.E_BagTool:
                Debug.Log("click222");
                Show();
                break;
            default:
                break;
        }

    }
    public void Start()
    {
        BagToolArgs args = new BagToolArgs();
        MVC.SendEvent(Consts.E_BagToolNum, args);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void ToolHide()
    {
        gameObject.SetActive(false);
    }
    public void OnClickBack()
    {
        Debug.Log("click");
        ToolHide();
    }
    public void UpDateTool(BagToolArgs data) 
    {
        magenetNum.text = data.magenetNum.ToString();
        invincibleNum.text = data.invincibleNum.ToString();
        doublecoinNum.text = data.doublecoinNum.ToString();
        protectionNum.text = data.protectionNum.ToString();
    }
}
