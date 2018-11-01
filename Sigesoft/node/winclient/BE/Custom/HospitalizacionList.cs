using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class HospitalizacionList
    {
        public string v_HopitalizacionId { get; set; }
        public string v_PersonId { get; set; }
        public string v_Paciente { get; set; }
        public DateTime? d_FechaIngreso { get; set; }
        public DateTime? d_FechaAlta { get; set; }
        public int i_IsDeleted { get; set; }
        public string v_Comentario { get; set; }

        public List<HospitalizacionServiceList> Servicios { get; set; }
        public List<HospitalizacionHabitacionList> Habitaciones { get; set; }
    }

   public class HospitalizacionHabitacionList
   {
       public string v_HospitalizacionHabitacionId { get; set; }
       public string v_HopitalizacionId { get; set; }
       public int i_HabitacionId { get; set; }
       public string NroHabitacion { get; set; }
       public DateTime? d_StartDate { get; set; }
       public DateTime? d_EndDate { get; set; }
       public decimal? d_Precio { get; set; }
       public decimal Total { get; set; }
   }

   public class HospitalizacionServiceList
   {
       public string v_HospitalizacionServiceId { get; set; }
       public string v_HopitalizacionId { get; set; }
       public string v_ServiceId { get; set; }
       public DateTime? d_ServiceDate { get; set; }
       public string v_ProtocolName { get; set; }
       public string v_ProtocolId { get; set; }
       public List<TicketList> Tickets { get; set; }

       public List<ComponentesHospitalizacion> Componentes { get; set; }
   }

   public class ComponentesHospitalizacion
   {

       public string ServiceComponentId { get; set; }
       public string Categoria { get; set; }
       public string Componente { get; set; }
       public float Precio { get; set; }
       public string MedicoTratante { get; set; }
       public DateTime Ingreso { get; set; }
   }
}
