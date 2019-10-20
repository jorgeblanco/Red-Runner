using UnityEngine;

namespace AssetPacks.Yurowm.Demo.Scripts
{
	public class CharacterViewer : MonoBehaviour {
	
		public Transform cameras;

		Transform _targetForCamera;
		Vector3 _deltaPosition;
		Vector3 _lastPosition = Vector3.zero;
		bool _rotating = false;

		void Awake () {
			_targetForCamera = GameObject.Find ("RigSpine3").transform;
			_deltaPosition = cameras.position - _targetForCamera.position;
		}

		void Update () {
			if (Input.GetMouseButtonDown (0) && Input.mousePosition.x < Screen.width * 0.6f) {
				_lastPosition = Input.mousePosition;
				_rotating = true;
			}

			if (Input.GetMouseButtonUp(0))
				_rotating = false;
		
			if (_rotating && Input.GetMouseButton(0))
				transform.Rotate(0, -300f * (Input.mousePosition - _lastPosition).x / Screen.width, 0);

			_lastPosition = Input.mousePosition;
		}

		void LateUpdate () {
			cameras.position += (_targetForCamera.position + _deltaPosition - cameras.position) * Time.unscaledDeltaTime * 5;
		}
	}
}
