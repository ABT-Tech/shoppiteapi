using System;
using System.Collections.Generic;

#nullable disable

namespace Shoppite.Core.Model
{
    public partial class UserActivation
    {
        public int UserId { get; set; }
        public Guid ActivationCode { get; set; }
        public int? OrgId { get; set; }
    }
}
