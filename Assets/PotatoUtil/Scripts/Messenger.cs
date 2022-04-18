using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public struct Message : IEquatable<Message> {
		private const string HASH_PREFIX = "Message";
		private string m_name;
		private FNVHash m_hash;

		public Message(string name) {
			m_name = name;
			m_hash = new FNVHash(string.Concat(HASH_PREFIX,name));
		}
		public static bool operator ==(Message lhs, Message rhs) {
			return lhs.Equals(rhs);
		}
		public static bool operator !=(Message lhs, Message rhs) {
			return !lhs.Equals(rhs);
		}
		public override bool Equals(object obj) {
			if (obj is Message message) {
				return Equals(message);
			} else {
				return false;
			}
		}
		public bool Equals(Message other) {
			return m_hash == other.m_hash;
		}
		public override int GetHashCode() {
			return m_hash;
		}
		public override string ToString() {
			return m_name;
		}
	}

	public class Messenger {

		private Dictionary<Message, IEntry> m_registry;

		#region Classes

		private interface IEntry {
			bool TryGetAs(out Entry entry);
			bool TryGetAs<T>(out Entry<T> entry);
		}
		private class Entry : IEntry {
			private SortedAction m_actions;
			public Entry() {
				m_actions = new SortedAction();
			}
			public void Register(Action handler, int priority) {
				m_actions.Register(handler, priority);
			}
			public void Deregister(Action handler) {
				m_actions.Deregister(handler);
			}
			public void Inovke() {
				m_actions.Invoke();
			}
			public void Clear() {
				m_actions.Clear();
			}
			public bool TryGetAs(out Entry entry) {
				entry = this;
				return true;
			}
			public bool TryGetAs<T>(out Entry<T> entry) {
				entry = null;
				return false;
			}
		}
		private class Entry<T> : IEntry {
			private SortedAction<T> m_actions;
			public Entry() {
				m_actions = new SortedAction<T>();
			}
			public void Register(Action<T> handler, int priority) {
				m_actions.Register(handler, priority);
			}
			public void Deregister(Action<T> handler) {
				m_actions.Deregister(handler);
			}
			public void Clear() {
				m_actions.Clear();
			}
			public void Invoke(T arg) {
				m_actions.Invoke(arg);
			}
			public bool TryGetAs(out Entry entry) {
				entry = null;
				return false;
			}
			public bool TryGetAs<U>(out Entry<U> entry) {
				entry = this as Entry<U>;
				return (entry != null);
			}
		}

		#endregion

		public Messenger() {
			m_registry = new Dictionary<Message, IEntry>();
		}

		public void Register(Message message, Action handler, int priority = 0) {
			GetEntry(message).Register(handler, priority);
		}
		public void Register<T>(Message message, Action<T> handler, int priority = 0) {
			GetEntry<T>(message).Register(handler, priority);
		}

		public void Deregister(Message message, Action handler) {
			GetEntry(message).Deregister(handler);
		}
		public void Deregister<T>(Message message, Action<T> handler) {
			GetEntry<T>(message).Deregister(handler);
		}

		public void DeregisterAll(Message message) {
			GetEntry(message).Clear();
		}
		public void DeregisterAll<T>(Message message) {
			GetEntry(message).Clear();
		}

		public void Broadcast(Message message) {
			GetEntry(message).Inovke();
		}
		public void Broadcast<T>(Message message, T arg) {
			GetEntry<T>(message).Invoke(arg);
		}

		private Entry<T> GetEntry<T>(Message message) {
			Entry<T> entry;
			if (m_registry.ContainsKey(message)) {
				if (!m_registry[message].TryGetAs(out entry)) {
					throw new Exception("Mismatched Message and/or Handler " +
						"type. Should have an argument!");
				}
			} else {
				entry = new Entry<T>();
				m_registry.Add(message, entry);
			}
			return entry;
		}

		private Entry GetEntry(Message message) {
			Entry entry;
			if (m_registry.ContainsKey(message)) {
				if (!m_registry[message].TryGetAs(out entry)) {
					throw new Exception("Mismatched Message and/or Handler " +
						"type. Should have no arguments!");
				}
			} else {
				entry = new Entry();
				m_registry.Add(message, entry);
			}
			return entry;
		}

	}

}