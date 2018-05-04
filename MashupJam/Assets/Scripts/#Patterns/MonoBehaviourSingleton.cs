using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameJam.Core
{
	public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
	{
		public static T Instance_ { get; private set; }

		protected virtual void Awake()
		{
			if (Instance_ == null)
			{
				Instance_ = this as T;
				if (Instance_.transform.parent == null)
				{
					DontDestroyOnLoad(Instance_.gameObject);
				}
			}
			else if (Instance_ != this)
				Destroy(this.gameObject);
		}
	}
}