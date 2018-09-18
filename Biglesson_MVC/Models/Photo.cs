namespace Biglesson_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Photo
    {
        public int id { get; set; }

        public int product_id { get; set; }

        [StringLength(50)]
        public string image { get; set; }

        public virtual Product Product { get; set; }
    }
}
