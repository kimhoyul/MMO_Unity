using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager
{
	#region Pool
	class Pool
	{
		public GameObject Original { get; private set; }
		public Transform Root { get; set; }

		Stack<Poolable> _poolStack = new Stack<Poolable>();

		public void Init(GameObject original, int count = 5)
		{
			Original = original;
			Root = new GameObject().transform;
			Root.name = $"{original.name}_Root";

			for (int i = 0; i < count; i++)
				Push(Create());
		}

		Poolable Create()
		{
			GameObject go = Object.Instantiate<GameObject>(Original);
			go.name = Original.name;
			return go.GetOrAddComponent<Poolable>();
		}

		public void Push(Poolable poolable)
		{
			if (poolable == null)
				return;

			poolable.transform.parent = Root;
			poolable.gameObject.SetActive(false);
			poolable.IsUsing = false;

			_poolStack.Push(poolable);
		}

		public Poolable Pop(Transform parent)
		{
			Poolable poolable;

			if (_poolStack.Count > 0)
				poolable = _poolStack.Pop();
			else
				poolable = Create();

			poolable.gameObject.SetActive(true);

			if (parent == null)
				poolable.transform.parent = Managers.Scene.CurrentScene.transform;

			poolable.transform.parent = parent;
			poolable.IsUsing = true;

			return poolable;
		}
	}
	#endregion

	Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
	Transform _root;

	public void Init()
	{
		if (_root == null)
		{
			_root = new GameObject { name = "@PoolRoot" }.transform;
			Object.DontDestroyOnLoad(_root);
		}
	}

	public void CreatePool(GameObject original, int count = 5)
	{
		Pool pool = new Pool();
		pool.Init(original, count);
		pool.Root.parent = _root;

		_pools.Add(original.name, pool);

	}

	public void Push(Poolable poolable)
	{
		string name = poolable.gameObject.name;
		if (_pools.ContainsKey(name) == false)
		{
			GameObject.Destroy(poolable.gameObject);
			return;
		}
		_pools[name].Push(poolable);
	}

	public Poolable Pop(GameObject original, Transform parent = null)
	{
		if (_pools.ContainsKey(original.name) == false)
			CreatePool(original);

		return _pools[original.name].Pop(parent);
	}

	public GameObject GetOriginal(string name)
	{
		if (_pools.ContainsKey(name) == false)
			return null;

		return _pools[name].Original;
	}

	public void Clear()
	{
		foreach (var pool in _pools.Values)
		{
			foreach (var poolable in pool.Root.GetComponentsInChildren<Poolable>())
				GameObject.Destroy(poolable.gameObject);

			GameObject.Destroy(pool.Root.gameObject);
		}

		_pools.Clear();
	}
}
