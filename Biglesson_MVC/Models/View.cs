namespace Biglesson_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("View")]
    public partial class View
    {
        public int id { get; set; }

        public int product_id { get; set; }

        public string comment { get; set; }

        public int? vote { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? flat { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] time { get; set; }

        public virtual Product Product { get; set; }
    }
}
