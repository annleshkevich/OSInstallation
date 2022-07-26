using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OS_Installation.Models
{
    public class Computer
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("OSId")]
        public int? OSId { get; set; }
        public OS? OS { get; set; }
        [ForeignKey("InstallerId")]
        public int? InstallerId { get; set; }
        public Installer? Installer { get; set; }
    }
}
