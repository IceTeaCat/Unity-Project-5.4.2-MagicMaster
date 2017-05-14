using UnityEngine;
using System.Collections;

public class RightAnalogy : MonoBehaviour {

	public RectTransform handle;
	public RectTransform rect;

	public GameObject Player;

	public GameObject SkillImage;
	public GameObject SkillParticle;

	CharacterController _characterController;
	public float MovementSpeed=5;

	private int TouchID;

	private bool dragging = false;

	void Start () 
	{
		_characterController = SkillImage.GetComponent<CharacterController> ();
	}


	void Update () 
	{
		Drag ();
	}


	public void StartDrag()
	{
		dragging = true;
		SkillImage.SetActive (true);

		if(Input.touchCount!=0)
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).position.x > 700 && Input.GetTouch (i).position.y < 300)
					TouchID = i;
			}
	}
	public void Drag()
	{
		if (!dragging) 
		{
			return;
		}

		//Vector2 mPos = Input.mousePosition;

		Vector2 mPos=Input.GetTouch (TouchID).position;
		Vector2 newPos = mPos - rect.anchoredPosition;
		Vector2 pos = Vector2.ClampMagnitude (newPos, 70);

		handle.anchoredPosition = pos;

		//物件移動
		Vector2 dir = (pos/7)*MovementSpeed*Time.deltaTime;
		//obj.Translate(dir.x,0,dir.y);
		var inputVector = new Vector3 (dir.x, dir.y);
		Vector3 movementVector = Vector3.zero;


		if (inputVector.sqrMagnitude > 0.00001f)
		{
			movementVector = Camera.main.transform.TransformDirection(inputVector);
			movementVector.y = 0f;
			//movementVector.Normalize();
			SkillImage.transform.forward = movementVector;
		}

		//movementVector += Physics.gravity;
		//_characterController.Move(movementVector * Time.deltaTime*MovementSpeed);





	}
	public void StopDrag()
	{
		dragging = false;
		SkillImage.SetActive (false);
		handle.anchoredPosition = Vector2.zero;

		GameObject fireball = Instantiate (SkillParticle, SkillImage.transform.position, Quaternion.identity) as GameObject;
		fireball.transform.forward = -SkillImage.transform.forward;
		print (fireball.transform.rotation.eulerAngles);
		Player.transform.forward = SkillImage.transform.forward;

	}


}
