using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class CertificadoCosapi
    {
        public byte[] LogoClinica { get; set; }
        public string Service_Id { get; set; }
        public string GrupoSanguineo { get; set; }
        public string NombreCompleto { get; set; }
        public string Dni { get; set; }
        public DateTime? d_BirthDate { get; set; }
        public int? Edad { get; set; }
        public int? Genero { get; set; }
        public int TipoExamen { get; set; }
        public string EmpresaCliente { get; set; }
        public string ProtocoloNombre { get; set; }
        public string PuestoTrabajo { get; set; }
        public byte[] FirmaAuditor { get; set; }
        public string NombreAuditor { get; set; }
        public  DateTime? FechaExamen { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public string NumeroCMP { get; set; }
        public string AlturaEstruturalApto { get; set; }
        public string AltitudGeograficoApto { get; set; }
        public string Restricciones { get; set; }
        public string Recomendacion { get; set; }
        public int? Aptitud { get; set; }
    }
}
