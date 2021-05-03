using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public class SortedAction {

		private List<PrioritizedAction> m_actions;

		private class PrioritizedAction {
			public Action action { get; private set; }
			public int Priority { get; private set; }

			public PrioritizedAction(Action action, int priority) {
				this.action = action;
				Priority = priority;
			}
		}

		public SortedAction() {
			m_actions = new List<PrioritizedAction>();
		}

		public void Register(Action handler, int priority) {
			if (handler == null) {
				throw new ArgumentNullException(nameof(handler));
			}
			m_actions.Add(new PrioritizedAction(handler, priority));
			m_actions.Sort(PrioritySorter);
		}
		public void Deregister(Action handler) {
			if (handler == null) {
				throw new ArgumentNullException(nameof(handler));
			}
			PrioritizedAction toRemove = m_actions.Find((PrioritizedAction action) => {
				return action.action == handler;
			});
			if (toRemove != null) {
				DoRemove(toRemove);
			}
		}

		public void Invoke() {
			m_isInvoking = true;
			foreach (PrioritizedAction action in m_actions) {
				action.action();
			}
			m_isInvoking = false;
			while (m_toRemove.Count > 0) {
				m_actions.Remove(m_toRemove[m_toRemove.Count - 1]);
				m_toRemove.RemoveAt(m_toRemove.Count - 1);
			}
			m_toRemove.Clear();
		}

		public void Clear() {
			if (m_isInvoking) {
				m_toRemove.AddRange(m_actions);
			} else {
				m_actions.Clear();
				m_toRemove.Clear();
			}
		}

		private static int PrioritySorter(PrioritizedAction x, PrioritizedAction y) {
			return Math.Min(1, Math.Max(-1, y.Priority - x.Priority));
		}

		private bool m_isInvoking = false;
		private List<PrioritizedAction> m_toRemove = new List<PrioritizedAction>();
		private void DoRemove(PrioritizedAction action) {
			if (m_isInvoking) {
				m_toRemove.Add(action);
			} else {
				m_actions.Remove(action);
			}
		}

	}


	public class SortedAction<T> {

		private List<PrioritizedAction> m_actions;

		private class PrioritizedAction {
			public Action<T> action { get; private set; }
			public int Priority { get; private set; }

			public PrioritizedAction(Action<T> action, int priority) {
				this.action = action;
				Priority = priority;
			}
		}

		public SortedAction() {
			m_actions = new List<PrioritizedAction>();
		}

		public void Register(Action<T> handler, int priority) {
			if (handler == null) {
				throw new ArgumentNullException(nameof(handler));
			}
			m_actions.Add(new PrioritizedAction(handler, priority));
			m_actions.Sort(PrioritySorter);
		}
		public void Deregister(Action<T> handler) {
			if (handler == null) {
				throw new ArgumentNullException(nameof(handler));
			}
			PrioritizedAction toRemove = m_actions.Find((PrioritizedAction action) => {
				return action.action == handler;
			});
			if (toRemove != null) {
				DoRemove(toRemove);
			}
		}

		public void Invoke(T arg) {
			m_isInvoking = true;
			foreach (PrioritizedAction action in m_actions) {
				action.action(arg);
			}
			m_isInvoking = false;
			while (m_toRemove.Count > 0) {
				m_actions.Remove(m_toRemove[m_toRemove.Count - 1]);
				m_toRemove.RemoveAt(m_toRemove.Count - 1);
			}
			m_toRemove.Clear();
		}

		public void Clear() {
			if (m_isInvoking) {
				m_toRemove.AddRange(m_actions);
			} else {
				m_actions.Clear();
				m_toRemove.Clear();
			}
		}

		private static int PrioritySorter(PrioritizedAction x, PrioritizedAction y) {
			return Math.Min(1, Math.Max(-1, y.Priority - x.Priority));
		}

		private bool m_isInvoking = false;
		private List<PrioritizedAction> m_toRemove = new List<PrioritizedAction>();
		private void DoRemove(PrioritizedAction action) {
			if (m_isInvoking) {
				m_toRemove.Add(action);
			} else {
				m_actions.Remove(action);
			}
		}

	}

}