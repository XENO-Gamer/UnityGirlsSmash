using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainCol : MonoBehaviour {

    GameObject Player1Obj;
    GameObject Player2Obj;
    GameObject CameraObj;
    GameObject FadeObj;
    GameObject BgmObj;
    GameObject WinObj;
    GameObject StageObj;

    [SerializeField]
    private Text _textCountdown;

    public int Stock_Player1;   //プレーヤ1のストック
    public int Stock_Player2;   //プレーヤ2のストック
    public bool GameEndFlag;    //ゲームが終了したかどうか
    public bool mainCame_Flag;
    public bool P1Came_Flag;
    public bool P2Came_Flag;
    public bool GameStartFlag;
    public bool BGMFlag;
    public bool WinFlag;
    public AudioClip CountSound;//カウントダウン音
    public AudioClip GameStartSound;//ゲーム開始音

    BGMScript BgmScript;
    BGMScript WinBGMScript;

    AudioSource audioSource;
    bool CountStartFlag;


    // Use this for initialization
    void Start () {
        Player1Obj = GameObject.Find("Player1");
        Player2Obj = GameObject.Find("Player2");
        CameraObj = GameObject.Find("camera");
        FadeObj = GameObject.Find("fadePanel");
        BgmObj = GameObject.Find("BGM");
        StageObj = GameObject.Find("StageControl");
        WinObj = GameObject.Find("WinBGM");
        BgmScript = BgmObj.GetComponent<BGMScript>();
        WinBGMScript = WinObj.GetComponent<BGMScript>();

        Stock_Player1 = Player1Obj.GetComponent<PrefabGeneration>().Stock;
        Stock_Player2 = Player2Obj.GetComponent<PrefabGeneration>().Stock;
        audioSource = GetComponent<AudioSource>();
        mainCame_Flag = true;
        P1Came_Flag = false;
        P2Came_Flag = false;
        GameEndFlag = false;
        _textCountdown.text = "";
        CountStartFlag = false;
        GameStartFlag = false;
        BGMFlag = false;
        WinFlag = false;

    }
	
	// Update is called once per frame
	void Update () {
        Stock_Player1 = Player1Obj.GetComponent<PrefabGeneration>().Stock;
        Stock_Player2 = Player2Obj.GetComponent<PrefabGeneration>().Stock;

        if (!CountStartFlag)
        {
            CountStartFlag = true;
            StartCoroutine(CountdownCoroutine());
        }

        GameStart();

    }

    void GameStart()
    {
        if (!BGMFlag)
        {
            BGMFlag = true;
            BgmScript.BGM_ON_Stage(StageObj.GetComponent<Prefab_Stage>().StageID);
        }
        //どちらかのストックが0になったら終了
        if (Stock_Player1 <= 0 || Stock_Player2 <= 0)
        {
            BgmScript.BGM_Stop();
            if (FadeObj.GetComponent<FadeScript>().CameraChange == false)
            {
                FadeObj.GetComponent<FadeScript>().FadeFlag = true;
            }
            else
            {
                if (Stock_Player1 <= 0)
                {
                    mainCame_Flag = false;
                    P2Came_Flag = true;
                    GameEndFlag = true;
                    if (!WinFlag)
                    {
                        WinFlag = true;
                        WinBGMScript.BGM_ON_Chara(Player2Obj.GetComponent<PrefabGeneration>().CharaID);

                    }
                }
                if (Stock_Player2 <= 0)
                {
                    mainCame_Flag = false;
                    P1Came_Flag = true;
                    GameEndFlag = true;
                    if (!WinFlag)
                    {
                        WinFlag = true;
                        WinBGMScript.BGM_ON_Chara(Player1Obj.GetComponent<PrefabGeneration>().CharaID);
                    }
                }
            }

        }
    }

    IEnumerator CountdownCoroutine()
    {
        _textCountdown.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        _textCountdown.text = "3";
        audioSource.PlayOneShot(CountSound);
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        audioSource.PlayOneShot(CountSound);
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        audioSource.PlayOneShot(CountSound);
        yield return new WaitForSeconds(1.0f);
        _textCountdown.text = "GO!";
        audioSource.PlayOneShot(GameStartSound);
        GameStartFlag = true;
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
    }
}
