using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResourcePath {

    private static string BootSequence = "Scene/bootSequence";
    private static string Title = "Scene/Title";
    private static string Loading = "Scene/Loading";

    private static string[] Stage = new string[]
    {
        "Scene/Stage001/Stage001",
        "Scene/Stage002/Stage002",
    };


    private static string Rocket = "Prefab/Rocket";
    private static string Weight = "Prefab/Weight";


    public static string GetSceneName(GameModeData.GAMEMODE mode)
    {
        switch( mode )
        {
        case GameModeData.GAMEMODE.GAME_MODE_BOOT:
            return BootSequence;
        case GameModeData.GAMEMODE.GAME_MODE_STAGE:
            return Stage[GameModeData.StageCount];
        case GameModeData.GAMEMODE.GAME_MODE_TITLE:
            return Title;
        }

        return BootSequence;
    }

    public static string GetRocketPath()
    {
        return Rocket;
    }

    public static string GetWeightPath()
    {
        return Weight;
    }

}
