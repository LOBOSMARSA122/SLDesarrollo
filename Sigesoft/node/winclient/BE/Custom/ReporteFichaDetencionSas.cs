using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteFichaDetencionSas
    {
        public string ServiceId { get; set; }
        public string ServiceComponentId { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int TipoDocumentoId { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string EmpresaCliente { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string EmpresaEmpleadora { get; set; }
        public string Puesto { get; set; }
        public int GeneroId { get; set; }
        public string Genero { get; set; }
        public string LugarNacimiento { get; set; }
        public string LugarProcedencia { get; set; }
        public string UsuarioGraba { get; set; }
        public string Cmp { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] FirmaUsuarioGraba { get; set; }
        public byte[] FirmaMedicina { get; set; }

        public string TipoLicencia { get; set; }
        public string NroLicencia { get; set; }
        public string TrabajaNoche { get; set; }
        public string NroDiasTrabajoDescanso { get; set; }
        public string Apnea { get; set; }
        public string ApneaSi { get; set; }
        public string HipertensionArterial { get; set; }
        public string HipertensionArterialSi { get; set; }
        public string ChoqueVehiculo { get; set; }
        public string ChoqueVehiculoSomnolencia { get; set; }
        public string Ronca { get; set; }
        public string RoncaSi { get; set; }
        public string PausasSuenio { get; set; }
        public string PausasSuenioSi { get; set; }
        public string Fatiga { get; set; }
        public string FatigaSi { get; set; }


        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }
        public string CircunferenciaCuello { get; set; }
        public string So2 { get; set; }
        public string Pa1 { get; set; }
        public string Pa2 { get; set; }

        public string ClasificacionMallampati { get; set; }

        public string Aptitud { get; set; }
        public string VigenciaDesde { get; set; }
        public string VigenciaHasta { get; set; }
        public string ConclusionEvaluacion { get; set; }
        
    }
}
