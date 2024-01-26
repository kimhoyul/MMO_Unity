using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _speed = 10f;

	float _yAngle = 0f;
	float ratio = 0.2f;

	void Start()
	{
		Managers.Input.KeyAction -= OnKeyBoard;
		Managers.Input.KeyAction += OnKeyBoard;
	}

	void OnKeyBoard()
	{
		if (Input.GetKey(KeyCode.W))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), ratio);
			transform.position += _speed*Time.deltaTime*Vector3.forward;
		}

		if (Input.GetKey(KeyCode.S))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), ratio);
			transform.position += _speed*Time.deltaTime*Vector3.back;
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), ratio);
			transform.position += _speed*Time.deltaTime*Vector3.left;
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), ratio);
			transform.position += _speed*Time.deltaTime*Vector3.right;
		}
	}
}
