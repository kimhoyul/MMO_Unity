using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
	[SerializeField] int _monsterCount = 0;
	[SerializeField] int _keepMonsterCount = 0;
	[SerializeField] Vector3 _spawnPos;
	[SerializeField] float _spawnRadius = 15.0f;
	[SerializeField] float _spawnTime = 5.0f;

	int reservedCount = 0;

	private void Start()
	{
		Managers.Game.OnSpawnEvent -= AddMonsterCount;
		Managers.Game.OnSpawnEvent += AddMonsterCount;
	}

	public void AddMonsterCount(int value) { _monsterCount += value; }
	public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

	private void Update()
	{
		while (reservedCount + _monsterCount < _keepMonsterCount)
		{
			StartCoroutine("ReserveSpawn");
		}
	}

	IEnumerator ReserveSpawn()
	{
		reservedCount++;
		yield return new WaitForSeconds(Random.Range(0.0f, _spawnTime));
		GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Warrior");
		NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

		Vector3 randPos;
		while (true)
		{
			Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
			randDir.y = 0.0f;
			randPos = _spawnPos + randDir;

			NavMeshPath path = new NavMeshPath();
			if (nma.CalculatePath(randPos, path))
				break;
		}

		obj.transform.position = randPos;
		reservedCount--;
	}
}
