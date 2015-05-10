using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wholesale_Marketplace.Models
{
    [MetadataTypeAttribute(typeof(UserMetadata))]
    public partial class User { }

    class UserMetadata
    {
        [Required]
        //[RegularExpression("^[a-zA-Z][a-zA-Z0-9-_\\.]{5,10}$")]
        public string Login { get; set; }

        [Required]
        //[Range(4, 10)]
        //[RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).*$")]
        public string Password { get; set; }

        [Required]
        //[RegularExpression("^[-\\w.]+@([A-z0-9][-A-z0-9]+\\.)+[A-z]{2,4}$")]
        public string Email { get; set; }
    }




    [MetadataTypeAttribute(typeof(BuyerMetadata))]
    public partial class Buyer { }

    class BuyerMetadata
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

    }


    [MetadataTypeAttribute(typeof(SellerMetadata))]
    public partial class Seller { }

    class SellerMetadata
    {
        [Required]
        public string Name { get; set; }

    }

    [MetadataTypeAttribute(typeof(StoreMetadata))]
    public partial class Store { }

    class StoreMetadata
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SecretCode { get; set; }

    }
}