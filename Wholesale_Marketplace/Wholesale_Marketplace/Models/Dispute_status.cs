//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wholesale_Marketplace.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dispute_status
    {
        public Dispute_status()
        {
            this.Dialog_dispute = new HashSet<Dialog_dispute>();
        }
    
        public int Dispute_statusID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Dialog_dispute> Dialog_dispute { get; set; }
    }
}
