using UnityEngine;
using System.Collections;
using CnControls;

public class PlayerSkill : Photon.MonoBehaviour
{
    public GameObject R_Joystick;

    Vector3 R_inputVector;
    Vector3 R_movementVector;

    PlayerAbilityValue _pav;
    Animator _anim;

    public GameObject PlayerCharacter;

    public bool CanFire = true;
    public bool Fireing = false;
    public float CDtime=0;

    public GameObject playerSkill;
    public GameObject Skill_Img_Arrow;
    public GameObject Skill_Img_RangeCircle;
    public GameObject Skill_Img_DamageCircle;

    public GameObject ElectricLockRange;

    public bool SkillStandBy = false;
    public bool SkillFire = false;

    float FireDelay = 0.05f;

    void Start()
    {
        _pav = GetComponent<PlayerAbilityValue>();
        _anim = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        
        if (_pav.SKILL == 2 && _pav.ADVANCED_SKILL == 3)
        {
            GameObject GR = PhotonNetwork.Instantiate("GlacierRange", transform.position, Quaternion.identity, 0);
            photonView.RPC("SetGRParent", PhotonTargets.AllBuffered, new object[] { GR.GetComponent<PhotonView>().viewID, GetComponent<PhotonView>().viewID });

            //GR.transform.parent = gameObject.transform;
        }

        if (_pav.SKILL == 3 && _pav.ADVANCED_SKILL == 3)
        {
            GameObject EIR = PhotonNetwork.Instantiate("ElectricIncreaseRange", transform.position, Quaternion.identity, 0);
            photonView.RPC("SetEIRParent", PhotonTargets.AllBuffered, new object[] { EIR.GetComponent<PhotonView>().viewID, GetComponent<PhotonView>().viewID });

            //EIR.transform.parent = gameObject.transform;
            //EIR.GetComponent<ElectricIncreaseRange>().Team = _pav.TEAM;

        }


    }


    void Update()
    {
        if (photonView.isMine && CanFire)
        {
            SkillJoystick();
            Skill_Function(_pav.SKILL, _pav.ADVANCED_SKILL);
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            Fireing = true;
        else
            Fireing = false;


        if (Fireing)
            FireDelay -= Time.deltaTime;
        else
            FireDelay = 0.05f;

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
                        Skill_Img_DamageCircle.transform.localPosition = new Vector3(R_movementVector.x * 9, 0, R_movementVector.z * 9 * 10 / 8);
                        //R_movementVector.Normalize();
                        //playerSkill.transform.forward = R_movementVector;
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
                        Fireing = true;
                        PlayerCharacter.transform.forward = playerSkill.transform.forward;
                        if (FireDelay <= 0)
                        {
                            GameObject fireball = PhotonNetwork.Instantiate("FireBall_small", Skill_Img_Arrow.transform.position, Quaternion.identity, 0) as GameObject;
                            GetComponent<PhotonView>().RPC("Init_Fireball", PhotonTargets.All, fireball.GetComponent<PhotonView>().viewID);

                            fireball.transform.forward = -playerSkill.transform.forward;
                            playerSkill.transform.rotation = Quaternion.identity;
                            SkillFire = false;
                            SkillStandBy = false;
              

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
                        Skill_Img_Arrow.transform.gameObject.SetActive(false);
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
                        Fireing = true;
                        PlayerCharacter.transform.forward = playerSkill.transform.forward;
                        if (FireDelay <= 0)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject ice = PhotonNetwork.Instantiate("Ice", Skill_Img_Arrow.transform.position, Quaternion.identity, 0) as GameObject;
                                GetComponent<PhotonView>().RPC("Init_Ice", PhotonTargets.All, ice.GetComponent<PhotonView>().viewID);
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
                                            GetComponent<PhotonView>().RPC("AddFrozen", PhotonTargets.All, ice.GetComponent<PhotonView>().viewID);
                                        }
                                        break;

                                    case 2:
                                        {
                                            //寒風
                                            GetComponent<PhotonView>().RPC("AddChill", PhotonTargets.All, ice.GetComponent<PhotonView>().viewID);
                                        }
                                        break;

                                    case 3:
                                        {
                                            //刺骨
                                            GetComponent<PhotonView>().RPC("AddGlacier", PhotonTargets.All, ice.GetComponent<PhotonView>().viewID);
                                        }
                                        break;
                                }

                            }
                            playerSkill.transform.rotation = Quaternion.identity;
                            SkillFire = false;
                            SkillStandBy = false;
                            
                        }
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
                        Fireing = true;
                        PlayerCharacter.transform.LookAt(Skill_Img_DamageCircle.transform);
                        PlayerCharacter.transform.rotation = Quaternion.Euler(0, PlayerCharacter.transform.rotation.eulerAngles.y, 0);
                        //PlayerCharacter.transform.forward = playerSkill.transform.forward;
                        if (FireDelay <= 0)
                        {
                            GameObject sr = PhotonNetwork.Instantiate("ElectricLockRange", Skill_Img_DamageCircle.transform.position, Quaternion.identity, 0);
                            GetComponent<PhotonView>().RPC("CreateSR", PhotonTargets.All, sr.GetComponent<PhotonView>().viewID);

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
                                        GetComponent<PhotonView>().RPC("AddElectricMultiple", PhotonTargets.All, sr.GetComponent<PhotonView>().viewID);
                                    }
                                    break;

                                case 2:
                                    {
                                        //連鎖
                                        GetComponent<PhotonView>().RPC("AddElectricChain", PhotonTargets.All, sr.GetComponent<PhotonView>().viewID);
                                        /*
                                        sr.AddComponent<ElectricChain>();
                                        sr.GetComponent<ElectricChain>().Team = _pav.TEAM;
                                        sr.GetComponent<ElectricChain>().Player = _pav.gameObject;
                                        */
                                    }
                                    break;

                                case 3:
                                    {
                                        //增幅
                                        //sr.AddComponent<>();
                                    }
                                    break;
                            }

                            Skill_Img_DamageCircle.transform.localPosition = new Vector3(0, -0.4f, 0);
                            //playerSkill.transform.rotation = Quaternion.identity;
                            SkillFire = false;
                            SkillStandBy = false;
                        }
  
                        Skill_Img_DamageCircle.SetActive(false);
                        Skill_Img_RangeCircle.transform.gameObject.SetActive(false);
                    }
                }
                break;
        }


    }

    [PunRPC]
    void SetGRParent(int GR_ID, int obj)
    {
        PhotonView.Find(GR_ID).gameObject.transform.parent = PhotonView.Find(obj).gameObject.transform;
        PhotonView.Find(GR_ID).gameObject.transform.localPosition = Vector3.zero;
    }

    [PunRPC]
    void SetEIRParent(int EIR_ID, int obj)
    {
        PhotonView.Find(EIR_ID).gameObject.transform.parent = PhotonView.Find(obj).gameObject.transform;
        PhotonView.Find(EIR_ID).gameObject.transform.localPosition = Vector3.zero;
    }


    [PunRPC]
    void SetCanFire(bool x)
    {
        CanFire = x;
    }



    //-------------------------------
    [PunRPC]
    void AddCombustion(int fireball_ID)
    {
        PhotonView.Find(fireball_ID).gameObject.AddComponent<Combustion>();
        PhotonView.Find(fireball_ID).gameObject.GetComponent<Combustion>().Host=gameObject;
        PhotonView.Find(fireball_ID).gameObject.GetComponent<Combustion>().Team = _pav.TEAM;
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

    //-------------------------------
    [PunRPC]
    void AddFrozen(int ice_ID)
    {
        PhotonView.Find(ice_ID).gameObject.AddComponent<Frozen>();
    }

    [PunRPC]
    void AddChill(int ice_ID)
    {
        PhotonView.Find(ice_ID).gameObject.AddComponent<Chill>();
    }
    
    [PunRPC]
    void AddGlacier(int ice_ID)
    {
        PhotonView.Find(ice_ID).gameObject.AddComponent<Glacier>();
    }

    //-------------------------------
    [PunRPC]
    void CreateSR(int sr_ID)
    {
        //PhotonView.Find(sr_ID).gameObject.AddComponent<ElectricMultiple>();

        PhotonView.Find(sr_ID).gameObject.GetComponent<ElectricLockRange>().PlayerOrEnemy = _pav.gameObject;
        PhotonView.Find(sr_ID).gameObject.GetComponent<ElectricLockRange>().Player = _pav.gameObject;
        PhotonView.Find(sr_ID).gameObject.GetComponent<ElectricLockRange>().Team = _pav.TEAM;
        //PhotonView.Find(sr_ID).gameObject.transform.localScale = Skill_Img_DamageCircle.transform.localScale;

    }



    [PunRPC]
    void AddElectricMultiple(int sr_ID)
    {
        PhotonView.Find(sr_ID).gameObject.AddComponent<ElectricMultiple>();
    }

    [PunRPC]
    void AddElectricChain(int sr_ID)
    {
        PhotonView.Find(sr_ID).gameObject.AddComponent<ElectricChain>();
        PhotonView.Find(sr_ID).gameObject.GetComponent<ElectricChain>().Team = _pav.TEAM;
        PhotonView.Find(sr_ID).gameObject.GetComponent<ElectricChain>().Player = _pav.gameObject;
    }

    [PunRPC]
    void AddElectricIncrease(int sr_ID)
    {
        //PhotonView.Find(ice_ID).gameObject.AddComponent<>();
    }


    //---初始化火球---
    [PunRPC]
    void Init_Fireball(int Fireball_ID)
    {
        PhotonView.Find(Fireball_ID).GetComponent<FireBall>().Host = gameObject;
        PhotonView.Find(Fireball_ID).GetComponent<FireBall>().Team = _pav.TEAM;
    }

    //---初始化冰---
    [PunRPC]
    void Init_Ice(int Ice_ID)
    {
        PhotonView.Find(Ice_ID).GetComponent<Ice>().Host = gameObject;
        PhotonView.Find(Ice_ID).GetComponent<Ice>().Team = _pav.TEAM;
    }


}
