namespace Biglesson_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Photos = new HashSet<Photo>();
            Views = new HashSet<View>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name_product { get; set; }

        public int price { get; set; }

        public int cate_id { get; set; }

        public int favorite { get; set; }

        public int sale { get; set; }

        [Column("new")]
        public int _new { get; set; }

        public int star { get; set; }

        [Required]
        public string dicription { get; set; }

        public int quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string image { get; set; }

        public int brand_id { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<View> Views { get; set; }
    }
}
