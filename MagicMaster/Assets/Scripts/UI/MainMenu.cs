using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : Photon.MonoBehaviour
{

    //UI
    public GameObject CreatePlayerPanel;
    public GameObject GameLobbyPanel;
    public GameObject CreateRoomPanel;
    public GameObject JoinRoomPanel;
    public GameObject InTheRoomPanel;
    public GameObject StorePanel;
    public GameObject StoreSkillList;
    public GameObject StoreGemList;

    public Text CreatePlayerName;


    public GameObject JoystickUI;
    public Text LobbyPlayerName;


    public string playerPrefabName = "Player";


    //動作
    Animation anim;

    void Awake()
    {
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings("v1.0");



    }


    private string roomName = "房間名稱";
    private Vector2 scrollPos = Vector2.zero;

     void Start()
    {

    }




    void Update()
    {


        if (InTheRoomManager.Boss)
            InTheRoomPanel.transform.FindChild("Room_panel/StartGame_btn").gameObject.SetActive(true);
        else
            InTheRoomPanel.transform.FindChild("Room_panel/StartGame_btn").gameObject.SetActive(false);
    }

    void OnJoinedLobby()
    {
        //PhotonNetwork.player.NickName = PlayerPrefs.GetString("PlayerName");
        print(PhotonNetwork.player.NickName);
        if (PhotonNetwork.player.NickName == "")
        {
            CreatePlayerPanel.SetActive(true);
            CreatePlayerPanel.GetComponent<Animator>().SetTrigger("ChangeIn");
        }
        else
        {
            GameLobbyPanel.GetComponent<Animator>().SetTrigger("ChangeIn");
            GameLobbyPanel.SetActive(true);
            LobbyPlayerName.text = PhotonNetwork.playerName;

        }
    }


    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedRoom()
    {
        InTheRoomPanel.SetActive(true);
        //InTheRoomPanel.GetComponent<Animator>().SetTrigger("ChangeIn");
        JoinRoomPanel.SetActive(false);


        

        //玩家加入房間後
        print("玩家已加入房間:" + PhotonNetwork.room.name);
    }




    public void CreatePlayerToGameLobby()
    {
        CreatePlayerPanel.GetComponent<Animator>().SetTrigger("Pressed");
        if (CreatePlayerName.text != "")
        {
            //CreatePlayerPanel.GetComponent<Animator>().SetTrigger("ChangeOut");

            GameLobbyPanel.SetActive(true);
            GameLobbyPanel.GetComponent<Animator>().SetTrigger("ChangeIn");
            CreatePlayerPanel.SetActive(false);

            PhotonNetwork.playerName = CreatePlayerPanel.transform.FindChild("PanelBG/EnterPlayerName/PlayerNameTextField").GetComponent<InputField>().text;
            print("歡迎你~" + PhotonNetwork.playerName);
            LobbyPlayerName.text = PhotonNetwork.playerName;
            PlayerPrefs.SetString("PlayerName", PhotonNetwork.playerName);
            //PlayerPrefs.Save();
        }
    }

    public void GameLobbyToCreateRoom()
    {
        GameLobbyPanel.SetActive(false);
        CreateRoomPanel.SetActive(true);
        //CreateRoomPanel.GetComponent<Animator>().SetTrigger("ChangeIn");
    }

    public void CreateRoomReturnToGameLobby()
    {
        CreateRoomPanel.SetActive(false);
        GameLobbyPanel.SetActive(true);
    }


    public void CreateRoomToInTheRoom()
    {
        CreateRoomPanel.SetActive(false);


        roomName = CreateRoomPanel.transform.FindChild("PanelBG/EnterRoomName/RoomTextField").GetComponent<InputField>().text;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 10 }, TypedLobby.Default);

    }

    public void GameLobbyToJoinRoom()
    {
        GameLobbyPanel.SetActive(false);
        JoinRoomPanel.SetActive(true);
    }

    public void JoinRoomReturnToGameLobby()
    {
        JoinRoomPanel.SetActive(false);
        GameLobbyPanel.SetActive(true);
    }

    public void OpenStore()
    {
        StorePanel.SetActive(true);
    }
    public void CloseStore()
    {
        StorePanel.SetActive(false);
    }

    public void SwitchToSkill()
    {
        StoreSkillList.SetActive(true);
        StoreGemList.SetActive(false);
    }
    public void SwitchToGem()
    {
        StoreGemList.SetActive(true);
        StoreSkillList.SetActive(false);
    }


    public void SelectSkillPanel()
    {
        if (!InTheRoomManager.Ready)
            InTheRoomPanel.transform.Find("Room_panel/SkillPanel").gameObject.SetActive(true);
    }

    public void CloseSkillPanel(int skillnumber)
    {
        InTheRoomPanel.transform.Find("Room_panel/SkillPanel").gameObject.SetActive(false);
        InTheRoomPanel.transform.Find("Room_panel/Skill_Select_btn").GetComponent<Image>().sprite = gameObject.GetComponent<SkillList>().All_Skill_Sprite[skillnumber];
    }


    public void BossReadyToStartGame()
    {
        if (InTheRoomManager.ReadyPlayerCount == PhotonNetwork.room.PlayerCount)
        {
            GetComponent<PhotonView>().RPC("STARTGAME", PhotonTargets.AllBufferedViaServer);

        }
        else
            print("只有" + InTheRoomManager.ReadyPlayerCount + "個玩家準備完成");
    }



[PunRPC]
    void STARTGAME()
    {
        InTheRoomPanel.SetActive(false);
        JoystickUI.SetActive(true);
        GameObject MyCharacter = PhotonNetwork.Instantiate(this.playerPrefabName, transform.position, Quaternion.identity, 0);
        MyCharacter.GetComponent<PlayerAbilityValue>().PLAYER_NAME = PhotonNetwork.player.NickName;
        MyCharacter.GetComponent<PlayerAbilityValue>().TEAM = InTheRoomManager.Team;
        MyCharacter.GetComponent<PlayerAbilityValue>().SKILL = InTheRoomManager.SkillNumber;
        MyCharacter.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = InTheRoomManager.Skill_AdvanceNumber;
    }

}
