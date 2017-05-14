using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviour {

    public string AllPlayerCount;
    public string AllRoomCount;

    
    void Start () {

    }


    void Update()
    {


        AllPlayerCount = PhotonNetwork.countOfPlayers.ToString();
        AllRoomCount = PhotonNetwork.countOfRooms.ToString();

        print("線上玩家總人數:" + AllPlayerCount);
        print("線上房間總數:" + AllRoomCount);

        if (PhotonNetwork.connectionStateDetailed.ToString() != "Joined")
        {
            GetComponent<Text>().text = PhotonNetwork.connectionStateDetailed.ToString();
            print( PhotonNetwork.connectionStateDetailed.ToString());
        }
        else
        {
            GetComponent<Text>().text = "Connected to " + PhotonNetwork.room.name + "  Player(s)Coiunt:" + PhotonNetwork.room.playerCount;
            print("Connected to " + PhotonNetwork.room.name + "  Player(s)Coiunt:" + PhotonNetwork.room.playerCount);
        }
  
    }
}
