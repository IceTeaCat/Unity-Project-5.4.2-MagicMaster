using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class RoomList : MonoBehaviour
{
    public Transform panel;
    public Transform slot;
    private List<GameObject> roomList;
    public GameObject scroll;


    void Start()
    {

    }


    public void OnEnable()
    {

            if (roomList == null)
            {
                //panel = transform.FindChild("Area/Panel");
                //scroll = transform.FindChild("Scrollbar").gameObject;
                roomList = new List<GameObject>();
            }
            InvokeRepeating("PopulateServerList", 0, 1);


    }

    public void OnDisable()
    {
        CancelInvoke();
    }


    public void PopulateServerList()
    {
        int i = 0;
        RoomInfo[] hostroomData = PhotonNetwork.GetRoomList();

        for (int j = 0; j < roomList.Count; j++)
        {
            Destroy(roomList[j]);
        }
        roomList.Clear();

        if (null != hostroomData)
        {
            for (i = 0; i < hostroomData.Length; i++)
            {
                if (!hostroomData[i].open)
                    continue;
                GameObject roombox = (GameObject)Instantiate(Resources.Load("MyRoomBox"));
                roomList.Add(roombox);
                roombox.transform.SetParent(slot, false);
                roombox.transform.FindChild("RoomNameBG/RoomNameText").GetComponent<Text>().text = hostroomData[i].name;
                roombox.transform.FindChild("RoomPlayerCount/RoomPlayerCountText").GetComponent<Text>().text = hostroomData[i].playerCount + "/" + hostroomData[i].maxPlayers;
                roombox.transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }





}
