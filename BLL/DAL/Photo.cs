using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Photo
    {

        public int Id { get; set; }

      
     
        public bool isTakenStudio { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public DateTime? PhotoDate { get; set; }

        public decimal? ApertureValue { get; set; }

        public decimal? ShutterSpeed { get; set; }

        
        public int? PhotoTypesID { get; set; }


        public  PhotoTypes PhotoTypes { get; set; } //Navigational Property

        public List<PhotoOwner> PhotoOwners { get; set;} = new List<PhotoOwner>();
    }

}
