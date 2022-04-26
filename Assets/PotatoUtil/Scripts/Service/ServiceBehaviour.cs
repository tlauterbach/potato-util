using UnityEngine;

namespace PotatoUtil {

	public abstract class ServiceBehaviour : MonoBehaviour, IService {
		public virtual void OnServiceRegistered() {
			// override to implement custom behaviour
		}
		public virtual void OnServiceDeregistered() {
			// override to implement custom behaviour
		}
	}

}