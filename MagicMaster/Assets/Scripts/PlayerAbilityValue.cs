using UnityEngine;
using System.Collections;

public class PlayerAbilityValue : Photon.MonoBehaviour
{
    public string PLAYER_NAME = "";
    public int TEAM = 0;
    public int SKILL = 0;
    public int ADVANCED_SKILL = 1;

    public float MOVE_SPEED=5;

    public float HEALTH=100;

    void Start()
    {
        PLAYER_NAME = GetComponent<PhotonView>().owner.NickName;
    }


    void Update()
    {

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(TEAM);
            stream.SendNext(SKILL);
        }

        else
        {
            TEAM = (int)stream.ReceiveNext();
            SKILL = (int)stream.ReceiveNext();
        }

    }



}
