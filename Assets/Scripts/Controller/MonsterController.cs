using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
	Stat _stat;
	
	// TODO : Data 로 뺄 것
	[SerializeField] float _scanRange = 10.0f;
	[SerializeField] float _attackRange = 2.0f;

	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Monster;
		_stat = gameObject.GetComponent<Stat>();

		if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
	}

	protected override void UpdateIdle()
	{
		GameObject player = Managers.Game.GetPlayer();
		if (player == null)
			return;

		float distance = (player.transform.position - transform.position).magnitude;
		if (distance <= _scanRange)
		{
			_lockTarget = player;
			_destPos = player.transform.position;
			State = Define.State.Moving;
			return;
		}
	}

	protected override void UpdateMoving()
	{
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= _attackRange)
			{
				NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
				nma.SetDestination(transform.position);
				State = Define.State.Skill;
				return;
			}
		}

		// 이동
		Vector3 dir = _destPos - transform.position;
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{
			NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
			nma.SetDestination(_destPos);
			nma.speed = _stat.MoveSpeed;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}
	}

	protected override void UpdateSkill()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}

	private void OnHitEvent()
	{
		if (_lockTarget != null)
		{
			Stat targetStat = _lockTarget.GetComponent<Stat>();
			targetStat.OnAttacked(_stat);

			if (targetStat.Hp > 0)
			{
				float distenace = (_lockTarget.transform.position - transform.position).magnitude;
				if (distenace <= _attackRange)
					State = Define.State.Skill;
				else
					State = Define.State.Moving;
			}
			else
			{
				_lockTarget = null;
				State = Define.State.Idle;
			}
		}
		else
		{
			State = Define.State.Idle;
		}
	}

	private void FootR()
	{
		Debug.Log("FootR");
	}

	private void FootL()
	{
		Debug.Log("FootL");
	}	
}
