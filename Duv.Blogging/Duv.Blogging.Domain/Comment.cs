//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Duv.Blogging.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment : Entry
    {
    
        public virtual Post Post { get; set; }
    }
}
