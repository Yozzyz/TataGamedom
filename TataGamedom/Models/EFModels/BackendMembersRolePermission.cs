namespace TataGamedom.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BackendMembersRolePermission
    {
        public int Id { get; set; }

        public int BackendMembersRoleId { get; set; }

        public int BackendMemberPermissionId { get; set; }

        public virtual BackendMembersPermissionsCode BackendMembersPermissionsCode { get; set; }

        public virtual BackendMembersRolesCode BackendMembersRolesCode { get; set; }
    }
}
