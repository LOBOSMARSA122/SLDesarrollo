//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/01 - 16:28:21
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class serviceorderDto
    {
        [DataMember()]
        public String v_ServiceOrderId { get; set; }

        [DataMember()]
        public String v_CustomServiceOrderId { get; set; }

        [DataMember()]
        public String v_Description { get; set; }

        [DataMember()]
        public String v_Comentary { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NumberOfWorker { get; set; }

        [DataMember()]
        public Nullable<Single> r_TotalCost { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_DeliveryDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceOrderStatusId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_LineaCreditoId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MostrarPrecio { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsProtocoloEspecial { get; set; }

        [DataMember()]
        public List<serviceorderdetailDto> serviceorderdetail { get; set; }

        public serviceorderDto()
        {
        }

        public serviceorderDto(String v_ServiceOrderId, String v_CustomServiceOrderId, String v_Description, String v_Comentary, Nullable<Int32> i_NumberOfWorker, Nullable<Single> r_TotalCost, Nullable<DateTime> d_DeliveryDate, Nullable<Int32> i_ServiceOrderStatusId, Nullable<Int32> i_LineaCreditoId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_MostrarPrecio, Nullable<Int32> i_EsProtocoloEspecial, List<serviceorderdetailDto> serviceorderdetail)
        {
			this.v_ServiceOrderId = v_ServiceOrderId;
			this.v_CustomServiceOrderId = v_CustomServiceOrderId;
			this.v_Description = v_Description;
			this.v_Comentary = v_Comentary;
			this.i_NumberOfWorker = i_NumberOfWorker;
			this.r_TotalCost = r_TotalCost;
			this.d_DeliveryDate = d_DeliveryDate;
			this.i_ServiceOrderStatusId = i_ServiceOrderStatusId;
			this.i_LineaCreditoId = i_LineaCreditoId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.i_MostrarPrecio = i_MostrarPrecio;
			this.i_EsProtocoloEspecial = i_EsProtocoloEspecial;
			this.serviceorderdetail = serviceorderdetail;
        }
    }
}
