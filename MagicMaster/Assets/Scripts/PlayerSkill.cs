using UnityEngine;
using System.Collections;
using CnControls;

public class PlayerSkill : Photon.MonoBehaviour
{
    Vector3 R_inputVector;
    Vector3 R_movementVector;

    PlayerAbilityValue _pav;

    public bool CanFire=true;

    public GameObject Skill_Img_Arrow;
    public GameObject Skill_Img_RangeCircle;
    public GameObject Skill_Img_DamageCircle;

    public GameObject ElectricLockRange;

    public bool SkillStandBy = false;
    public bool SkillFire = false;

    void Start()
    {
        _pav = GetComponent<PlayerAbilityValue>();

        if (_pav.SKILL == 2 && _pav.ADVANCED_SKILL == 3)
        {
            GameObject GR = PhotonNetwork.Instantiate("GlacierRange", transform.position, Quaternion.identity, 0);
            GR.transform.parent = gameObject.transform;
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
        R_inputVector = new Vector3(CnInputManager.GetAxis("Horizontal2"), CnInputManager.GetAxis("Vertical2"));
        R_movementVector = Vector3.zero;

        switch (_pav.SKILL)
        {
            //火球發動技能圖示控制
            case 1:
                {
                    if (R_inputVector.sqrMagnitude > 0.001f)
                    {
                        R_movementVector = Camera.main.transform.TransformDirection(R_inputVector);
                        R_movementVector.y = 0f;
                        R_movementVector.Normalize();
                        Skill_Img_Arrow.transform.forward = R_movementVector;
                    }
                }
                break;
            //冰凍發動技能圖示
            case 2:
                {

                }
                break;
            //閃電發動技能圖示控制
            case 3:
                {

                    if (R_inputVector.sqrMagnitude > 0.001f)
                    {
                        R_movementVector = Camera.main.transform.TransformDirection(R_inputVector);
                        R_movementVector.y = 0f;
                        Skill_Img_DamageCircle.transform.localPosition = new Vector3(R_movementVector.x * 9, 0, R_movementVector.z * 9 * 10 / 8);
                    }


                }
                break;
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
                        Skill_Img_Arrow.transform.gameObject.SetActive(true);
                    }

                    //玩家放開右按鈕
                    //施放技能 
                    if (SkillFire)
                    {
                        GameObject fireball = PhotonNetwork.Instantiate("FireBall_small", Skill_Img_Arrow.transform.position, Quaternion.identity, 0) as GameObject;
                        fireball.GetComponent<FireBall>().Team = GetComponent<PlayerAbilityValue>().TEAM;
                        fireball.transform.forward = -Skill_Img_Arrow.transform.forward;
                        SkillFire = false;
                        SkillStandBy = false;
                        Skill_Img_Arrow.transform.gameObject.SetActive(false);


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
                        Skill_Img_Arrow.transform.gameObject.SetActive(true);
                    }

                    //玩家放開右按鈕
                    //施放技能 
                    if (SkillFire)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            GameObject ice = PhotonNetwork.Instantiate("Ice", Skill_Img_Arrow.transform.position, Quaternion.identity, 0) as GameObject;
                            ice.GetComponent<Ice>().Team = GetComponent<PlayerAbilityValue>().TEAM;
                            ice.transform.forward = -Skill_Img_Arrow.transform.forward;
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
                        Skill_Img_Arrow.transform.gameObject.SetActive(false);
                    }


                }
                break;

            case 3:
                {
                    //玩家正在按右按鈕
                    if (SkillStandBy)
                    {
                        Skill_Img_RangeCircle.transform.gameObject.SetActive(true);
                        Skill_Img_DamageCircle.transform.gameObject.SetActive(true);
                    }

                    //玩家放開右按鈕
                    //施放技能 
                    if (SkillFire)
                    {
                        GameObject SR = Instantiate(ElectricLockRange, Skill_Img_DamageCircle.transform.position, Quaternion.identity) as GameObject;
                        SR.GetComponent<ElectricLockRange>().Player = _pav.gameObject;
                        SR.GetComponent<ElectricLockRange>().Team = _pav.TEAM;
                        //附加進階技能
                        switch (a)
                        {
                            case 0:
                                {

                                }
                                break;

                            case 1:
                                {
                                    //多重
                                    SR.AddComponent<ElectricMultiple>();
                                }
                                break;

                            case 2:
                                {
                                    //連鎖
                                    SR.AddComponent<ElectricChain>();
                                }
                                break;

                            case 3:
                                {
                                    //增幅
                                    //elc.AddComponent<Glacier>();
                                }
                                break;
                        }



                        Skill_Img_DamageCircle.transform.localPosition = new Vector3(0, -0.4f, 0);
                        Skill_Img_DamageCircle.SetActive(false);

                        SkillFire = false;
                        SkillStandBy = false;
                    }


                }
                break;




        }


    }




}
