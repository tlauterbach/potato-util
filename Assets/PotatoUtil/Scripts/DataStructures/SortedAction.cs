using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public class SortedAction {

		private class PrioritizedAction {
			public Action action { get; private set; }
			public int Priority { get; private set; }

			public PrioritizedAction(Action action, int priority) {
				this.action = action;
				Priority = priority;
			}
		}

		private List<PrioritizedAction> m_actions;
		private List<PrioritizedAction> m_toRemove;
		private List<PrioritizedAction> m_toAdd;

		private bool m_isInvoking = false;

		public SortedAction() {
			m_actions = new List<PrioritizedAction>();
			m_toAdd = new List<PrioritizedAction>();
			m_toRemove = new List<PrioritizedAction>();
		}

		public void Register(Action handler, int priority) {
			if (handler == null) {
				throw new ArgumentNullException(nameof(handler));
			}
			DoAdd(new PrioritizedAction(handler, priority));
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
			while (m_toAdd.Count > 0) {
				m_actions.Add(m_toAdd[m_toAdd.Count - 1]);
				m_toAdd.RemoveAt(m_toAdd.Count - 1);
			}
			while (m_toRemove.Count > 0) {
				m_actions.Remove(m_toRemove[m_toRemove.Count - 1]);
				m_toRemove.RemoveAt(m_toRemove.Count - 1);
			}
			m_toAdd.Clear();
			m_toRemove.Clear();
			m_actions.Sort(PrioritySorter);
		}

		public void Clear() {
			if (m_isInvoking) {
				m_toRemove.AddRange(m_actions);
			} else {
				m_actions.Clear();
				m_toRemove.Clear();
				m_toAdd.Clear();
			}
		}

		private static int PrioritySorter(PrioritizedAction x, PrioritizedAction y) {
			return Math.Min(1, Math.Max(-1, y.Priority - x.Priority));
		}

		private void DoRemove(PrioritizedAction action) {
			if (m_isInvoking) {
				m_toRemove.Add(action);
			} else {
				m_actions.Remove(action);
			}
		}
		private void DoAdd(PrioritizedAction action) {
			if (m_isInvoking) {
				m_toAdd.Add(action);
			} else {
				m_actions.Add(action);
				m_actions.Sort(PrioritySorter);
			}
		}

	}


	public class SortedAction<T> {

		private class PrioritizedAction {
			public Action<T> action { get; private set; }
			public int Priority { get; private set; }

			public PrioritizedAction(Action<T> action, int priority) {
				this.action = action;
				Priority = priority;
			}
		}

		private List<PrioritizedAction> m_actions;
		private List<PrioritizedAction> m_toRemove;
		private List<PrioritizedAction> m_toAdd;

		private bool m_isInvoking = false;

		public SortedAction() {
			m_actions = new List<PrioritizedAction>();
		}

		public void Register(Action<T> handler, int priority) {
			if (handler == null) {
				throw new ArgumentNullException(nameof(handler));
			}
			DoAdd(new PrioritizedAction(handler, priority));
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
			while (m_toAdd.Count > 0) {
				m_actions.Add(m_toAdd[m_toAdd.Count - 1]);
				m_toAdd.RemoveAt(m_toAdd.Count - 1);
			}
			while (m_toRemove.Count > 0) {
				m_actions.Remove(m_toRemove[m_toRemove.Count - 1]);
				m_toRemove.RemoveAt(m_toRemove.Count - 1);
			}
			m_toAdd.Clear();
			m_toRemove.Clear();
			m_actions.Sort(PrioritySorter);
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

		private void DoRemove(PrioritizedAction action) {
			if (m_isInvoking) {
				m_toRemove.Add(action);
			} else {
				m_actions.Remove(action);
			}
		}

		private void DoAdd(PrioritizedAction action) {
			if (m_isInvoking) {
				m_toAdd.Add(action);
			} else {
				m_actions.Add(action);
				m_actions.Sort(PrioritySorter);
			}
		}
	}

}