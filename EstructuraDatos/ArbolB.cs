using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDatos
{
	class ArbolB
	{

		public class ArbolBe
		{
			int MaxKeys = 3;
			public NodoAB Raiz { get; set; }
			public ArbolBe()
			{
				Raiz = null;
			}
			public void Insertar(int value)
			{
				if (Raiz == null)
				{
					Raiz = new NodoAB(value);
					return;
				}
				NodoAB Actual = Raiz;
				NodoAB Padre = null;
				while (Actual != null)
				{
					if (Actual.Keys.Count == MaxKeys)
					{
						if (Padre == null)
						{
							int k = Actual.Pop(1);
							NodoAB nuevaRaiz = new NodoAB(k);
							NodoAB[] newNodos = Actual.Split();
							nuevaRaiz.InsertEdge(newNodos[0]);
							nuevaRaiz.InsertEdge(newNodos[1]);
							Raiz = nuevaRaiz;
							Actual = nuevaRaiz;
						}
						else
						{
							int? k = Actual.Pop(1);
							if (k != null)
							{
								Padre.Push(k.Value);
							}
							NodoAB[] nNodos = Actual.Split();
							int pos1 = Padre.FindEdgePosition(nNodos[1].Keys[0]);
							Padre.InsertEdge(nNodos[1]);

							int posActual = Padre.FindEdgePosition(value);
							Actual = Padre.GetEdge(posActual);

						}
					}
					Padre = Actual;
					Actual = Actual.Traverse(value);
					if (Actual == null)
					{
						Padre.Push(value);
					}
				}
			}
			public NodoAB Find(int k)
			{
				NodoAB curr = Raiz;

				while (curr != null)
				{
					if (curr.HasKey(k) >= 0)
					{
						return curr;
					}
					else
					{
						int p = curr.FindEdgePosition(k);
						curr = curr.GetEdge(p);
					}
				}

				return null;
			}
			public void Remove(int k)
			{

				NodoAB curr = Raiz;
				NodoAB parent = null;
				while (curr != null)
				{

					if (curr.Keys.Count == 1)
					{
						if (curr != Raiz)
						{
							int cK = curr.Keys[0];
							int edgePos = parent.FindEdgePosition(cK);

							bool? takeRight = null;
							NodoAB sibling = null;

							if (edgePos > -1)
							{
								if (edgePos < 3)
								{
									sibling = parent.GetEdge(edgePos + 1);
									if (sibling.Keys.Count > 1)
									{
										takeRight = true;
									}
								}

								if (takeRight == null)
								{
									if (edgePos > 0)
									{
										sibling = parent.GetEdge(edgePos - 1);
										if (sibling.Keys.Count > 1)
										{
											takeRight = false;
										}
									}
								}

								if (takeRight != null)
								{
									int? pK = 0;
									int? sK = 0;

									if (takeRight.Value)
									{
										pK = parent.Pop(edgePos);
										sK = sibling.Pop(0);

										if (sibling.Edges.Count > 0)
										{
											NodoAB edge = sibling.RemoveEdge(0);
											curr.InsertEdge(edge);
										}
									}
									else
									{
										pK = parent.Pop(edgePos);
										sK = sibling.Pop(sibling.Keys.Count - 1);

										if (sibling.Edges.Count > 0)
										{
											NodoAB edge = sibling.RemoveEdge(sibling.Edges.Count - 1);
											curr.InsertEdge(edge);
										}
									}

									parent.Push(sK.Value);
									curr.Push(pK.Value);
								}
								else
								{
									int? pK = null;
									if (parent.Edges.Count >= 2)
									{
										if (edgePos == 0)
										{
											pK = parent.Pop(0);
										}
										else if (edgePos == parent.Edges.Count)
										{
											pK = parent.Pop(parent.Keys.Count - 1);
										}
										else
										{
											pK = parent.Pop(1);
										}

										if (pK != null)
										{
											curr.Push(pK.Value);
											NodoAB sib = null;
											if (edgePos != parent.Edges.Count)
											{
												sib = parent.RemoveEdge(edgePos + 1);
											}
											else
											{
												sib = parent.RemoveEdge(parent.Edges.Count - 1);
											}

											curr.Fuse(sib);
										}
									}
									else
									{
										curr.Fuse(parent, sibling);
										Raiz = curr;
										parent = null;
									}
								}
							}
						}
					}

					int rmPos = -1;
					if ((rmPos = curr.HasKey(k)) >= 0)
					{
						if (curr.Edges.Count == 0)
						{
							if (curr.Keys.Count == 0)
							{
								parent.Edges.Remove(curr);
							}
							else
							{
								curr.Pop(rmPos);
							}
						}
						else
						{
							NodoAB successor = Min(curr.Edges[rmPos]);
							int sK = successor.Keys[0];
							if (successor.Keys.Count > 1)
							{
								successor.Pop(0);
							}
							else
							{
								if (successor.Edges.Count == 0)
								{
									NodoAB p = successor.Parent;
									p.RemoveEdge(successor);
								}
								else
								{
								}
							}
						}

						curr = null;
					}
					else
					{
						int p = curr.FindEdgePosition(k);
						parent = curr;
						curr = curr.GetEdge(p);
					}
				}

			}
			public NodoAB Min(NodoAB n = null)
			{
				if (n == null)
				{
					n = Raiz;
				}

				NodoAB curr = n;
				if (curr != null)
				{
					while (curr.Edges.Count > 0)
					{
						curr = curr.Edges[0];
					}
				}

				return curr;
			}
			public int[] Inorder(NodoAB n = null)
			{
				if (n == null)
				{
					n = Raiz;
				}

				List<int> items = new List<int>();
				Tuple<NodoAB, int> curr = new Tuple<NodoAB, int>(n, 0);
				Stack<Tuple<NodoAB, int>> stack = new Stack<Tuple<NodoAB, int>>();
				while (stack.Count > 0 || curr.Item1 != null)
				{
					//casos
					if (curr.Item1 != null)
					{
						stack.Push(curr);
						NodoAB leftChild = curr.Item1.GetEdge(curr.Item2);
						curr = new Tuple<NodoAB, int>(leftChild, 0);
					}
					else
					{
						curr = stack.Pop();
						NodoAB currNode = curr.Item1;

						if (curr.Item2 < currNode.Keys.Count)
						{
							items.Add(currNode.Keys[curr.Item2]);
							curr = new Tuple<NodoAB, int>(currNode, curr.Item2 + 1);
						}
						else
						{
							NodoAB rightChild = currNode.GetEdge(curr.Item2 + 1);

							curr = new Tuple<NodoAB, int>(rightChild, curr.Item2 + 1);
						}
					}
				}
				return items.ToArray();
			}

		}
	}

}
