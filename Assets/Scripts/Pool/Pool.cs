using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimplePool
{
	public class Pool : MonoBehaviour
	{
		#region Singleton
		public static Pool Instance
		{
			get
			{
				if (instance == null)
				{
					instance = GameObject.FindObjectOfType<Pool>();
					instance.pools = new List<Part>();
					instance.CreateDefault();
				}

				return instance;
			}
		}
		private static Pool instance;
		#endregion

		[SerializeField] private PoolComponent prefab;
		[SerializeField] private new string name = "SimplePool";
		[SerializeField] private int count = 1;
		[SerializeField] private bool autoResize = true;

		private List<Part> pools;

		private struct Part
		{
			public string name;
			public List<PoolComponent> prefab;
			public bool resize;
			public Transform parent;
		}

		public void CreateDefault()
        {
			CreatePool(prefab, name, count, autoResize);
		}

		public void CreatePool(PoolComponent sample, string name, int count, bool autoResize)
		{
			if (pools == null || count <= 0 || name.Trim() == string.Empty || sample == null) return;

			Part p = new Part();
			p.prefab = new List<PoolComponent>();
			p.name = name;
			p.resize = autoResize;
			p.parent = new GameObject("Pool-" + name).transform;

			for (int i = 0; i < count; i++)
			{
				p.prefab.Add(AddObject(sample, name, i, p.parent));
			}

			pools.Add(p);
			Debug.Log(" Добавлен пул: " + name);
		}

		private PoolComponent AddObject(PoolComponent sample, string name, int index, Transform parent)
		{
			PoolComponent comp = GameObject.Instantiate(sample) as PoolComponent;
			comp.gameObject.name = name + "-" + index;
			comp.transform.parent = parent;
			comp.gameObject.SetActive(false);
			return comp;
		}

		private void AutoResize(Part part, int index)
		{
			part.prefab.Add(AddObject(part.prefab[0], part.name, index, part.parent));
		}

		public PoolComponent GetObject(string name, Vector3 position, Quaternion rotation)
		{
			if (pools == null) return null;

			foreach (Part part in pools)
			{
				if (string.Compare(part.name, name) == 0)
				{
					foreach (PoolComponent comp in part.prefab)
					{
						if (!comp.isActiveAndEnabled)
						{
							comp.transform.rotation = rotation;
							comp.transform.position = position;
							comp.gameObject.SetActive(true);
							return comp;
						}
					}

					if (part.resize)
					{
						AutoResize(part, part.prefab.Count);
						PoolComponent comp = part.prefab[part.prefab.Count - 1];
						comp.transform.rotation = rotation;
						comp.transform.position = position;
						comp.gameObject.SetActive(true);
						return comp;
					}
				}
			}

			return null;
		}

		public void DestroyPool(string name)
		{
			if (pools == null) return;

			int j = 0;

			foreach (Part p in pools)
			{
				if (string.Compare(p.name, name) == 0)
				{
					GameObject.Destroy(p.parent.gameObject);
					pools.RemoveAt(j);
					Debug.Log(" Уничтожен пул: " + name);
					return;
				}

				j++;
			}
		}
	}
}