using OS_Installation.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(connection));

var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionsBuilder.UseSqlServer(connection).Options;
builder.Services.AddControllers();

using (ApplicationContext db = new(options))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();


    OS windows = new() { Name = "Windows 7" };
    OS linux = new() { Name = "Linux" };
    db.OperatingSystems.AddRange(windows, linux);
    Installer yura = new() { Name = "Yura" };
    Installer zina = new() { Name = "Zina" };
    db.Installers.AddRange(yura, zina);
    Computer lim = new() { Name = "lim", Installer = yura, OS = windows };
    Computer opl = new() { Name = "opl", Installer = zina, OS = linux };
    db.Computers.AddRange(lim, opl);

    yura.OperatingSystems.Add(windows);
    zina.OperatingSystems.Add(linux);
    db.SaveChanges();

    Console.WriteLine("list of all computers:");
    var computers = db.Computers.AsNoTracking().ToList();
    foreach (var computer in computers) Console.WriteLine(computer.Name);
    var os = db.OperatingSystems.FirstOrDefault();
    if (os != null) db.OperatingSystems.Remove(os);
    db.SaveChanges();
    Console.WriteLine("\nlist of computers after a os is deleted:");
    computers = db.Computers.AsNoTracking().ToList();
    foreach (var computer in computers) Console.WriteLine(computer.Name);
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
