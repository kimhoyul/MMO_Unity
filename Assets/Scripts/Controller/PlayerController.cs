using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _speed = 10f;

	Vector3 _destPos;

	void Start()
	{
		Managers.Input.MouseAction -= OnMouseClicked;
		Managers.Input.MouseAction += OnMouseClicked;
	}

	public enum PlayerState
	{
		Die,
		Moving,
		Idle,
	}

	PlayerState _state = PlayerState.Idle;

	private void UpdateDie()
	{

	}

	private void UpdateMoving()
	{
		Vector3 dir = _destPos - transform.position;
		if (dir.magnitude < 0.0001f)
		{
			_state = PlayerState.Idle;
		}
		else
		{
			float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}

		Animator anim = GetComponent<Animator>();
		anim.SetFloat("speed", _speed);
	}

	private void UpdateIdle()
	{
		Animator anim = GetComponent<Animator>();
		anim.SetFloat("speed", 0);
	}

	private void Update()
	{
		switch (_state)
		{
			case PlayerState.Die:
				UpdateDie();
				break;
			case PlayerState.Moving:
				UpdateMoving();
				break;
			case PlayerState.Idle:
				UpdateIdle();
				break;
		}
	}

	private void OnMouseClicked(Define.MouseEvent evt)
	{
		if (_state == PlayerState.Die)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);

		if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Wall")))
		{
			_destPos = hit.point;
			_state = PlayerState.Moving;
		}
	}

}
