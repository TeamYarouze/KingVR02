using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameModeData {

    //---------------------------------------------------------------
    // ゲームモード
    //---------------------------------------------------------------
    public enum GAMEMODE
    {
        GAME_MODE_BOOT = 0,         // ブートシーケンス
        GAME_MODE_TITLE,            // タイトル
        GAME_MODE_STORY,            // ストーリーモード
        GAME_MODE_STAGE,            // ゲーム
        GAME_MODE_NUM,
    };
     
    private static GAMEMODE gameMode;
    public static GAMEMODE GameMode
    {
        set { gameMode = value; }
        get { return gameMode; }
    }

    private static GAMEMODE prevGameMode;
    public static GAMEMODE PrevGameMode
    {
        set { prevGameMode = value; }
        get { return prevGameMode; }
    }

    static GameModeData()
    {
        gameMode = GAMEMODE.GAME_MODE_BOOT;
        prevGameMode = gameMode;
        stageCount = 0;
    }

    public static void ChangeGameMode(GAMEMODE mode)
    {
        prevGameMode = gameMode;
        gameMode = mode;
    }

    //-----------------------------------------------------------------
    // ステージ数
    //-----------------------------------------------------------------
    private static int stageCount;
    public static int StageCount
    {
        set { stageCount = value; }
        get { return stageCount; }
    }

}
