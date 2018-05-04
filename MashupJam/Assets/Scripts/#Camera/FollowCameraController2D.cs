using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameJam.Core
{
	public class FollowCameraController2D : MonoBehaviourSingleton<FollowCameraController2D> 
	{
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);

		[Range(0f, 20f)]
		[SerializeField] private float _smoothingFactor = 5f;

		public void FixedUpdate()
		{
			this.transform.position = Vector3.Lerp(this.transform.position, this._target.transform.position + this._offset, this._smoothingFactor * Time.deltaTime);
		}

#if UNITY_EDITOR

		public void LateUpdate()
		{
			if (!Application.isPlaying)
			{
				this.transform.position = Vector3.Lerp(this.transform.position, this._target.transform.position + this._offset, this._smoothingFactor * Time.deltaTime);
			}
		}

		//protected override void OnDrawGizmos()
		//{

		//}
#endif
	}
}

namespace GameJam.Core.UTILITY
{
#if UNITY_EDITOR
	[CustomEditor(typeof(FollowCameraController2D))]
	[CanEditMultipleObjects]
	public class FollowCameraController2DEditor : Editor
	{
		private Object _something;

		private void OnEnable()
		{
			if (this._something == null)
			{
				EditorApplication.update += (target as FollowCameraController2D).LateUpdate;
				this._something = target;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

#pragma warning disable 0219
			FollowCameraController2D sFollowCameraController2D = target as FollowCameraController2D;
#pragma warning restore 0219
		}
	}
#endif
}