namespace Online_Boutique.Models.MyModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [StringLength(50)]
        public string tentaikhoan { get; set; }

        [StringLength(50)]
        public string matkhau { get; set; }

        [StringLength(50)]
        public string hoten { get; set; }

        [StringLength(10)]
        public string gioitinh { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string sdt { get; set; }

        [StringLength(100)]
        public string Diachi { get; set; }

        [StringLength(50)]
        public string quyenhan { get; set; }

        public int id { get; set; }
    }
}
