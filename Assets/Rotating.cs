using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour {

	bool rotating = false;
	float m_Angle = 0;
	public Vector3 point;
	public Vector3 axis;
	bool playerRot = false;

	public bool PlayerRot {
		set {
			playerRot = value;
			//renderer.material.color = new Color(0.75f, 0.75f, (playerRot ? 1 : 0.75f));
		}
	}

	/*
	void Awake(Vector3 point, Vector3 axis) {
		point = point;
		axis = axis;
	}
	*/

	// Use this for initialization
	void Start () {
		renderer.material.color = new Color(0.75f, 0.75f, 0.75f);
	}


	
	// Update is called once per frame
	void Update () {
		float angle = 0;
		bool player_stand;
		bool player_stand_this;

		GameObject player = GameObject.Find("Player");
		MoveCharacter script = player.GetComponent("MoveCharacter") as MoveCharacter;
		player_stand = (script.StandingFloor.GetComponent("Rotating") != null);
		player_stand_this = (script.StandingFloor == gameObject);

		if (!playerRot && player_stand) return; 

		if (Input.GetKey (KeyCode.Q))
			angle = -Time.deltaTime * 128;
		else if (Input.GetKey (KeyCode.W))
			angle = Time.deltaTime * 128;
		else
		{
			float dst = Mathf.Floor((m_Angle + 45) / 90) * 90;
			angle = dst - m_Angle;
			if (Mathf.Abs(angle) > 1) angle *= 0.125f;
			if (rotating && angle == 0) {
				rotating = false;
				Vector3 pos = transform.position;
				transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
				if (player_stand_this)
				{
					script.RecalcPlayerPos();
				}
			}
		}

		m_Angle += angle;
		while (m_Angle > 360)
			m_Angle -= 360;
		while (m_Angle < 0)
			m_Angle += 360;

		transform.RotateAround (point, axis, angle);

		if (angle != 0)
		{
			rotating = true;

			script.StopMoving();
			if (!player_stand)
				script.RecalcPlayerPos();

			if (player_stand_this) {
				player.transform.RotateAround (point, axis, angle);
			}
		}
	}
}
