using UnityEngine;
using System.Collections;

public class Magic : Photon.MonoBehaviour {

    #region 宣告數值區
    [Header("-----Magic_Host-----")]
    [Tooltip("是誰發射的技能")]
    //法術主人
    public GameObject Host;

    [Header("-----Magic_Team-----")]
    [Tooltip("法術是哪一隊")]
    //法術隊伍
    public int Team = -1;


    [Header("-----Magic_BornTime-----")]
    [Tooltip("法術是什麼時候發射的")]
    //法術出生時間
    public float BornTime = 0;

    [Header("-----Magic_LifeTime-----")]
    [Tooltip("法術可以存活多久")]
    //法術壽命
    public float LifeTime = 5.0f;

    [Header("-----Magic_Type-----")]
    [Tooltip("法術的類型")]
    //法術類型
    public int Type = 1;

    [Header("-----Magic_Level-----")]
    [Tooltip("法術的等級")]
    //法術等級
    public int MagicLevel = 1;

    [Header("-----Magic_Power-----")]
    [Tooltip("法術的威力")]
    //法術威力
    public int Power = 10;

    [Header("-----Magic_MoveSpeed-----")]
    [Tooltip("法術的移動速度")]
    //法術移動速度
    public float MoveSpeed = 10;

    [Header("-----Magic_HitFX-----")]
    [Tooltip("法術的擊中特效")]
    public GameObject[] HitFX;

    [Header("-----Magic_isDestroy-----")]
    [Tooltip("法術是否已經破壞")]
    //法術是否破壞
    public bool IsDestroy = false;

    [Header("-----Magic_Target-----")]
    [Tooltip("法術是否已經破壞")]
    //法術是否破壞
    public GameObject Target;
    #endregion

    #region 功能區

    //生成擊中特效
    [PunRPC]
    public void HitFX_Function(int Number, Vector3 TargetPos)
    {
        if (HitFX[0] != null)
            Instantiate(HitFX[0], TargetPos, Quaternion.identity);
        print("HitFX_Function");
    }



    #endregion

}
