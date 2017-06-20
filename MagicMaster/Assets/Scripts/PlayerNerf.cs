using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNerf : Photon.MonoBehaviour
{
    public Image NerfImage;
    public Sprite[] NerfIcon;
    public bool[] Isnerf = new bool[5];

    void Update()
    {
        if (photonView.isMine)
        {
            CheckNerf();
            ShowNerf();
        }
    }


    void CheckNerf()
    {
        if(transform.FindChild("CombustionEffect(Clone)"))
            Isnerf[0] = true;
        else
            Isnerf[0] = false;

        if (GetComponent<FrozenDamage>())
            Isnerf[1] = true;
        else if (!GetComponent<FrozenDamage>())
            Isnerf[1] = false;

        if (GetComponent<ChillSlowEffect>())
            Isnerf[2] = true;
        else if (!GetComponent<ChillSlowEffect>())
            Isnerf[2] = false;

        if (GetComponent<GlacierEffect>())
        {
            if (GetComponent<GlacierEffect>().EndTime > 0)
                Isnerf[3] = true;
            else if (GetComponent<GlacierEffect>().EndTime <= 0)
                Isnerf[3] = false;
        }

        if (GetComponent<DizzyEffect>())
            Isnerf[4] = true;
        else if (!GetComponent<DizzyEffect>())
            Isnerf[4] = false;
    }

    void ShowNerf()
    {
        if (GameObject.Find("Code").GetComponent<GM>().GAMESTART)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject.Find("NerfGroup").transform.GetChild(i).gameObject.SetActive(Isnerf[i]);
            }
        }
    }


}
