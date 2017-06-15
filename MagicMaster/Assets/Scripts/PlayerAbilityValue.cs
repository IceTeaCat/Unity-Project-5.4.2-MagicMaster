using UnityEngine;
using System.Collections;

public class PlayerAbilityValue : Photon.MonoBehaviour
{

    [Header("-----玩家資料-----")]
    [Tooltip("暱稱")]
    public string PLAYER_NAME = "";

    [Tooltip("隊伍")]
    public int TEAM = 0;

    [Tooltip("技能")]
    public int SKILL = 0;

    [Tooltip("進階技能")]
    public int ADVANCED_SKILL = 0;

    public float SKILL_CD=0;

    [Tooltip("移動速度")]
    public float MOVE_SPEED=5;

    [Tooltip("血量")]
    public int HEALTH =5000;

    [Tooltip("是否死亡")]
    public bool IsDestroy = false;

    [Tooltip("殺敵數")]
    public int KILL = 0;

    [Tooltip("存活時間")]
    public float LIFETIME = 0;


    void Start()
    {
        PLAYER_NAME = GetComponent<PhotonView>().owner.NickName;
    }


    void Update()
    {
        if (!IsDestroy)
            if (HEALTH <= 0)
            {
                IsDestroy = true;

                //transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                //transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                GetComponent<PlayerController>().enabled = false;
                GetComponent<PlayerSkill>().enabled = false;
                //PhotonNetwork.Destroy(gameObject);
            }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //資料在這裡同步的話可以直接從Unity修改數值方便測試
        //否則其實只要用RPC同步就好了
        if (stream.isWriting)
        {
            stream.SendNext(TEAM);
            stream.SendNext(SKILL);
            stream.SendNext(ADVANCED_SKILL);
            stream.SendNext(HEALTH);
        }

        else
        {
            TEAM = (int)stream.ReceiveNext();
            SKILL = (int)stream.ReceiveNext();
            ADVANCED_SKILL = (int)stream.ReceiveNext();
            HEALTH = (int)stream.ReceiveNext();
        }
    }

    /*
    //隊伍資訊
    [PunRPC]
    void SetTeam(int team)
    {
        TEAM = team;
    }

    //技能資訊
    [PunRPC]
    void SetSkill(int skill,int adv_skill)
    {
        SKILL = skill;
        ADVANCED_SKILL = adv_skill;
    }
    */

    //接受傷害
    [PunRPC]
    void SetDamage(int Power)
    {
        if (HEALTH > 0)
        {
            HEALTH -= Power;
            /*
            if(HEALTH<=0)
            {
                print(PhotonView.Find(fromWho_ID).GetComponent<PlayerAbilityValue>().PLAYER_NAME + "擊殺了" + PLAYER_NAME);
                PhotonView.Find(fromWho_ID).GetComponent<PlayerAbilityValue>().KILL += 1;

            }
            */
        }

        print("受到攻擊");
    }




}
