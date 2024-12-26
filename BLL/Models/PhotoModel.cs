using BLL.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PhotoModel // aka DTO "Data Transfer Object"
    {
        public Photo Record { get; set; }

        public string Name => Record.Title;

        [DisplayName("Studio")] // title : displayname for html helper
        public string isTakenStudio => Record.isTakenStudio ? "In Studio" : "Out Studio";


        [DisplayName("Photo Date")]
        public string PhotoDate => !Record.PhotoDate.HasValue ? string.Empty : Record.PhotoDate.Value.ToString("MM/dd/yyyy");


        public string ApertureValue => Record.ApertureValue.HasValue ? Record.ApertureValue.Value.ToString("N2") : "0";

        public string ShutterSpeed => (Record.ShutterSpeed ?? 0).ToString("N1");

        public string PhotoTypes => Record.PhotoTypes?.Name;

        public string Owners => string.Join("<br>", Record.PhotoOwners?.Select(po => po.Owner?.Name + " " + po.Owner?.Surname));

        [DisplayName("Owners")]

        public List<int> OwnerIds {

            get => Record.PhotoOwners?.Select(po => po.OwnerId).ToList();
            set => Record.PhotoOwners = value.Select(v => new PhotoOwner() { OwnerId = v }).ToList();



        }

    }
}
