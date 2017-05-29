﻿using UnityEngine;
using System.Collections;
using CnControls;

public class PlayerSkill : Photon.MonoBehaviour
{
    Vector3 R_inputVector;
    Vector3 R_movementVector;

    PlayerAbilityValue _pav;

    public bool CanFire = true;

    public GameObject playerSkill;
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

        if (_pav.SKILL == 3 && _pav.ADVANCED_SKILL == 3)
        {
            GameObject EIR = PhotonNetwork.Instantiate("ElectricIncreaseRange", transform.position, Quaternion.identity, 0);
            EIR.transform.parent = gameObject.transform;
            EIR.GetComponent<ElectricIncreaseRange>().Team = _pav.TEAM;

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
                        playerSkill.transform.forward = R_movementVector;
                    }
                }
                break;
            //冰凍發動技能圖示
            case 2:
                {
                    if (R_inputVector.sqrMagnitude > 0.001f)
                    {
                        R_movementVector = Camera.main.transform.TransformDirection(R_inputVector);
                        R_movementVector.y = 0f;
                        R_movementVector.Normalize();
                        playerSkill.transform.forward = R_movementVector;
                    }
                }
                break;
            //閃電發動技能圖示控制
            case 3:
                {

                    if (R_inputVector.sqrMagnitude > 0.001f)
                    {
                        R_movementVector = Camera.main.transform.TransformDirection(R_inputVector);
                        R_movementVector.y = 0f;
                        playerSkill.transform.localPosition = new Vector3(R_movementVector.x * 9, 0, R_movementVector.z * 9 * 10 / 8);
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
                        fireball.GetComponent<FireBall>().Team = _pav.TEAM;

                        fireball.transform.forward = -playerSkill.transform.forward;
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
                                    GetComponent<PhotonView>().RPC("AddCombustion", PhotonTargets.All, fireball.GetComponent<PhotonView>().viewID);
                                    


                                }
                                break;

                            case 2:
                                {
                                    //融合
                                    GetComponent<PhotonView>().RPC("AddFusion", PhotonTargets.All, fireball.GetComponent<PhotonView>().viewID);


                                }
                                break;

                            case 3:
                                {
                                    //濺散
                                    GetComponent<PhotonView>().RPC("AddSpatter", PhotonTargets.All, fireball.GetComponent<PhotonView>().viewID);

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
                            ice.transform.forward = -playerSkill.transform.forward;
                            ice.transform.Rotate(0, (i - 1) * 15, 0);

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
                        SR.GetComponent<ElectricLockRange>().PlayerOrEnemy = _pav.gameObject;
                        SR.GetComponent<ElectricLockRange>().Player = _pav.gameObject;
                        SR.GetComponent<ElectricLockRange>().Team = _pav.TEAM;
                        SR.transform.localScale = Skill_Img_DamageCircle.transform.localScale;
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
                                    SR.GetComponent<ElectricChain>().Team = _pav.TEAM;
                                    SR.GetComponent<ElectricChain>().Player = _pav.gameObject;
                                }
                                break;

                            case 3:
                                {
                                    //增幅
                                    //SR.AddComponent<Glacier>();
                                }
                                break;
                        }

                        Skill_Img_DamageCircle.transform.localPosition = new Vector3(0, -0.4f, 0);
                        Skill_Img_DamageCircle.SetActive(false);
                        Skill_Img_RangeCircle.transform.gameObject.SetActive(false);
                        SkillFire = false;
                        SkillStandBy = false;
                    }
                }
                break;
        }


    }

 
    [PunRPC]
    void AddCombustion(int fireball_ID)
    {
        PhotonView.Find(fireball_ID).gameObject.AddComponent<Combustion>();
    }

    [PunRPC]
    void AddFusion(int fireball_ID)
    {
        PhotonView.Find(fireball_ID).gameObject.AddComponent<Fusion>();  
    }

    [PunRPC]
    void AddSpatter(int fireball_ID)
    {
        PhotonView.Find(fireball_ID).gameObject.AddComponent<Spatter>();
    }


}
