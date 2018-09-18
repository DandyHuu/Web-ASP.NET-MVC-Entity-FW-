namespace Biglesson_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int id { get; set; }

        [StringLength(50)]
        public string image { get; set; }

        public int? flat { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(200)]
        public string dicription { get; set; }
    }
}
