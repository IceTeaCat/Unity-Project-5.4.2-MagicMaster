using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GM : MonoBehaviour {

    public List<GameObject> All_Player;
    public List<GameObject> Blue_Player;
    public List<GameObject> Red_Player;

    MainMenu _mainmenu;

    public bool GAMESTART;
    public bool GAMEOVER;

    //遊戲時間
    public float GAMETIME;
    /*
    //擊殺數
    public int BLUEKILLCOUNT;
    public int REDKILLCOUNT;
    */
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
    
    public Text _blueteam;
    public Text _redteam;
    
    //GameOverUI
    public Text T_Overtime;
    public Text T_Money;
    public GameObject I_VoctoryTitle;
    public GameObject I_LoseTitle;

    public SoundEffectManager _AS;

    //自己是哪一隊
    public static int myTeam;

    void Start () {
        _mainmenu = GameObject.Find("Code").GetComponent<MainMenu>();


    }
	
	
	void Update () {
        if(GAMESTART)
        {
            //持續更新或有人斷線
            if (GAMETIME<=3 || PhotonNetwork.room.PlayerCount != ALLPLAYERCOUNT)
            {
                All_Player.Clear();
                Red_Player.Clear();
                Blue_Player.Clear();
                All_Player = GameObject.FindGameObjectsWithTag("PLAYER").ToList();

                for (int i = 0; i < All_Player.Count; i++)
                {
                    if (All_Player[i].GetComponent<PlayerAbilityValue>().TEAM == 0)
                        Red_Player.Add(All_Player[i]);
                    else
                        Blue_Player.Add(All_Player[i]);
                }
            }
            
            for (int i = 0; i < All_Player.Count; i++)
            {
                if (All_Player[i].GetComponent<PlayerAbilityValue>().HEALTH <= 0)
                {
                    All_Player.RemoveAt(i);
                }
            }

            for (int i = 0; i < Red_Player.Count; i++)
            {
                if (Red_Player[i].GetComponent<PlayerAbilityValue>().HEALTH <= 0)
                {
                    Red_Player.RemoveAt(i);
                }
            }
            for (int i = 0; i < Blue_Player.Count; i++)
            {
                if (Blue_Player[i].GetComponent<PlayerAbilityValue>().HEALTH <= 0)
                {
                    Blue_Player.RemoveAt(i);
                }
            }


            REDPLAYERCOUNT = Red_Player.Count;
            BLUEPLAYERCOUNT = Blue_Player.Count;



            _mainmenu.NerfGroup.SetActive(true);

            GAMETIME += Time.deltaTime;


            SetUI();

            
            if (Red_Player.Count == 0 && GAMETIME >= 10)
                GameOver();
            if(Blue_Player.Count == 0 && GAMETIME >= 10)
                GameOver();
            
        }
    }



    void SetUI()
    {
        int sec = (int)(GAMETIME % 60);
        int min = (int)(GAMETIME / 60);
        string time = min + ":" + sec;
        _gametime.text = string.Format("{0:00}:{1:00}", min, sec); ;



        _blueteam.text = BLUEPLAYERCOUNT.ToString();
        _redteam.text = REDPLAYERCOUNT.ToString();
    }



    void GameOver()
    {
        GAMEOVER = true;
        GAMESTART = false;

        for (int i = 0; i < All_Player.Count; i++)
        {
            All_Player[i].GetComponent<PlayerController>().enabled = false;
        }


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




        _mainmenu.NerfGroup.SetActive(false);
        _mainmenu.GamePanel.SetActive(false);
        //_mainmenu.JoystickUI.SetActive(false);
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
            _AS.Victory();
        }
        else
        {
            I_LoseTitle.SetActive(true);
            GetMoney = 100;
            _AS.Lose();
        }
    }

    public void AddMoney()
    {
        ShopManager.money += GetMoney;
    }


}
