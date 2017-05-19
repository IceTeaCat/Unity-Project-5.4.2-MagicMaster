using UnityEngine;
using System.Collections;
using CnControls;

public class PlayerSkill : Photon.MonoBehaviour
{
    PlayerAbilityValue _pav;

    public bool CanFire=true;

    public GameObject playerSkill;


    public static bool SkillStandBy = false;
    public static bool SkillFire = false;


    void Start()
    {
        _pav = GetComponent<PlayerAbilityValue>();

        if (_pav.SKILL == 2 && _pav.ADVANCED_SKILL == 3)
        {
            GameObject GR = PhotonNetwork.Instantiate("GlacierRange", transform.position, Quaternion.identity, 0);
            GR.transform.parent = gameObject.transform;
            print("123");
        }

    }


    void Update()
    {
        if (photonView.isMine && CanFire)
        {
            SkillJoystick();
            Skill_Function(_pav.SKILL, _pav.ADVANCED_SKILL);
        }





    }


    void SkillJoystick()
    {
        Vector3 R_inputVector = new Vector3(CnInputManager.GetAxis("Horizontal2"), CnInputManager.GetAxis("Vertical2"));
        Vector3 R_movementVector = Vector3.zero;

        if (R_inputVector.sqrMagnitude > 0.001f)
        {
            R_movementVector = Camera.main.transform.TransformDirection(R_inputVector);
            R_movementVector.y = 0f;
            R_movementVector.Normalize();
            playerSkill.transform.forward = R_movementVector;
        }
    }


    void Skill_Function(int s, int a)
    {
        switch (s)
        {
            case 1:
                {
                    //玩家正在按右按鈕
                    if (SkillStandBy)
                    {
                        playerSkill.SetActive(true);
                    }

                    //玩家放開右按鈕
                    //施放技能 
                    if (SkillFire)
                    {
                        GameObject fireball = PhotonNetwork.Instantiate("FireBall_small", playerSkill.transform.position, Quaternion.identity, 0) as GameObject;
                        fireball.GetComponent<FireBall>().Team = GetComponent<PlayerAbilityValue>().TEAM;
                        fireball.transform.forward = -playerSkill.transform.forward;
                        SkillFire = false;
                        SkillStandBy = false;
                        playerSkill.SetActive(false);


                        //附加進階技能
                        switch (a)
                        {
                            case 0:
                                {

                                }
                                break;

                            case 1:
                                {
                                    //燃燒
                                    fireball.AddComponent<Combustion>();


                                }
                                break;

                            case 2:
                                {
                                    //融合
                                    fireball.AddComponent<Fusion>();


                                }
                                break;

                            case 3:
                                {
                                    //濺散
                                    fireball.AddComponent<Spatter>();


                                }
                                break;
                        }
                    }


                }
                break;

            case 2:
                {
                    //玩家正在按右按鈕
                    if (SkillStandBy)
                    {
                        playerSkill.SetActive(true);
                    }

                    //玩家放開右按鈕
                    //施放技能 
                    if (SkillFire)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            GameObject ice = PhotonNetwork.Instantiate("Ice", playerSkill.transform.position, Quaternion.identity, 0) as GameObject;
                            ice.GetComponent<Ice>().Team = GetComponent<PlayerAbilityValue>().TEAM;
                            ice.transform.forward = -playerSkill.transform.forward;
                            ice.transform.Rotate(0, (i-1)*15, 0);

                            //附加進階技能
                            switch (a)
                            {
                                case 0:
                                    {

                                    }
                                    break;

                                case 1:
                                    {
                                        //結凍
                                        ice.AddComponent<Frozen>();
                                    }
                                    break;

                                case 2:
                                    {
                                        //寒風
                                        ice.AddComponent<Chill>();
                                    }
                                    break;

                                case 3:
                                    {
                                        //刺骨
                                        ice.AddComponent<Glacier>();
                                    }
                                    break;
                            }

                        }

                        SkillFire = false;
                        SkillStandBy = false;
                        playerSkill.SetActive(false);


    
                    }


                }
                break;



        }


    }




}
