using UnityEngine;
using System.Collections;

public class SkillTest_ChangeSkill : Photon.MonoBehaviour {

	
	void Start () {
	
	}
	
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<PlayerAbilityValue>().TEAM = 0;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            gameObject.GetComponent<PlayerAbilityValue>().TEAM = 1;
        }


        //火球
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 1;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 1;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 1;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 1;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 3;
        }



        //冰凍
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 2;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 2;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 2;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            gameObject.GetComponent<PlayerAbilityValue>().SKILL = 2;
            gameObject.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL = 3;


            GameObject GR = PhotonNetwork.Instantiate("GlacierRange", transform.position, Quaternion.identity, 0);
            photonView.RPC("SetGRParent", PhotonTargets.All, new object[] { GR.GetComponent<PhotonView>().viewID, GetComponent<PhotonView>().viewID });


        }






    }




}
