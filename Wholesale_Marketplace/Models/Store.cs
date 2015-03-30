//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wholesale_Marketplace.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store
    {
        public Store()
        {
            this.Items = new HashSet<Item>();
            this.Sellers = new HashSet<Seller>();
        }
    
        public int StoreID { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Orders_count { get; set; }
        public int Positive_marks { get; set; }
        public int Negative_marks { get; set; }
        public System.DateTime Created_date { get; set; }
        public string Description { get; set; }
        public int Owner_sellerID { get; set; }
    
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
    }
}
