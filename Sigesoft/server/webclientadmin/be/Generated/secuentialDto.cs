//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/03/25 - 12:08:43
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract()]
    public partial class secuentialDto
    {
        [DataMember()]
        public Int32 i_NodeId { get; set; }

        [DataMember()]
        public Int32 i_TableId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SecuentialId { get; set; }

        public secuentialDto()
        {
        }

        public secuentialDto(Int32 i_NodeId, Int32 i_TableId, Nullable<Int32> i_SecuentialId)
        {
			this.i_NodeId = i_NodeId;
			this.i_TableId = i_TableId;
			this.i_SecuentialId = i_SecuentialId;
        }
    }
}
