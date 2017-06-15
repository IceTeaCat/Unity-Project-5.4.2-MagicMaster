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
        _gametime.text = GAMETIME.ToString();
        _blueteamkill.text = BLUEKILLCOUNT.ToString();
        _redteamkill.text = REDKILLCOUNT.ToString();
    }


}
