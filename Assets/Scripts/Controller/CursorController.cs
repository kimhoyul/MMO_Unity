using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
	int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

	Texture2D _attackIcon;
	Texture2D _handIcon;

	enum CursorType
	{
		None,
		Hand,
		Attack,
	}

	CursorType _cursorType = CursorType.None;

	void Start()
    {
		_attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
		_handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
	}

	void Update()
    {
		if (Input.GetMouseButton(0))
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit, 100f, _mask))
		{
			if (hit.collider.gameObject.layer  == (int)Define.Layer.Monster)
			{
				if (_cursorType != CursorType.Attack)
				{
					_cursorType = CursorType.Attack;
					Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
				}
			}
			else
			{
				if (_cursorType != CursorType.Hand)
				{
					_cursorType = CursorType.Hand;
					Cursor.SetCursor(_handIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
				}
			}
		}
	}
}
