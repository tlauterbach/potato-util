using System;
using System.Collections.Generic;

namespace PotatoUtil {


	public interface IMessage {

	}

	public sealed class Message : IMessage {
		private string m_name;
		public Message(string name) {
			m_name = name;
		}
	}
	public sealed class Message<T> : IMessage {
		private string m_name;
		public Message(string name) {
			m_name = name;
		}
	}

	public class Messenger {

		private Dictionary<IMessage, IEntry> m_registry;

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
				return true;
			}
			public bool TryGetAs<U>(out Entry<U> entry) {
				entry = this as Entry<U>;
				return (entry != null);
			}
		}

		#endregion

		public Messenger() {
			m_registry = new Dictionary<IMessage, IEntry>();
		}

		public void Register(Message message, Action handler, int priority = 0) {
			GetEntry(message).Register(handler, priority);
		}
		public void Register<T>(Message<T> message, Action<T> handler, int priority = 0) {
			GetEntry(message).Register(handler, priority);
		}

		public void Deregister(Message message, Action handler) {
			GetEntry(message).Deregister(handler);
		}
		public void Deregister<T>(Message<T> message, Action<T> handler) {
			GetEntry(message).Deregister(handler);
		}

		public void DeregisterAll(Message message) {
			GetEntry(message).Clear();
		}
		public void DeregisterAll<T>(Message<T> message) {
			GetEntry(message).Clear();
		}

		public void Broadcast(Message message) {
			GetEntry(message).Inovke();
		}
		public void Broadcast<T>(Message<T> message, T arg) {
			GetEntry(message).Invoke(arg);
		}

		private Entry GetEntry(Message message) {
			Entry entry;
			if (m_registry.ContainsKey(message)) {
				if (!m_registry[message].TryGetAs(out entry)) {
					throw new Exception("Mismatched Message and/or Handler " +
						"type. Should have an argument! (Message<T>)");
				}
			} else {
				entry = new Entry();
				m_registry.Add(message, entry);
			}
			return entry;
		}
		private Entry<T> GetEntry<T>(Message<T> message) {
			Entry<T> entry;
			if (m_registry.ContainsKey(message)) {
				if (!m_registry[message].TryGetAs(out entry)) {
					throw new Exception("Mismatched Message and/or Handler type.");
				}
			} else {
				entry = new Entry<T>();
				m_registry.Add(message, entry);
			}
			return entry;
		}

	}

}