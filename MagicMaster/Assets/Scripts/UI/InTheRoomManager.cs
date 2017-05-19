using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InTheRoomManager : Photon.MonoBehaviour
{
    public GameObject Test;

    public static int Team;
    public static int SkillNumber;
    public static int Skill_AdvanceNumber;
    public static bool Ready=false;
    public static bool Boss = false;

    public static int ReadyPlayerCount = 0;

    //Player_UI
    public string playerPrefabName = "MyPlayerBox";

    void OnJoinedRoom()
    {
        if (PhotonNetwork.masterClient.NickName == PhotonNetwork.player.NickName)
            Boss = true;
        InTheRoom();
    }

    IEnumerator OnLeftRoom()
    {
        while (PhotonNetwork.room != null || PhotonNetwork.connected == false)
            yield return 0;
        
        Application.LoadLevel(Application.loadedLevel);

    }

    void InTheRoom()
    {
        GameObject playerbox = PhotonNetwork.Instantiate(this.playerPrefabName, transform.position, Quaternion.identity, 0);
    }
    
    private void Update()
    {
        Test.GetComponent<Text>().text = ReadyPlayerCount.ToString();

    }
    


    void OnDisconnectedFromPhoton()
    {
        UnityEngine.Debug.LogWarning("OnDisconnectedFromPhoton");
    }






    public void ChangeTeam(int team)
    {
        if (!Ready)
            Team = team;
    }

    public void ChangeSkill(int skillnumber)
    {
        if (!Ready)
            SkillNumber = skillnumber;
    }

    public void ChangeSkilladv(int skill_advnumber)
    {
        if (!Ready)
        {
            Skill_AdvanceNumber = skill_advnumber;
        }
    }
    

    public void ReadyToStartGamel( )
    {
        if (!Ready && SkillNumber!=0)
        {
            Ready = true;
        }
        else
        {
            Ready = false;
        }
    }





}
