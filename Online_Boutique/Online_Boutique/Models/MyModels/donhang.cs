namespace Online_Boutique.Models.MyModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("donhang")]
    public partial class donhang
    {
        [Key]
        public int id_hd { get; set; }

        public int? masp { get; set; }

        [StringLength(50)]
        public string tenkh { get; set; }

        [StringLength(12)]
        public string sdt { get; set; }

        [StringLength(200)]
        public string email { get; set; }

        [StringLength(200)]
        public string diachi { get; set; }

        public int? soluong { get; set; }

        [StringLength(200)]
        public string ghichu { get; set; }

        public int? thanhtien { get; set; }

        [StringLength(50)]
        public string trangthai { get; set; }

        public DateTime? create_time { get; set; }

        public DateTime? update_time { get; set; }
    }
}
