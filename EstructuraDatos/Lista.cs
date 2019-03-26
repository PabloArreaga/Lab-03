using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	class Lista<T>
	{
		public Nodo<T> Cabeza;
		public Nodo<T> Cola;

		public Lista()
		{
			Cabeza = null;
			Cola = null;
		}

		void Insertar_Fin(Nodo<T> nuevo)
		{
			//cuando esta vacia
			if (EstaVacia())
			{
				Cabeza = nuevo;
				Cola = nuevo;
			}
			//cuando hay no lo esta
			else
			{
				Cola.Siguiente = nuevo;
				Cola = nuevo;
			}
		}

		public void Insertar_Inicio(Nodo<T> nuevo)
		{
			//cuando esta vacia
			if (EstaVacia())
			{
				Cabeza = nuevo;
				Cola = nuevo;
			}
			//cuando no lo esta
			else
			{
				nuevo.Siguiente = Cabeza;
				Cabeza = nuevo;
			}
		}

		void Eliminar_Final()
		{
			//si no hay nada
			if (EstaVacia())
			{
				return;
			}

			Nodo<T> actual = Cabeza;

			//Cuando solo hay uno
			if (actual.Siguiente == null)
			{
				Cabeza = null;
				Cola = null;
				return;
			}

			//cuando hay mas de uno
			while (actual.Siguiente != Cola)
			{
				actual = actual.Siguiente;
			}

			actual.Siguiente = null;
			Cola = actual;
		}

		public void Eliminar_Inicial()
		{

		}

		public bool Eliminar_elemento(int value)
		{
			throw new NotImplementedException();
		}

		public bool EstaVacia()
		{
			return (Cabeza == null) && (Cola == null);
		}

		public T Mostrar()
		{
			Nodo<T> Aux = Cabeza;
			return Aux.Dato;
		}

		public IEnumerator<T> GetEnumerator()
		{
			Nodo<T> current = Cabeza;

			while (current != null)
			{
				yield return current.Dato;
				current = current.Siguiente;
			}
		}
	}
}
