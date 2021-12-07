using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//scene 6 setting
public class UISetting : View
{
    //用于离开设置时通知保存用户设置 通过static保存ui组件处理函数对数据的修改
    public static float volumn;
    public static int mode;

    public Slider volumnSlider;
    public ToggleGroup modeList;
    public GameModel m_gm;

    public override string M_Name
    {
        get
        {
            return Consts.V_UISetting;
        }
    }

    public override void RegisterAttentionEvent()
    {
    }

    private void Awake()
    {
        m_gm = Game.M_Instance.M_GM;
    }
    private void Start() //通过prefer获得历史信息并显示
    {
        //读取历史设置
        volumn=PlayerPrefs.GetFloat("music");
        mode=PlayerPrefs.GetInt("difficulty");
        Debug.Log(mode+"readmode");
        //volumn = 0.3f;
        //mode = 1;
        //初始化显示
        volumnSlider.value = volumn;
        Toggle[] modelist = modeList.GetComponentsInChildren<Toggle>();
        modelist[mode].isOn=true;
        
    }

    public override void HandleEvent(string eventName, object data = null)
    {
        throw new System.NotImplementedException();
    }
    

    public void OnVolumnValueChange(float value)//volumn slider 
    {
        volumn = value;
        AudioListener.volume = value;
        //Debug.Log("vol" + value+" "+volumn);
    }

    public void OnToggleMode()
    {
        IEnumerable<Toggle> toggleGroup = modeList.ActiveToggles();//获得被选择的toggle
        foreach (Toggle t in toggleGroup)
        {
            if (t.isOn)
            {
                switch (t.name)
                {
                    case "easy":
                        mode = 0;
                        break;
                    case "normal":
                        mode = 1;
                        break;
                    case "hard":
                        mode = 2;
                        break;
                }
                Debug.Log("mode " + mode );
            }

            break;
        }
    }

    public void OnClickBacktoMenu()//返回主菜单
    {
        SettingArgs args = new SettingArgs() { music = volumn, difficulty = mode };
        MVC.SendEvent(Consts.E_SettingChanged, args); //通知修改设置
        Debug.Log(args.music+"vol  and  mode"+args.difficulty);//test for taking down changes
        Game.M_Instance.LoadLevel(1);
    }

    

}
