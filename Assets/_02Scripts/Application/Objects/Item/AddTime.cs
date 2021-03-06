using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Tag_Player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitAddTime", SendMessageOptions.RequireReceiver);//发送消息给人物
        }
    }
    public override void HitPlayer(Transform pos)//碰撞后播放音效 消失
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Time);
        Game.M_Instance.M_ObjectPool.UnSpawn(gameObject);
    }
}
