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
    
    public partial class Message
    {
        public Message()
        {
            this.Pictures = new HashSet<Picture>();
        }
    
        public int MessageID { get; set; }
        public int DisputeID { get; set; }
        public string Text { get; set; }
        public System.DateTime Post_date { get; set; }
    
        public virtual Dialog_dispute Dialog_dispute { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}