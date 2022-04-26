using System;

namespace PotatoUtil {

	public interface IService {
		Type Key { get; }
		void OnServiceRegistered();
		void OnServiceDeregistered();
	}

}