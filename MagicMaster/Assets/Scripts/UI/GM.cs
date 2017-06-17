using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {

    public bool GAMESTART;

    //遊戲時間
    public float GAMETIME;

    //擊殺數
    public int BLUEKILLCOUNT;
    public int REDKILLCOUNT;

    //玩家數
    public int ALLPLAYERCOUNT;
    public int BLUEPLAYERCOUNT;
    public int REDPLAYERCOUNT;
    public int NOWPLAYERCOUNT;

    //獲勝隊伍
    public int VICTORYTEAM;


    //UI
    public Text _gametime;
    public Text _blueteamkill;
    public Text _redteamkill;


    void Start () {
	
	}
	
	
	void Update () {
        if(GAMESTART)
        {
            GAMETIME += Time.deltaTime;


            SetUI();
        }
    }



    void SetUI()
    {
        int sec = (int)(GAMETIME % 60);
        int min = (int)(GAMETIME / 60);
        string time = min + ":" + sec;
        _gametime.text = string.Format("{0:00}:{1:00}", min, sec); ;



        _blueteamkill.text = BLUEPLAYERCOUNT.ToString();
        _redteamkill.text = REDPLAYERCOUNT.ToString();
    }


}
