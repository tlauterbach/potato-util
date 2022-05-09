using PotatoUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	private interface IMyService : IService {
	}
	private class MyService : IMyService {
		public Type Key { get { return typeof(IMyService); } }
		public void OnServiceDeregistered() { Debug.Log("MyService Deregistered"); }
		public void OnServiceRegistered() { Debug.Log("MyService Registered"); }
	}


	[SerializeField]
	private MinMax m_test;
	[SerializeField, Wrapped]
	private string m_testString;

	private ServiceCache m_serviceCache;

	private void Awake() {
		m_serviceCache = new ServiceCache();
		m_serviceCache.Register(new MyService());
		m_serviceCache.Deregister(typeof(IMyService));

		Randomizer randomizer = new Randomizer();
		Debug.Log(randomizer.ToString());

	}

}
