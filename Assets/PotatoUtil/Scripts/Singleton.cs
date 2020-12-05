using System;
using UnityEngine;

namespace PotatoUtil {

	/// <summary>
	/// Monobehaviour based singleton class that allows only
	/// internal access to the singleton itself. Any external
	/// access must be dictated/allowed by the inheriting class.
	/// </summary>
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

		protected static T Instance {
			get {
				if (IsNull) {
					throw new Exception(string.Format(
						"No singleton of type `{0}' has been initialized!",
						typeof(T).Name
					));
				}
				return m_instance;
			}
		}
		private static T m_instance = null;

		protected static bool IsNull {
			get { return m_instance == null; }
		}

		private void Awake() {
			if (m_instance == null) {
				m_instance = this as T;
				OnAwake();
			} else {
				Destroy(this);
			}
		}
		private void OnDestroy() {
			if (m_instance == this) {
				m_instance = null;
				OnDestroyed();
			}
		}

		protected virtual void OnAwake() {
			// override to implement custom behaviour
		}
		protected virtual void OnDestroyed() {
			// override to implement custom behaviour
		}

	}

}