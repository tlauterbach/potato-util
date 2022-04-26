using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PotatoUtil {

	public class ServiceCache : IServiceProvider {

		private Dictionary<Type, IService> m_cache;

		public ServiceCache() {
			m_cache = new Dictionary<Type, IService>();
		}

		public void Register(IService service) {
			if (m_cache.ContainsKey(service.Key)) {
				throw new Exception(string.Format(
					"Service of type `{0}' is already " +
					"registered to the ServiceCache.",
					service.Key.Name
				));
			} else {
				m_cache.Add(service.Key, service);
				service.OnServiceRegistered();
			}
		}
		public void RegisterFromHierarchy(Transform transform) {
			IService[] services = transform.GetComponentsInChildren<IService>();
			foreach (IService service in services) {
				Register(service);
			}
		}
		public void RegisterFromScene(Scene scene) {
			GameObject[] roots = scene.GetRootGameObjects();
			foreach (var root in roots) {
				RegisterFromHierarchy(root.transform);
			}
		}
		public void Deregister(IService service) {
			if (m_cache.ContainsKey(service.Key)) {
				m_cache.Remove(service.Key);
				service.OnServiceDeregistered();
			}
		}
		public void Deregister(Type type) {
			if (m_cache.ContainsKey(type)) {
				IService service = m_cache[type];
				m_cache.Remove(type);
				service.OnServiceDeregistered();
			}
		}
		public void Clear() {
			m_cache.Clear();
		}
		public T Get<T>() where T : IService {
			return (T)Get(typeof(T));
		}
		public IService Get(Type type) {
			if (m_cache.ContainsKey(type)) {
				return m_cache[type];
			} else {
				return null;
			}
		}
		public bool Get<T>(out T service) where T : IService {
			service = Get<T>();
			return service != null;
		}

		object IServiceProvider.GetService(Type serviceType) {
			return Get(serviceType);
		}
	}

}