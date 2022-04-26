using System;
using UnityEngine;

namespace PotatoUtil {

	public abstract class ServiceBehaviour : MonoBehaviour, IService {

		public abstract Type Key { get; }

		public virtual void OnServiceRegistered() {
			// override to implement custom behaviour
		}
		public virtual void OnServiceDeregistered() {
			// override to implement custom behaviour
		}
	}

}