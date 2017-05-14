using UnityEngine;
using System.Collections;

public class GameManagerVik : Photon.MonoBehaviour {



    public string playerPrefabName = "Player";

    void OnJoinedRoom()
    {
        //玩家加入房間後
        print("玩家已加入房間:"+PhotonNetwork.room.name);

        //開始遊戲
        StartGame();
    }
    
    IEnumerator OnLeftRoom()
    {
        //Easy way to reset the level: Otherwise we'd manually reset the camera

        //Wait untill Photon is properly disconnected (empty room, and connected back to main server)
        while(PhotonNetwork.room!=null || PhotonNetwork.connected==false)
            yield return 0;

        Application.LoadLevel(Application.loadedLevel);

    }

    void StartGame()
    {
        //Camera.main.farClipPlane = 1000; //Main menu set this to 0.4 for a nicer BG    

        //prepare instantiation data for the viking: Randomly diable the axe and/or shield
        bool[] enabledRenderers = new bool[2];
        enabledRenderers[0] = Random.Range(0,2)==0;//Axe
        enabledRenderers[1] = Random.Range(0, 2) == 0; ;//Shield
        
        object[] objs = new object[1]; // Put our bool data in an object array, to send
        objs[0] = enabledRenderers;

        // Spawn our local player
        PhotonNetwork.Instantiate(this.playerPrefabName, transform.position, Quaternion.identity, 0, objs);
    }

    void OnGUI()
    {
        if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room

        if (GUILayout.Button("Exit"))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    void OnDisconnectedFromPhoton()
    {
        UnityEngine.Debug.LogWarning("OnDisconnectedFromPhoton");
    }    










}
