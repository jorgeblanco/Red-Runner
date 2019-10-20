using UnityEngine;

namespace AssetPacks.Yurowm.Demo.Scripts
{
	[RequireComponent (typeof (Animator))]
	public class Actions : MonoBehaviour {

		private Animator _animator;

		private const int CountOfDamageAnimations = 3;
		private int _lastDamageAnimation = -1;
		private static readonly int AimingAnim = Animator.StringToHash("Aiming");
		private static readonly int SpeedAnim = Animator.StringToHash("Speed");
		private static readonly int AttackAnim = Animator.StringToHash("Attack");
		private static readonly int DeathAnim = Animator.StringToHash("Death");
		private static readonly int DamageId = Animator.StringToHash("DamageID");
		private static readonly int DamageAnim = Animator.StringToHash("Damage");
		private static readonly int SquatAnim = Animator.StringToHash("Squat");
		private static readonly int JumpAnim = Animator.StringToHash("Jump");

		private void Awake () {
			_animator = GetComponent<Animator> ();
		}

		public void Stay () {
			_animator.SetBool(AimingAnim, false);
			_animator.SetFloat (SpeedAnim, 0f);
		}

		public void Walk () {
			_animator.SetBool(AimingAnim, false);
			_animator.SetFloat (SpeedAnim, 0.5f);
		}

		public void Run () {
			_animator.SetBool(AimingAnim, false);
			_animator.SetFloat (SpeedAnim, 1f);
		}

		public void Attack () {
			Aiming ();
			_animator.SetTrigger (AttackAnim);
		}

		public void Death () {
			if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
				_animator.Play("Idle", 0);
			else
				_animator.SetTrigger (DeathAnim);
		}

		public void Damage () {
			if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) return;
			int id = Random.Range(0, CountOfDamageAnimations);
			if (CountOfDamageAnimations > 1)
				while (id == _lastDamageAnimation)
					id = Random.Range(0, CountOfDamageAnimations);
			_lastDamageAnimation = id;
			_animator.SetInteger (DamageId, id);
			_animator.SetTrigger (DamageAnim);
		}

		public void Jump () {
			_animator.SetBool (SquatAnim, false);
			_animator.SetFloat (SpeedAnim, 0f);
			_animator.SetBool(AimingAnim, false);
			_animator.SetTrigger (JumpAnim);
		}

		public void Aiming () {
			_animator.SetBool (SquatAnim, false);
			_animator.SetFloat (SpeedAnim, 0f);
			_animator.SetBool(AimingAnim, true);
		}

		public void Sitting () {
			_animator.SetBool (SquatAnim, !_animator.GetBool(SquatAnim));
			_animator.SetBool(AimingAnim, false);
		}
	}
}
