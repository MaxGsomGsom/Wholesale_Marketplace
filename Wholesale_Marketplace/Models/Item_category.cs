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
    
    public partial class Item_category
    {
        public Item_category()
        {
            this.Items = new HashSet<Item>();
        }
    
        public int CategoryID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Item> Items { get; set; }
    }
}
