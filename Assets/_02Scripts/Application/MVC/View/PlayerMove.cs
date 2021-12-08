using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : View
{
    const float grivaty = 9.8f;
    const float m_moveSpeed = 13;//用于左右移动
    const float m_jumpValue = 3.5f;//向上跃起高度
    const float m_maxSpeed = 40;
    const float m_speedAddDis = 200;//一次前进移动距离
    const float m_speedAddRate = 0.5f;//增加速度 delta v

    public override string M_Name { get { return Consts.V_PlayerMove; } }

    private CharacterController m_cc;
    private CharacterController M_Cc
    {
        get
        {
            if (m_cc == null)
            {
                m_cc = GetComponent<CharacterController>();
            }
            return m_cc;
        }
    }

    private InputDirection m_inputDir = InputDirection.Null;//移动方向

    private float m_targetX = 0;//横向位置参数 -2 0 2分别代表左 中 右道路中心位置
    private float M_TargetX //目前位置
    {
        get
        {
            return m_targetX;
        }
        set
        {
            if (value < -2)
            {
                value = -2;
            }
            else if (value > 2)
            {
                value = 2;
            }
            m_targetX = value;
        }
    }

    float xPos = 0;//当前横坐标

    private bool m_isSlide = false;//向下滑动
    private float m_slideTime;

    private float m_yDistance;//当前纵坐标

    private float m_speed = 20;//初始速度
    public float M_Speed //当前速度
    {
        get
        {
            return m_speed;
        }
        set
        {
            m_speed = value;
            if (m_speed > m_maxSpeed)
            {
                m_speed = m_maxSpeed;
            }
        }
    }

    private bool m_isInvincible = false; //是否使用无敌道具
    private bool m_isHit = false;//是否碰撞
    private float m_maskSpeed;//应恢复的速度，用于碰撞后减速恢复
    private float m_addRate = 10;//恢复的速度

    private bool m_isGoal = false;//是否射门
    private int m_nowIndex = 1;//处于的当前横向路段参数0 1 2分别为左中右

    private int m_DoubleTime = 1;//获得金币倍数，当使用双倍金币道具时计数方式改为2
    private int m_skillTime //各道具使用有效时间
    {
        get
        {
            return Game.M_Instance.M_GM.M_SkillTime;
        }
    }

    private IEnumerator m_multiplyCoroutine; //开启双倍金币道具作用
    private IEnumerator m_magnetCoroutine;//开启磁铁道具作用

    public GameObject m_magnetColliderObj;//

    private IEnumerator m_invincibleCoroutine;//开启无敌道具作用

    private GameObject m_trail;//射门后球的残影
    
    private GameObject m_ball;//球游戏体
    private GameObject M_Ball
    {
        get
        {
            if (m_ball == null)
            {
                m_ball = transform.Find("Ball").gameObject;
            }
            return m_ball;
        }
    }
    private IEnumerator m_goalCoroutine;//MoveBall()

    public GameObject m_qiuWang;//球网 未射门时经过球门会触发球网动效
    public GameObject m_jiaSu;//加速
    IEnumerator m_showHideNet;//ShowHideNet
    IEnumerator m_showHideSpeedUp;//ShowHideSpeedUP

    public SkinnedMeshRenderer M_PlayerSkin;//实现换肤人物渲染模型
    public MeshRenderer M_BallSkin;//球渲染模型

    public override void RegisterAttentionEvent()//注册响应事件 射门
    {
        M_AttentionList.Add(Consts.E_ClickGoalButtonAttention);
    }
    public override void HandleEvent(string name, object data)//通过OnGoalClick响应
    {
        switch (name)
        {
            case Consts.E_ClickGoalButtonAttention:
                OnGoalClick();
                break;
        }
    }
    public void OnGoalClick()
    {
        if (m_goalCoroutine != null)
            StopCoroutine(m_goalCoroutine);
        SendMessage("MessagePlayShoot");//发送消息PlayerAnim处理
        m_trail.SetActive(true);//开始射门渲染球的轨迹残影
        M_Ball.SetActive(false);
        m_goalCoroutine = MoveBall();
        StartCoroutine(m_goalCoroutine);//开启协程
    }

    public void GetGoalKeyboard() //通过空格可以射门，发送事件给UIBroad，其判断响应是否可触发射门
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MVC.SendEvent(Consts.E_KeySpacetoGoal);
        }
    }

    IEnumerator MoveBall()//射门协程
    {
        while (true)
        {
            if (!Game.M_Instance.M_GM.M_IsPause && Game.M_Instance.M_GM.M_IsPlay)
            {
                m_trail.transform.Translate(transform.forward * 40 * Time.deltaTime);
            }
            yield return null;
        }
    }
    public void HitBallDoor()
    {
        StopCoroutine(m_goalCoroutine);
        m_trail.transform.localPosition = new Vector3(0, 1.62f, 6.28f);
        m_trail.SetActive(false);
        M_Ball.SetActive(true);

        m_isGoal = true;

        GameObject go = Game.M_Instance.M_ObjectPool.Spawn(Consts.O_FX_GOAL);
        go.transform.SetParent(m_trail.gameObject.transform.parent);
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Goal);

        MVC.SendEvent(Consts.E_ShootGoalAttention);
    }

    private void Awake()
    {
        m_trail = GameObject.Find("trail").gameObject; //实例化
        m_trail.SetActive(false);//秋的残影设置隐藏
        //渲染人物和球模型
        M_PlayerSkin.material = Game.M_Instance.M_StaticData.GetClothInfo(Game.M_Instance.M_GM.M_TakeOnSkin, Game.M_Instance.M_GM.M_TakeOnCloth).M_Material;
        M_BallSkin.material = Game.M_Instance.M_StaticData.GetFootballInfo(Game.M_Instance.M_GM.M_TakeOnFootball).M_Material;
    }
    private void Start()
    {
        //开启协程 更新各操作
        StartCoroutine(UpdateAction());
    }

    private void OnTriggerEnter(Collider other) //检测到碰撞物体进行相应处理
    {
        if (other.gameObject.tag == Tag.Tag_SmallFence)//矮障碍
        {
            if (m_isInvincible) return;//无敌模式视作未碰撞

            other.gameObject.SendMessage("HitPlayer", transform.position);//发送消息HitPlayer是场景中其他物品都有的虚函数
            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Hit);//渲染效果
            HitObstacles();//速度清零
        }
        else if (other.gameObject.tag == Tag.Tag_BigFence)
        {
            if (m_isInvincible) return;
            if (m_isSlide) return;

            other.gameObject.SendMessage("HitPlayer", transform.position);
            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Hit);
            HitObstacles();
        }
        else if (other.gameObject.tag == Tag.Tag_Block)//死亡障碍1
        {
            if (m_isInvincible) return;

            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_End);
            other.gameObject.SendMessage("HitPlayer", transform.position);

            MVC.SendEvent(Consts.E_EndGameController);
        }
        else if (other.gameObject.tag == Tag.Tag_SmallBlock)//死亡障碍2
        {
            if (m_isInvincible) return;

            Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_End);
            other.transform.parent.parent.SendMessage("HitPlayer", transform.position);

            MVC.SendEvent(Consts.E_EndGameController);
        }
        else if (other.gameObject.tag == Tag.Tag_BeforeTrigger)
        {
            other.transform.parent.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);
        }
        else if (other.gameObject.tag == Tag.Tag_BeforeGoalTrigger)//可以射门 加速
        {
            MVC.SendEvent(Consts.E_HitGoalTriggerAttention); 
            if (m_showHideSpeedUp != null)
            {
                StopCoroutine(m_showHideSpeedUp);
            }
            m_showHideSpeedUp = ShowHideSpeedUp();
            StartCoroutine(m_showHideSpeedUp);
        }
        else if (other.gameObject.tag == Tag.Tag_GoalKeeper)//普通障碍 守门员 实现效果
        {
            HitObstacles();
            other.transform.parent.parent.parent.SendMessage("HitGoalKeeper", SendMessageOptions.RequireReceiver);
        }
        else if (other.gameObject.tag == Tag.Tag_BallDoor)//球门
        {
            if (m_isGoal)
            {
                m_isGoal = false;
                return;
            }
            HitObstacles();//速度清零
            if (m_showHideNet != null)
            {
                StopCoroutine(m_showHideNet);
            }
            m_showHideNet = ShowHideNet();//通过协程展示球网
            StartCoroutine(m_showHideNet);

            other.transform.parent.parent.SendMessage("HitDoor", m_nowIndex);//发送撞击球门的消息ShootGoal.cs处理
        }
    }

    IEnumerator ShowHideNet()
    {
        m_qiuWang.SetActive(true);
        yield return new WaitForSeconds(1);//1*timescale 执行次序在后
        m_qiuWang.SetActive(false);
    }
    IEnumerator ShowHideSpeedUp()
    {
        m_jiaSu.SetActive(true);
        yield return new WaitForSeconds(1);
        m_jiaSu.SetActive(false);
    }

    public void HitObstacles()//撞击障碍物后速度为零并开启协程进行速度恢复
    {
        if (m_isHit) return;
        m_isHit = true;
        m_maskSpeed = M_Speed;
        M_Speed = 0;
        StartCoroutine(DecreaseSpeed());
    }
    IEnumerator DecreaseSpeed() //速度恢复
    {
        while (M_Speed <= m_maskSpeed)
        {
            M_Speed += Time.deltaTime * m_addRate;
            yield return 0;
        }
        m_isHit = false;
    }

    #region 移动控制
    IEnumerator UpdateAction() //使用协程按帧操作，提高流畅性
    {
        while (true)
        {
            if (!Game.M_Instance.M_GM.M_IsPause && Game.M_Instance.M_GM.M_IsPlay)
            {
                //更新距离
                UpdateDis();
                UpdateMoveForwardAndJumpState();
                //用户操作
                GetInputDirection();
                GetGoalKeyboard();
                //设置目标坐标并实现移动
                UpdateTargetState();
                MoveControl();
                //下滑速度更新
                UpdateSlide();
                UpdateSpeed();
            }
            yield return 0;
        }
    }
    
    void UpdateDis() //发起距离修改事件
    {
        DistanceArgs e = new DistanceArgs() { M_Distance = (int)transform.position.z };
        MVC.SendEvent(Consts.E_UpdateDisAttention, e);
    }

    void UpdateMoveForwardAndJumpState()
    {
        if (!M_Cc.isGrounded)//未触地 实现下落
            m_yDistance -= grivaty * Time.deltaTime;
        if (Mathf.Abs(m_yDistance) < 0.1f)//距离足够小直接落地
            m_yDistance = 0;
        M_Cc.Move((transform.forward * M_Speed + new Vector3(0, m_yDistance, 0)) * Time.deltaTime);//按帧实现坐标变换
    }

    void GetInputDirection()
    {
        m_inputDir = InputDirection.Null;
        //键盘识别 WSAD上下左右
        if (Input.GetKeyDown(KeyCode.W) )
        {
            m_inputDir = InputDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_inputDir = InputDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_inputDir = InputDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_inputDir = InputDirection.Right;
        }
    }

    void UpdateTargetState() //通过sendMsg调用PlyaerAnim实现动画
    {
        xPos = transform.position.x;
        switch (m_inputDir)
        {
            case InputDirection.Right:
                if (xPos < 2)
                {
                    M_TargetX += 2;//目标x坐标向右
                    SendMessage("AnimManager", m_inputDir);
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Left:
                if (xPos > -2)
                {
                    M_TargetX -= 2;
                    SendMessage("AnimManager", m_inputDir);
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Down:
                if (!m_isSlide)
                {
                    m_isSlide = true;//设置下滑时间
                    m_slideTime = 0.733f;
                    SendMessage("AnimManager", m_inputDir);
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Up:
                if (M_Cc.isGrounded)
                {
                    Game.M_Instance.M_Sound.PlayEffect("Se_UI_Jump");
                    m_yDistance = m_jumpValue;//更新目的纵坐标
                    SendMessage("AnimManager", m_inputDir);
                }
                break;
        }
    }

    void MoveControl()//实现人物从当前坐标到目的坐标的移动展示
    {
        if (xPos != M_TargetX)
        {
            if (xPos > M_TargetX)
                xPos -= m_moveSpeed * Time.deltaTime;//速度*时间 即m/s * (1s/帧数）*帧数
            else
                xPos += m_moveSpeed * Time.deltaTime;
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

            if (Mathf.Abs(xPos - M_TargetX) < 0.1f)//距离差值足够小时直接移动，减小资源开销
            {
                transform.position = new Vector3(M_TargetX, transform.position.y, transform.position.z);
                if (M_TargetX == -2)
                {
                    m_nowIndex = 0;
                }
                else if (M_TargetX == 0)
                {
                    m_nowIndex = 1;
                }
                else if (M_TargetX == 2)
                {
                    m_nowIndex = 2;
                }
            }
        }
    }
    
    void UpdateSlide()//实现下滑
    {
        if (m_isSlide)
        {
            m_slideTime -= Time.deltaTime;
            if (m_slideTime < 0)
            {
                m_isSlide = false;
                m_slideTime = 0;
            }
        }
    }

    float m_lastZDis = 0;
    void UpdateSpeed()//更新移动速度
    {
        if ((transform.position.z-m_lastZDis) >= m_speedAddDis)
        {
            m_lastZDis += m_speedAddDis;

            if (m_isHit)//撞击后应恢复的速度为当前速度(即maskSpeed)+delta v
                m_maskSpeed += m_speedAddRate;
            else //未撞击 速度增加
                M_Speed += m_speedAddRate;
        }
    }
    #endregion

    public void HitCoin()//处理金币发送的消息 发送金币计数事件 传入计数值
    {
        CoinArgs e = new CoinArgs
        {
            M_Coin = m_DoubleTime
        };
        MVC.SendEvent(Consts.E_UpdateCoinAttention, e);
    }
    public void HitItem(ItemKind item) //道具发送的信息
    {
        ItemArgs e = new ItemArgs
        {
            M_Kind = item,
            M_HitCount = 0//拾取场景中的道具 而非点击使用道具 不需要计数
        };
        MVC.SendEvent(Consts.E_HitItemController, e);//发起事件
    }

    public void HitMultiply()//双倍金币作用 开启协程计时
    {
        if (m_multiplyCoroutine != null)
            StopCoroutine(m_multiplyCoroutine);
        m_multiplyCoroutine = MultiplyCoroutine();
        StartCoroutine(m_multiplyCoroutine);
    }
    IEnumerator MultiplyCoroutine()//碰撞一个金币得到两个 倒计时
    {
        m_DoubleTime = 2;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return null;
        }
        m_DoubleTime = 1;
    }

    public void HitMagnet()//磁铁作用 开启协程
    {
        if (m_magnetCoroutine != null)
        {
            StopCoroutine(m_magnetCoroutine);
        }
        m_magnetCoroutine = MagnetCoroutine();
        StartCoroutine(m_magnetCoroutine);
    }
    IEnumerator MagnetCoroutine()//?磁铁如何作用
    {
        m_magnetColliderObj.SetActive(true);
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        m_magnetColliderObj.SetActive(false);
    }

    public void HitAddTime() //相应加时器的消息 发起增加时间事件 
    {
        MVC.SendEvent(Consts.E_HitAddTimeAttention);
    }

    public void HitInvincible()//无敌道具
    {
        if (m_invincibleCoroutine != null)
        {
            StopCoroutine(m_invincibleCoroutine);
        }
        m_invincibleCoroutine = InvincibleCoroutine();
        StartCoroutine(m_invincibleCoroutine);
    }
    IEnumerator InvincibleCoroutine() //通过协程实现无敌道具倒计时
    {
        m_isInvincible = true;
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;//倒计时
            }
            yield return 0; //等待当前一帧 从下一帧开始执行
        }
        m_isInvincible = false;
    }
}
