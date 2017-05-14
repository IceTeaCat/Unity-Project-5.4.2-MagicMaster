using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Player
{
    public string PlayerName;
    public Sprite SkillIcon;
    public int Team;
    public Sprite SkillEffectIcon;
    public bool isReady;

}

public class GameRoomManager : MonoBehaviour {

    public GameObject PlayerBox;
    public Transform PlayerSlot;

    public  List<Player> PlayerList;

	
	void Start () {

    }
	
	
	void Update () {



    }
    /*
    void PopulateList()
    {
        foreach (var player in PlayerList)
        {
            GameObject newPlayer = Instantiate(PlayerBox) as GameObject;
            PlayerBox playerbox = newPlayer.GetComponent<PlayerBox>();

            playerbox.PlayerNameLabel.text = player.PlayerName;
            playerbox.SkillIcon.sprite = player.SkillIcon;
            //playerbox.SkillEffectIcon.sprite = player.SkillEffectIcon;
            playerbox.isReady = player.isReady;

            playerbox.transform.SetParent(PlayerSlot);
            playerbox.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    */

    void ListUpdate()
    {
        foreach (var player in PhotonNetwork.playerList)
        {
            PlayerList.Add(new Player { PlayerName = player.NickName });
        }


    }



}
