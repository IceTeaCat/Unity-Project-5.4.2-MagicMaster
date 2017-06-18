using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {

    MainMenu _mainmenu;

    public bool GAMESTART;
    public bool GAMEOVER;

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

    public int GetMoney;

    //UI
    public Text _gametime;
    public Text _blueteamkill;
    public Text _redteamkill;

    //GameOverUI
    public Text T_Overtime;
    public Text T_Money;
    public GameObject I_VoctoryTitle;
    public GameObject I_LoseTitle;

    //自己是哪一隊
    public static int myTeam;

    void Start () {
        _mainmenu = GameObject.Find("Code").GetComponent<MainMenu>();

    }
	
	
	void Update () {
        if(GAMESTART)
        {
            GAMETIME += Time.deltaTime;


            SetUI();

            if (BLUEPLAYERCOUNT == 0 || REDPLAYERCOUNT == 0)
                GameOver();

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



    void GameOver()
    {
        GAMEOVER = true;
        GAMESTART = false;

        if (BLUEPLAYERCOUNT <= 0)
        {
            print("紅方獲勝");
            VICTORYTEAM = 0;
        }
        if (REDPLAYERCOUNT <= 0)
        {
            print("藍方獲勝");
            VICTORYTEAM = 1;
    
        }

        CheckMe();





        _mainmenu.GamePanel.SetActive(false);
        _mainmenu.JoystickUI.SetActive(false);
        _mainmenu.GameOverPanel.SetActive(true);

        T_Overtime.text = _gametime.text;
        T_Money.text = GetMoney.ToString();
    }

    void CheckMe()
    {
        if (myTeam == VICTORYTEAM)
        {
            I_VoctoryTitle.SetActive(true);
            GetMoney = 500;
        }
        else
        {
            I_LoseTitle.SetActive(true);
            GetMoney = 100;
        }
    }

    public void AddMoney()
    {
        ShopManager.money += GetMoney;
    }


}
