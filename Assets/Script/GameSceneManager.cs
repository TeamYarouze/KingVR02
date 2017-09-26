using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;
#if UNITY_PS4
using UnityEngine.PS4;
#endif  //

public class GameSceneManager : Singleton<GameSceneManager> {

    private Scene currentScene;
    public Scene CurrentScene
    {
        get { return currentScene; }
    }

    // フェード中かどうかをチェックする
    public bool IsFade
    {
        get { return GameFadeManager.Instance.IsFade(); }
    }

    private bool m_bLoad;
    public bool NowLoading
    {
        get { return m_bLoad; }
    }

    private GameObject sceneUpdater;

    new public void Awake()
    {
        Debug.Log("--------------- GameSceneManager Instance - ");

        base.Awake();
        DontDestroyOnLoad(this.gameObject);

        // FPS固定
        Application.targetFrameRate = GameDefine.BaseFPS;

        // ブートシーンの取得
        currentScene = SceneManager.GetActiveScene();
    }

	void Start () {
        Debug.Log("------------- GameSceneManager Start \n");
        if( VRSettings.enabled )
        {
            VRManager.Instance.BeginVRSetup();
        }

        sceneUpdater = GameObject.Find("SceneUpdater");

        m_bLoad = false;
	}
	
	void Update ()
    {
        currentScene = SceneManager.GetActiveScene();
	}

    //---------------------------------------------------------------
    /*
        @brief      シーンを遷移する
        @param[in]  GameModeData.GAMEMODE 遷移先シーン
    */
    //---------------------------------------------------------------
    public void ChangeScene(GameModeData.GAMEMODE next)
    {
        Debug.Log("Change Scene Start [ To: " + GameModeData.GameMode + " From: " + next + " ]");

        m_bLoad = true;
        StartCoroutine(LoadSceneAsync(next));
    }

    IEnumerator LoadSceneAsync(GameModeData.GAMEMODE next)
    {

        // フェードアウトしてから
        GameFadeManager.Instance.StartFade(GameFadeManager.FadeType.FADE_OUT, 30);
        while( GameFadeManager.Instance.IsFade() )
        {
            yield return null;
        }

        // シーンの非同期読み込み
        string sceneName = GameResourcePath.GetSceneName(next);
        AsyncOperation asyncOP = SceneManager.LoadSceneAsync(sceneName);
        while( !asyncOP.isDone )
        {
            yield return null;
        }

        GameModeData.ChangeGameMode(next);
        
        // フェードイン
        GameFadeManager.Instance.StartFade(GameFadeManager.FadeType.FADE_IN, 30);
        while( GameFadeManager.Instance.IsFade() )
        {
            yield return null;
        }

        m_bLoad = false;

    }

    //=========================================================================

    public UpdateBase GetSceneUpdater()
    {
        if (!sceneUpdater )
        {
            return null;
        }

        if( currentScene.name == "bootSequence" )
        {
            return sceneUpdater.GetComponent<UpdateBootSequence>();
        }
        else if( currentScene.name == "Title" )
        {
            return sceneUpdater.GetComponent<UpdateTitle>();
        }
        else
        {
            return sceneUpdater.GetComponent<UpdateStage>();
        }

        return null;
    }

    //---------------------------------------------------------------
    /*
        @brief      フェードアウト開始
    */
    //---------------------------------------------------------------
    public void StartFadeOut(int fadeTime)
    {
        GameFadeManager.Instance.StartFade(GameFadeManager.FadeType.FADE_OUT, fadeTime);
    }

    //---------------------------------------------------------------
    /*
        @brief      フェードイン開始
    */
    //---------------------------------------------------------------
    public void StartFadeIn(int fadeTime)
    {
        GameFadeManager.Instance.StartFade(GameFadeManager.FadeType.FADE_IN, fadeTime);
    }

    

}
 
