using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RoomBox : MonoBehaviour {

    public Button Button;
    public Text RoomNameLabel;
    public Text PlayerCount;

    string RoomName;

    
    void Start () {
        RoomName = gameObject.transform.FindChild("RoomNameBG/RoomNameText").GetComponent<Text>().text;
    }
	
	
	void Update () {

    }

    public void EnterRoom()
    {
        PhotonNetwork.JoinRoom(RoomName);
        print("JoinGame:" + RoomName);



    }


}
