using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
	Stat _stat;

	public override void Init()
	{
		_stat = gameObject.GetComponent<Stat>();

		if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
	}

	protected override void UpdateMoving()
	{

	}

	protected override void UpdateIdle()
	{
		
	}

	protected override void UpdateSkill()
	{

	}
}
