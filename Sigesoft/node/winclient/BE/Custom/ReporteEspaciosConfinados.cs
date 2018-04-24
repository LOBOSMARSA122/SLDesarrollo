using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteEspaciosConfinados
    {
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ServiceComponentId { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string Nombres { get; set; }
        public string ProtocoloNombre { get; set; }
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
        public byte[] b_Logo { get; set; }

        public string AntecedentesImportancia { get; set; }
        public string Peso { get; set; }
        public string Pa1 { get; set; }
        public string Pa2 { get; set; }
        public string Talla { get; set; }
        public string So2 { get; set; }
        public string Imc { get; set; }
        public string Fc { get; set; }
        public string PerimetroCinturaCadera1 { get; set; }
        public string PerimetroCinturaCadera2 { get; set; }
        public string PerimetroToraxico { get; set; }
        public string Icc { get; set; }

        public string EvaCardioClinica { get; set; }
        public string EvaCardioClinicaAnomalia { get; set; }
        public string EvaCardioEkg { get; set; }
        public string EvaCardioEkgAnomalia { get; set; }

        public string EvaPulmonarClinica { get; set; }
        public string EvaPulmonarClinicaAnomalia { get; set; }
        public string EvaPulmonarEspirometria { get; set; }
        public string EvaPulmonarEspirometriaAnomalia { get; set; }
        public string EvaPulmonarRx { get; set; }
        public string EvaPulmonarRxAnomalia { get; set; }

        public string EvaNeurologicaClinica { get; set; }
        public string EvaNeurologicaClinicaAnomalia { get; set; }

        public string ResultadoClaustrophobia { get; set; }

        public string ConclusionesRecomendaciones { get; set; }

        public string Aptitud { get; set; }
        public string VigenciaDesde { get; set; }
        public string VigenciaHasta { get; set; }
    }
}
