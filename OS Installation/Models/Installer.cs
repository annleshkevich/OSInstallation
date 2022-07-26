namespace OS_Installation.Models
{
    public class Installer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Computer> Computers { get; set; } = new();
        public List<OS> OperatingSystems { get; set; } = new();
    }
}
