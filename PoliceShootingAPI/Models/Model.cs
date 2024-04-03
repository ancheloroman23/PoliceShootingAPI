
public class Model
{
    public class ShootingExchange
    {
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public string Lugar { get; set; }
        public Coordinates Coordenadas { get; set; }
        public List<Participant> Participantes { get; set; }
    }

    public class Coordinates
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }

    public class Participant
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
    }
}

