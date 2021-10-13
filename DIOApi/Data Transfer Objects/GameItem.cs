using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace DIOApi.DTOs
{
    [XmlType("Game")]
    [Table("GamingRepository")]
    public class GameItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Year { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string Platforms { get; set; }
    }
}
