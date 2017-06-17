using UnityEngine;
using System.Collections;
using CnControls;

public class PlayerController : Photon.MonoBehaviour {

    PlayerAbilityValue _pav;
    public Animator _anim;

	public GameObject PlayerCharacter;
	CharacterController _characterController;

	private Vector3 correctPlayerPos = Vector3.zero; 
	private Quaternion correctPlayerRot = Quaternion.identity;
	private bool appliedInitialUpdate;

    public bool isDown;

    void Awake()
    {

    }
    
    void Start () {
        _pav = GetComponent<PlayerAbilityValue>();
        _anim = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        _characterController = PlayerCharacter.GetComponent<CharacterController> ();
    }


    void Update () {
        if (photonView.isMine) {
			//Camera.main.transform.parent = transform;
			Camera.main.GetComponent<OrthographicCamera> ().target = this.gameObject.transform;
            //Camera.main.transform.localPosition = new Vector3(0, 13, -10);

            if (!GetComponent<PlayerSkill>().Fireing)
            {
                MoveJoystick();

                if (!isDown)
                    _anim.SetFloat("Speed", 0);
            }
        }
		else
		{
			transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            PlayerCharacter.transform.rotation = Quaternion.Lerp(PlayerCharacter.transform.rotation, correctPlayerRot, Time.deltaTime * 5);
		}


	}


    void MoveJoystick()
    {
        Vector3 L_inputVector = new Vector3(CnInputManager.GetAxis("Horizontal1"), CnInputManager.GetAxis("Vertical1"));
        Vector3 L_movementVector = Vector3.zero;

        if (L_inputVector.sqrMagnitude > 0.001f)
        {
            L_movementVector = Camera.main.transform.TransformDirection(L_inputVector);
            L_movementVector.y = 0f;
            PlayerCharacter.transform.forward = L_movementVector;
            transform.position += L_movementVector * Time.deltaTime * _pav.MOVE_SPEED;
            //print(L_movementVector * Time.deltaTime * _pav.MOVE_SPEED);
            float temp = Mathf.Abs(L_movementVector.magnitude * Time.deltaTime * _pav.MOVE_SPEED);
            if (isDown)
                _anim.SetFloat("Speed", temp);


        }
        L_movementVector += Physics.gravity;
    }




	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(PlayerCharacter.transform.rotation);
		}

		else
		{
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();

            if (!appliedInitialUpdate)
			{
				appliedInitialUpdate = true;
				transform.position = correctPlayerPos;
                PlayerCharacter.transform.rotation = correctPlayerRot;
			}
		}
	}


}



