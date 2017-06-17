using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBox : Photon.MonoBehaviour
{
    private GameObject code;


    public string PlayerName;
    public int tempTeam = 0;
    public int tempSkillNumber = 0;
    public int tempSkillAdvNumber = 0;
    public bool tempReady = false;
    public int tempReadtPlayerCount = 0;


    //TeamPic
    public Sprite RedTeamPic;
    public Sprite BlueTeamPic;

    //SkillPic
    public Sprite DefaultPic;
    public Sprite FireBallPic;
    public Sprite FrozenPic;
    public Sprite LightningPic;

    void Awake()
    {
        code = GameObject.Find("Code");
    }

    void Start()
    {
        if (photonView.isMine)
        {
            GetComponent<PhotonView>().RPC("InitPlayerData", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player.NickName);
            GetComponent<PhotonView>().RPC("ChangeTeamRPC", PhotonTargets.AllBufferedViaServer, tempTeam);
        }
    }



    void Update()
    {
        if (photonView.isMine)
        {
            if (!InTheRoomManager.Ready)
            {
                if (tempTeam != InTheRoomManager.Team)
                {
                    tempTeam = InTheRoomManager.Team;
                    GetComponent<PhotonView>().RPC("ChangeTeamRPC", PhotonTargets.AllBufferedViaServer, tempTeam);
                }
                if (tempSkillNumber != InTheRoomManager.SkillNumber)
                {
                    tempSkillNumber = InTheRoomManager.SkillNumber;
                    GetComponent<PhotonView>().RPC("ChangeSkillRPC", PhotonTargets.AllBufferedViaServer, tempSkillNumber);
                }

                if (tempSkillAdvNumber != InTheRoomManager.Skill_AdvanceNumber)
                {
                    tempSkillAdvNumber = InTheRoomManager.Skill_AdvanceNumber;
                    //GetComponent<PhotonView>().RPC("ChangeSkillAdvRPC", PhotonTargets.AllBufferedViaServer, tempSkillAdvNumber);
                }
            }
            if (tempReady != InTheRoomManager.Ready)
            {
                tempReady = InTheRoomManager.Ready;
                GetComponent<PhotonView>().RPC("ReadyToStartGameRPC", PhotonTargets.AllBufferedViaServer, tempReady);
            }         
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    //離開房間
    public void ExitRoom()
    {
        if (photonView.isMine && !tempReady)
        {
            if (PhotonNetwork.room == null) return;
            PhotonNetwork.LeaveRoom();
        }
    }

    //初始化玩家資料
    [PunRPC]
    void InitPlayerData(string name)
    {
        transform.SetParent(GameObject.Find("Red_PlayerSlot").transform);
        transform.localScale = new Vector3(1, 1, 1);
        PlayerName = name;
        transform.FindChild("NameText").GetComponent<Text>().text = PlayerName;
    }


    //更換隊伍
    [PunRPC]
    void ChangeTeamRPC(int team)
    {
        if (team == 0)
        {
            transform.SetParent(GameObject.Find("Red_PlayerSlot").transform);
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.GetComponent<Image>().sprite = RedTeamPic;
        }
        else if (team == 1)
        {
            transform.SetParent(GameObject.Find("Blue_PlayerSlot").transform);
            gameObject.GetComponent<Image>().sprite = BlueTeamPic;
        }
    }



    //更換技能
    [PunRPC]
    void ChangeSkillRPC(int skillnumber)
    {
        transform.FindChild("Skill_Icon").GetComponent<Image>().sprite = code.GetComponent<SkillList>().All_Skill_Sprite[skillnumber];
    }

    [PunRPC]
    void ChangeSkillAdvRPC(int skilladvnumber)
    {
        transform.FindChild("Skill_Icon").GetComponent<Image>().sprite = code.GetComponent<SkillList>().All_Skill_Sprite[0];
    }

    //是否準備完成
    [PunRPC]
    void ReadyToStartGameRPC(bool ready)
    {
        if (!ready)
        {
            GetComponent<Image>().color = Color.white;
            InTheRoomManager.ReadyPlayerCount -= 1;
            //print("-1:"+ tempReadtPlayerCount);
        }
        else
        {
            GetComponent<Image>().color = Color.black;
            InTheRoomManager.ReadyPlayerCount += 1;
            //print("+1:"+tempReadtPlayerCount);
        }


    }


}
