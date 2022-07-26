namespace OS_Installation.Models
{
    public class OS
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Computer> Computers { get; set; } = new();
        public List<Installer> Installers { get; set; } = new();
    }
}
