using UnityEngine;

public class PoolManager
{
	Transform _root;

	public void Init()
	{
		if (_root == null)
		{
			_root = new GameObject { name = "@PoolRoot" }.transform;
			Object.DontDestroyOnLoad(_root);
		}
	}

	public void Push(Poolable poolable)
	{
		if (poolable == null)
			return;

		poolable.transform.parent = _root;
		poolable.gameObject.SetActive(false);
	}

	public Poolable Pop(Poolable original, Transform parent = null)
	{
		if (original == null)
			return null;

		Poolable poolable = null;
		if (parent == null)
			parent = _root;

		foreach (Transform child in parent)
		{
			if (child.name.Contains(original.name) && !child.gameObject.activeSelf)
			{
				poolable = child.GetComponent<Poolable>();
				if (poolable != null)
				{
					poolable.gameObject.SetActive(true);
					poolable.transform.parent = null;
					break;
				}
			}
		}

		if (poolable == null)
		{
			poolable = Object.Instantiate(original, parent);
			poolable.name = original.name;
		}

		return poolable;
	}
}
