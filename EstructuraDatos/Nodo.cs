using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	class Nodo<T>
	{
		public T Dato;
		public Nodo<T> Siguiente;

		public Nodo(T value)
		{
			this.Dato = value;
			Siguiente = null;
		}
	}
}
