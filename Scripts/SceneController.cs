using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    // 画面遷移制御関連項目（内部ワーク）
    protected Button button;                      // ボタンコンポーネント

    // タップ可否判定
    // @param : bool
    // @return: bool
    protected bool CanTapButton { set; get; }

    // Awake
    // @param : 
    // @return: 
    protected void Awake()
    {
        CanTapButton = true;
    }

    // Use this for initialization
    // @param : 
    // @return: 
    protected virtual void Start()
    {
        button = GetComponent<Button>();
    }

    // ボタン押下時処理
    // @param : int scene シーン番号
    // @return: 
    public virtual void OnTap(int scene)
    {
        StartCoroutine(JumpScene((Scene)Enum.ToObject(typeof(Scene), scene)));
    }

    // シーン遷移処理
    // @param : Scene scene シーン
    // @return: 
    private IEnumerator JumpScene(Scene scene)
    {
        // シーン遷移
        MainControl.ActiveScene = scene;
        SceneManager.LoadScene((int)scene, LoadSceneMode.Additive);

        Debug.Log("scene:"+(int)scene);

        while (true)
        {
            if (SceneManager.GetSceneByName(Enum.GetName(typeof(Scene), scene)).isLoaded)
            {
                break;
            }
            yield return null;
        }
        
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)scene));
        

        yield break;
    }


    // ボタンタップ可否判定
    // @param : 
    // @return: bool
    protected bool CanTap()
    {
        if (CanTapButton == false)
        {
            return false;
        }
        // タップ不可にする
        CanTapButton = false;
        return true;
    }
}


