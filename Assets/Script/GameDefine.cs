using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
    //----------------------------------------------------------------
    // FPS
    //----------------------------------------------------------------
    private static int baseFPS;
    public static int BaseFPS
    {
        get { return baseFPS; }
    }
    public static float FPSDeltaScale()
    {
        return baseFPS * Time.deltaTime;
    }

    static GameDefine()
    {
        baseFPS = 60;
    }

    //---------------------------------------------------------------
    // アイテムの種類
    //---------------------------------------------------------------
    public enum ITEM_TYPE
    {
        ITEM_TYPE_ROCKET,           // ロケット
        ITEM_TYPE_WEIGHT,           // 重り
        ITEM_TYPE_SKI,              // スキー

        ITEM_TYPE_NUM,
        ITEM_TYPE_INVALID,
    };

    //---------------------------------------------------------------
    // アイテムの使用タイプ
    //---------------------------------------------------------------
    public const int ItemUseType_OneShot        = 0x00000001;   // 1回使い切り
    public const int ItemUseType_UseAgain       = 0x00000002;   // 何度も使える
    public const int ItemUseType_Reload         = 0x00000004;   // リロードタイムあり
    public const int ItemUseType_BtnTrigger     = 0x00000010;   // ボタン押したら発動
    public const int ItemUseType_BtnDown        = 0x00000020;   // ボタン押してる間発動

}
