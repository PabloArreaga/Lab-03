using Lab03_PabloArreaga_1331818.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ACPA_Lab02.Controllers
{
	public class FarmaciaController : Controller
	{

		// GET: Farmacia
		public ActionResult Subir()
		{
			return View(new List<CustomerModel>());
		}
		[HttpPost]
		public ActionResult Subir(HttpPostedFileBase postedFile)
		{
			List<CustomerModel> customers = new List<CustomerModel>();
			string filePath = string.Empty;
			if (postedFile != null)
			{
				string path = Server.MapPath("~/Uploads/");
				if (!Directory.Exists(path))
				{

					Directory.CreateDirectory(path);
				}
				filePath = path + Path.GetFileName(postedFile.FileName);
				string extension = Path.GetExtension(postedFile.FileName);
				postedFile.SaveAs(filePath);

				string csvData = System.IO.File.ReadAllText(filePath);
				string[] separadas = null;
				string dato_dos = null;
				string dato_tres = null;
				string dato_cuatro = null;
				string dato_cinco = null;
				string dato_seis = null;
				foreach (string row in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(row))
					{
						if (row.Length != 1 & row.Length != 0)
						{
							if (row.Contains('"'))
							{
								string dato_uno;
								string linea = row;
								int pos = linea.IndexOf(",");
								dato_uno = linea.Substring(0, pos);
								linea = linea.Substring(pos + 1);
								if (linea[0] == '"')
								{
									pos = linea.IndexOf('"');
									linea = linea.Substring(pos + 1);
									pos = linea.IndexOf('"');
									dato_dos = linea.Substring(0, pos);
									linea = linea.Substring(pos + 1);
									pos = linea.IndexOf('"');
									if (pos == 1)
									{
										linea = linea.Substring(pos + 1);
										pos = linea.IndexOf('"');
										dato_tres = linea.Substring(0, pos);
										linea = linea.Substring(pos + 1);
										separadas = linea.Split(',');
										dato_cuatro = separadas[1];
										dato_cinco = separadas[2];
										dato_seis = separadas[3];
									}
									else
									{
										separadas = linea.Split(',');
										dato_cuatro = separadas[0];
										dato_cinco = separadas[1];
										dato_seis = separadas[2];
									}
								}
								else
								{
									pos = linea.IndexOf(",");
									dato_dos = linea.Substring(0, pos);
									linea = linea.Substring(pos + 1);
									if (linea[0] == '"')
									{
										pos = linea.IndexOf('"');
										linea = linea.Substring(pos + 1);
										pos = linea.IndexOf('"');
										dato_tres = linea.Substring(0, pos);
										linea = linea.Substring(pos + 1);
										pos = linea.IndexOf('"');
										if (pos == 1)
										{
											linea = linea.Substring(pos + 1);
											pos = linea.IndexOf('"');
											dato_cuatro = linea.Substring(0, pos);
											linea = linea.Substring(pos + 1);
											separadas = linea.Split(',');
											dato_cinco = separadas[1];
											dato_seis = separadas[2];
										}
										else
										{
											separadas = linea.Split(',');
											dato_cuatro = separadas[1];
											dato_cinco = separadas[2];
											dato_seis = separadas[3];
										}
									}
									else
									{
										pos = linea.IndexOf(",");
										dato_tres = linea.Substring(0, pos);
										linea = linea.Substring(pos + 1);
										pos = linea.IndexOf('"');
										if (pos == 0)
										{
											linea = linea.Substring(pos + 1);
											pos = linea.IndexOf('"');
											dato_cuatro = linea.Substring(0, pos);
											linea = linea.Substring(pos + 1);
											separadas = linea.Split(',');
											dato_cinco = separadas[1];
											dato_seis = separadas[2];
										}
										else
										{

										}
									}
								}
								customers.Add(new CustomerModel
								{
									id = dato_uno,
									nombre = dato_dos,
									descripcion = dato_tres,
									productora = dato_cuatro,
									precio = dato_cinco,
									existencia = dato_seis
								});
							}
							else
							{
								separadas = row.Split(',');
								customers.Add(new CustomerModel
								{
									id = separadas[0],
									nombre = separadas[1],
									descripcion = separadas[2],
									productora = separadas[3],
									precio = separadas[4],
									existencia = separadas[5]
								});
							}
						}
						
					}
				}
			}
			return View(customers);
		}
	}
}