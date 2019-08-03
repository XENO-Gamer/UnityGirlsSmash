using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainControl : MonoBehaviour {

    private void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        MainControl.ActiveScene = (Scene)Enum.ToObject(typeof(Scene), 1);
    }


    private static Scene activeScene;
    
    // アクティブシーンの取得と設定
    public static Scene ActiveScene
    {
        get
        {
            return activeScene;
        }
        set
        {
            activeScene = value;
        }
    }
}
