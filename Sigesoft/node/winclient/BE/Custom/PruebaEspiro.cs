using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class PruebaEspiro
    {
        public string Trabajador { get; set; }
        public string DNI { get; set; }
        public string Puesto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string IdServicio { get; set; }

        public string TipoProtocolo { get; set; }
        public string CVF { get; set; }
        public string VEF1 { get; set; }
        public string VEF1CVF { get; set; }
        public string FET { get; set; }

        public string FEV2575 { get; set; }
        public string PEF { get; set; }
        public string CVFDes { get; set; }
        public string VEF1Des { get; set; }
        public string VEF1CVFDes { get; set; }

        public string FETDes { get; set; }
        public string FEV2575Des { get; set; }
        public string PEFDes { get; set; }
        public Byte[] logo { get; set; }
        public Byte[] Firma { get; set; }
        public string EmpCliente { get; set; }
    }
}
