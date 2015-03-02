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
    
    public partial class Seller
    {
        public Seller()
        {
            this.Dialog_dispute = new HashSet<Dialog_dispute>();
            this.Orders = new HashSet<Order>();
        }
    
        public int SellerID { get; set; }
        public int StoreID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public System.DateTime Registration_date { get; set; }
        public byte[] Avatar { get; set; }
    
        public virtual ICollection<Dialog_dispute> Dialog_dispute { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
    }
}
