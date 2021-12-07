using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArgs //道具种类 以及计数 （减去hitcount）
{
    private ItemKind m_kind;
    public ItemKind M_Kind
    {
        get
        {
            return m_kind;
        }
        set
        {
            m_kind = value;
        }
    }
    private int m_hitCount;
    public int M_HitCount
    {
        get
        {
            return m_hitCount;
        }
        set
        {
            m_hitCount = value;
        }
    }
}
