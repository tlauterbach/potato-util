using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PotatoUtil {

	public class ServiceCache {

		private Dictionary<Type, IService> m_cache;

		public ServiceCache() {
			m_cache = new Dictionary<Type, IService>();
		}

		public void Register(IService service) {
			Type key = FindInterface(service.GetType());
			if (m_cache.ContainsKey(key)) {
				throw new Exception(string.Format(
					"Service of type `{0}' is already " +
					"registered to the ServiceCache.",
					key.Name
				));
			} else {
				m_cache.Add(key, service);
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
			Type key = FindInterface(service.GetType());
			if (m_cache.ContainsKey(key)) {
				m_cache.Remove(key);
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
			Type key = FindInterface(type);
			if (m_cache.ContainsKey(key)) {
				return m_cache[key];
			} else {
				return null;
			}
		}
		public bool Get<T>(out T service) where T : IService {
			service = Get<T>();
			return service != null;
		}


		private Type FindInterface(Type type) {
			Type[] interfaces = type.GetInterfaces();
			Type interfaceType = null;
			foreach (Type item in interfaces) {
				if (item.BaseType == typeof(IService)) {
					interfaceType = item;
					break;
				}
			}
			if (interfaceType == null) {
				throw new Exception(string.Format(
					"Type `{0}' does not inherit " +
					"from IService.",type.Name
				));
			}
			return interfaceType;
		} 

	}

}