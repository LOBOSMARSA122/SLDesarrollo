//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/01 - 16:28:36
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BE
{

    /// <summary>
    /// Assembler for <see cref="cie10"/> and <see cref="cie10Dto"/>.
    /// </summary>
    public static partial class cie10Assembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="cie10Dto"/> converted from <see cref="cie10"/>.</param>
        static partial void OnDTO(this cie10 entity, cie10Dto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="cie10"/> converted from <see cref="cie10Dto"/>.</param>
        static partial void OnEntity(this cie10Dto dto, cie10 entity);

        /// <summary>
        /// Converts this instance of <see cref="cie10Dto"/> to an instance of <see cref="cie10"/>.
        /// </summary>
        /// <param name="dto"><see cref="cie10Dto"/> to convert.</param>
        public static cie10 ToEntity(this cie10Dto dto)
        {
            if (dto == null) return null;

            var entity = new cie10();

            entity.v_CIE10Id = dto.v_CIE10Id;
            entity.v_CIE10Description1 = dto.v_CIE10Description1;
            entity.v_CIE10Description2 = dto.v_CIE10Description2;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="cie10"/> to an instance of <see cref="cie10Dto"/>.
        /// </summary>
        /// <param name="entity"><see cref="cie10"/> to convert.</param>
        public static cie10Dto ToDTO(this cie10 entity)
        {
            if (entity == null) return null;

            var dto = new cie10Dto();

            dto.v_CIE10Id = entity.v_CIE10Id;
            dto.v_CIE10Description1 = entity.v_CIE10Description1;
            dto.v_CIE10Description2 = entity.v_CIE10Description2;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="cie10Dto"/> to an instance of <see cref="cie10"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<cie10> ToEntities(this IEnumerable<cie10Dto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="cie10"/> to an instance of <see cref="cie10Dto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<cie10Dto> ToDTOs(this IEnumerable<cie10> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
