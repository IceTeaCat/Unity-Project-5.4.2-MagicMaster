using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour {

    public AudioClip[] AllSound;

    public AudioClip[] PlayerSound;


    private AudioSource _AS;



	// Use this for initialization
	void Start () {
        _AS = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void BtnDown()
    {
        _AS.clip = AllSound[0];
        _AS.Play();
    }

    public void ReturnDown()
    {
        _AS.clip = AllSound[1];
        _AS.Play();
    }

    public void BuyBtnDown()
    {
        _AS.clip = AllSound[2];
        _AS.Play();
    }

    public void ChangeTeamBtnDown()
    {
        _AS.clip = AllSound[3];
        _AS.Play();
    }

    public void ChangeSkillBtnDown()
    {
        _AS.clip = AllSound[4];
        _AS.Play();
    }


    public void PlayerSkill(int s)
    {
        _AS.clip = PlayerSound[s];
        _AS.Play();
    }

    public void Victory()
    {
        _AS.clip = AllSound[5];
        _AS.Play();
    }

    public void Lose()
    {
        _AS.clip = AllSound[6];
        _AS.Play();
    }


}
