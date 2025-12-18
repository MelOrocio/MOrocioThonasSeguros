using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MOrocioThonasSeguros.Controllers
{
    public class ConsultarCPController : Controller
    {
        // GET: ConsultarCP
        public ActionResult ConsultarCP()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ConsultarCP(string CP)
        {
            try
            {
                if (string.IsNullOrEmpty(CP))
                {
                    return Json(new { success = false, message = "Código postal requerido" });
                }

                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://thona-api-desarrollo.azurewebsites.net/api/CatalogoCP?CP={CP}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string xmlContent = await response.Content.ReadAsStringAsync();

                        // Parsear el XML
                        XDocument doc = XDocument.Parse(xmlContent);

                        ML.CP cpData = new ML.CP
                        {
                            DescEstado = doc.Descendants("DESCESTADO").FirstOrDefault()?.Value,
                            CodEstado = int.Parse(doc.Descendants("CODESTADO").FirstOrDefault()?.Value ?? "0"),
                            CodMunicipio = int.Parse(doc.Descendants("CODMUNICIPIO").FirstOrDefault()?.Value ?? "0"),
                            DescMunicipio = doc.Descendants("DESCMUNICIPIO").FirstOrDefault()?.Value,
                            results = doc.Descendants("COLONIA").Select(c => new ML.DatosCP
                                {
                                    ID = int.Parse(c.Attribute("ID")?.Value ?? "0"),
                                    DescripcionColonia = c.Element("DESCRIPCION_COLONIA")?.Value,
                                    CodigoColonia = c.Element("CODIGO_COLONIA")?.Value
                                }).ToList()
                        };

                        return Json(new { success = true, data = cpData });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error al consultar la API" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}