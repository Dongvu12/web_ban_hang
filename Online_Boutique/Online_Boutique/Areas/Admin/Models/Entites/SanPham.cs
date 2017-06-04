namespace Online_Boutique.Areas.Admin.Models.Entites
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        public int masp { get; set; }

        [StringLength(50)]
        public string tensp { get; set; }

        public int? giagocsp { get; set; }

        public int? maloaisp { get; set; }

        public int? giabansp { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaynhapsp { get; set; }

        public int? soluongsp { get; set; }

        [StringLength(50)]
        public string motasp { get; set; }

        [StringLength(200)]
        public string anhsp { get; set; }

        [StringLength(50)]
        public string tinhtrangsp { get; set; }

        [StringLength(50)]
        public string size { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }
    }
}
