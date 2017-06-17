using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour {

    public Text nameTag;

    private void Start()
    {
        nameTag.text = GetComponent<PlayerAbilityValue>().PLAYER_NAME;
    }


    private void Update()
    {

        //nameTag.text = transform.parent.GetComponent<PlayerAbilityValue>() .PLAYER_NAME+ " Team:" + (transform.parent.GetComponent<PlayerAbilityValue>().TEAM+1) + " Skill:"+ transform.parent.GetComponent<PlayerAbilityValue>().SKILL + " AdvSkill:" + transform.parent.GetComponent<PlayerAbilityValue>().ADVANCED_SKILL + " Hp:" + transform.parent.GetComponent<PlayerAbilityValue>().HEALTH;
    }
}
