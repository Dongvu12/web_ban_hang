using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace Online_Boutique.Models.MyModels
{
    [Table("ThuongHieu")]
    public class ThuongHieu
    {
        
        [Key]
        public int ma_thuonghieu { get; set; }

        [StringLength(50)]
        public string ten_thuonghieu { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}