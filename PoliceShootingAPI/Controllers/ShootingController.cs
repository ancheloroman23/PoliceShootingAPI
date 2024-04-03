using Microsoft.AspNetCore.Mvc;
using static Model;
using System.Xml;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class ShootingController : ControllerBase
{    
    
    private readonly string filePath = "Data\\shootingData.json";



    [HttpPost("Registrar")]
    public IActionResult Registrar([FromBody] ShootingExchange exchange)
    {
        var exchanges = ObtenerExchanges();
        exchanges.Add(exchange);
        GuardarExchanges(exchanges);
        return Ok("Intercambio de disparos registrado con éxito.");
    }

    [HttpGet("Listado")]
    public IActionResult Listado(DateTime fechaInicio, DateTime fechaFin)
    {
        var exchanges = ObtenerExchanges()
            .Where(e => e.FechaIni >= fechaInicio && e.FechaFin <= fechaFin)
            .ToList();
        return Ok(exchanges);
    }

    [HttpGet("ConsultaPersona")]
    public IActionResult ConsultaPersona(string cedula)
    {
        var exchanges = ObtenerExchanges();
        var participaciones = exchanges
            .Where(e => e.Participantes.Any(p => p.Cedula == cedula))
            .Select(e => new
            {
                FechaIni = e.FechaIni,
                FechaFin = e.FechaFin,
                Rol = e.Participantes.Select(p => p.Rol).First(),
                Nombre = e.Participantes.Select(p => p.Nombre).First(),
                Estado = e.Participantes.Select(p => p.Estado).First(),                               
            })
            .ToList();
        return Ok(participaciones);
    }

    private List<ShootingExchange> ObtenerExchanges()
    {
        if (System.IO.File.Exists(filePath))
        {
            var json = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ShootingExchange>>(json);
        }
        return new List<ShootingExchange>();
    }

    private void GuardarExchanges(List<ShootingExchange> exchanges)
    {
        var json = JsonConvert.SerializeObject(exchanges, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText(filePath, json);
    }
}


