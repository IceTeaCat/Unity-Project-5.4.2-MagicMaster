using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour {

    public Transform panel;
    public Transform Redslot;
    public Transform Blueslot;
    private List<GameObject> playerList;
    public GameObject scroll;

    public GameObject LocalPlayerBox;

    void Start()
    {

    }




    public void OnEnable()
    {
        if (playerList == null)
        {
            //panel = transform.FindChild("Area/Panel");
            //scroll = transform.FindChild("Scrollbar").gameObject;
            playerList = new List<GameObject>();
        }
        InvokeRepeating("PopulateServerList", 0, 1);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    public void Update()
    {

    }





    public void PopulateServerList()
    {
        int i = 0;

        //RoomInfo[] hostroomData = PhotonNetwork.GetRoomList();
        //RoomInfo[] hostroomData = PhotonNetwork.GetRoomList();

        PhotonPlayer[] otherList = PhotonNetwork.playerList;

        for (int j = 0; j < playerList.Count; j++)
        {
            Destroy(playerList[j]);
        }
        playerList.Clear();
        /*
        if (null != otherList)
        {
            for (i = 0; i < otherList.Length; i++)
            {
                GameObject playerbox = (GameObject)Instantiate(Resources.Load("MyPlayerBox"));
                playerList.Add(playerbox);



                
                if (playerbox.GetComponent<PlayerBox>().Team == 0)
                    playerbox.transform.SetParent(Redslot, false);
                else if (playerbox.GetComponent<PlayerBox>().Team == 1)
                    playerbox.transform.SetParent(Blueslot, false);
                

                playerbox.transform.FindChild("NameText").GetComponent<Text>().text = otherList[i].name;
                playerbox.transform.localScale = new Vector3(1, 1, 1);
 
                if (playerbox.transform.FindChild("NameText").GetComponent<Text>().text==PhotonNetwork.player.NickName)
                {
                    LocalPlayerBox = playerbox;
                }

            }
            
        }
        */
    }
}
