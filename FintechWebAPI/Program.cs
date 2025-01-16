using FintechWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto de la base de datos (DataContext) con la cadena de conexión
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Swagger para generar documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración para que las respuestas JSON no cambien la convención de nombres de las propiedades
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Crear la aplicación
var app = builder.Build();

// Configuración de la tubería de solicitudes HTTP (middlewares)
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger en el entorno de desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar HTTPS para las peticiones
app.UseHttpsRedirection();

// Habilitar la autorización para las rutas protegidas (si es necesario)
app.UseAuthorization();

// Habilitar la página de bienvenida (esto es opcional y depende de tu configuración)
app.UseWelcomePage();

// Mapea los controladores para que se reconozcan las rutas de la API
app.MapControllers();


app.UseWelcomePage(new WelcomePageOptions
{
    Path = "/"
});

// Ejecuta la aplicación
app.Run();