using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Debug : MonoBehaviour
{


    void Start()
    {

    }


    void Update()
    {
        GetComponent<Text>().text = PhotonNetwork.connectionStateDetailed.ToString();
        //print(PhotonNetwork.connectionStateDetailed.ToString());
    }

}