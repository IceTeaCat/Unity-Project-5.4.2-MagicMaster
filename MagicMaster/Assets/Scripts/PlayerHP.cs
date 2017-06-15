using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    public Image HPBar;
    public int MaxHP;
    public int NowHP;

    void Start()
    {
        MaxHP = GetComponent<PlayerAbilityValue>().HEALTH;
    }


    void Update()
    {
        NowHP = GetComponent<PlayerAbilityValue>().HEALTH;

        if (NowHP > 0)
            HPBar.transform.localScale = new Vector3((float)NowHP / MaxHP, 1, 1);
        else
            HPBar.transform.localScale = new Vector3(0, 1, 1);

    }
}
