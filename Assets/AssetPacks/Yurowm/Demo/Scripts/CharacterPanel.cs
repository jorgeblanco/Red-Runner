using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AssetPacks.Yurowm.Demo.Scripts
{
	public class CharacterPanel : MonoBehaviour {

		public GameObject character;
		public Transform weaponsPanel;
		public Transform actionsPanel;
		public Transform camerasPanel;
		public Button buttonPrefab;
		public Slider motionSpeed;

		Actions _actions;
		PlayerController _controller;
		Camera[] _cameras;

		void Start () {
			Initialize ();
		}

		void Initialize () {
			_actions = character.GetComponent<Actions> ();
			_controller = character.GetComponent<PlayerController> ();

			foreach (PlayerController.Arsenal a in _controller.arsenal)
				CreateWeaponButton(a.name);

			CreateActionButton("Stay");
			CreateActionButton("Walk");
			CreateActionButton("Run");
			CreateActionButton("Sitting");
			CreateActionButton("Jump"); 
			CreateActionButton("Aiming");
			CreateActionButton("Attack");
			CreateActionButton("Damage");
			CreateActionButton("Death Reset", "Death");

			_cameras = GameObject.FindObjectsOfType<Camera> ();
			var sort = from s in _cameras orderby s.name select s;

			foreach (Camera c in sort)
				CreateCameraButton(c);

			camerasPanel.GetChild (0).GetComponent<Button>().onClick.Invoke();
		}

		void CreateWeaponButton(string name) {
			Button button = CreateButton (name, weaponsPanel);
			button.onClick.AddListener(() => _controller.SetArsenal(name));
		}

		void CreateActionButton(string name) {
			CreateActionButton(name, name);
		}

		void CreateActionButton(string name, string message) {
			Button button = CreateButton (name, actionsPanel);
			button.onClick.AddListener(() => _actions.SendMessage(message, SendMessageOptions.DontRequireReceiver));
		}

		void CreateCameraButton (Camera c) {
			Button button = CreateButton (c.name, camerasPanel);
			button.onClick.AddListener(() => {
				ShowCamera(c);
			});
		}

		Button CreateButton(string name, Transform group) {
			GameObject obj = (GameObject) Instantiate (buttonPrefab.gameObject);
			obj.name = name;
			obj.transform.SetParent(group);
			obj.transform.localScale = Vector3.one;
			Text text = obj.transform.GetChild (0).GetComponent<Text> ();
			text.text = name;
			return obj.GetComponent<Button> ();
		}

		void ShowCamera (Camera cam) {
			foreach (Camera c in _cameras)
				c.gameObject.SetActive(c == cam);
		}

		void Update() {
			Time.timeScale = motionSpeed.value;
		}

		public void OpenPublisherPage() {
			Application.OpenURL ("https://www.assetstore.unity3d.com/en/#!/publisher/11008");
		}
	}
}
