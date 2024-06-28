# Proyecto Blazor con MudBlazor

## Introducción

Este proyecto es una aplicación web desarrollada con Blazor y utiliza MudBlazor para la interfaz de usuario. La implementación de MudBlazor en este proyecto presentó desafíos significativos, ya que fue la primera vez que se integró en un proyecto desde cero. La necesidad de utilizar un template de la página oficial de MudBlazor se hizo evidente para facilitar la configuración inicial y asegurar una base sólida.

Además, este proyecto marca la primera experiencia trabajando con Blazor, lo que añadió un nivel de complejidad adicional. A pesar de ello, se logró aprovechar el conocimiento existente en C# y patrones de arquitectura como MVC para estructurar adecuadamente la aplicación.
## Desafíos y Aprendizajes
Integración de MudBlazor
Implementar MudBlazor en un proyecto nuevo fue un reto considerable debido a la falta de familiaridad con la biblioteca y la necesidad de ajustar la configuración inicial. La utilización de un template de la página oficial de MudBlazor fue crucial para superar estos obstáculos y proporcionar una base sólida desde la cual construir.

## Primeros Pasos con Blazor
El proyecto también representó la primera experiencia trabajando con Blazor, lo que implicó una curva de aprendizaje. La adaptación a los paradigmas de Blazor y la integración con MudBlazor requirieron un esfuerzo significativo, pero resultaron en una valiosa experiencia de aprendizaje.

## Uso de Conocimientos Previos
A pesar de los desafíos, se logró aprovechar el conocimiento previo en C# y patrones de arquitectura como MVC para estructurar adecuadamente el proyecto. El uso de Entity Framework y la separación del proyecto de datos del cliente son ejemplos de cómo se aplicaron buenas prácticas de desarrollo para asegurar un código limpio y mantenible.

## Características del Proyecto

- **Result Pattern**: Se intento utilizar para manejar los resultados de las operaciones asíncronas, proporcionando una manera clara de gestionar los éxitos y errores en donde era posible remplazar el uso de try-catch.
- **Interfaces**: Implementadas para definir contratos claros entre los servicios y sus implementaciones, facilitando el mantenimiento y la extensibilidad del código.
- **Recursos (Resources)**: Configurados para soportar una posible internacionalización del idioma, permitiendo que la aplicación sea adaptada a diferentes locales (Incompleto).
- **Diccionarios de Predicados**: Utilizados para mapear filtros a sus respectivas funciones de manera eficiente, mejorando la claridad y mantenibilidad del código.
- **Entity Framework**: Empleado para manejar la interacción con la base de datos, con el proyecto de datos separado del cliente para seguir buenas prácticas de arquitectura.
- **Unit Test** : Usadas para comprobar el funcionamiento del servicio.

## Descripción del Código

### Estructura del Proyecto

- **Client**: Contiene la lógica del lado del cliente, incluyendo los componentes Blazor y la interfaz de usuario.
- **Data**: Maneja la lógica de acceso a datos utilizando Entity Framework. Este proyecto está separado del cliente para asegurar una clara separación de preocupaciones y facilitar las pruebas y el mantenimiento.
- **Resource** : Contiene las traducciones
- **BlazorApp**: Maneja la lógica de implementación de MudBlazor, así como el registro de las interfaces y Entity Framework. Este proyecto actúa como el núcleo donde se configuran y registran los servicios necesarios para la aplicación.

### Componentes Clave

#### `PersonService`

El servicio `PersonService` es responsable de manejar las operaciones relacionadas con las entidades `Person` y `AccountDetail`. Implementa los métodos definidos en la interfaz `IPersonService` y utiliza Entity Framework para interactuar con la base de datos.


public class PersonService : IPersonService
{
    private readonly AppDbContext _context;

    public PersonService(AppDbContext context)
    {
        _context = context;
    }

    // Métodos para obtener y manipular personas y detalles de cuenta
}
#### `Home.razor y Home.razor.cs`
El componente Home en Blazor utiliza MudBlazor para renderizar la interfaz de usuario. Se implementan formularios para agregar personas y detalles de cuenta, y se utilizan selectores y tablas para mostrar y filtrar los datos.
<MudContainer>
    <!-- Código del componente -->
</MudContainer>
El archivo de código subyacente Home.razor.cs maneja la lógica del componente, incluyendo la interacción con los servicios y la aplicación de filtros.
public partial class Home : ComponentBase
{
    [Inject]
    private IPersonService PersonService { get; set; }

    // Métodos y propiedades del componente
}
### Configuración y Registro de Servicios
En el proyecto inicial, se maneja la configuración de MudBlazor y el registro de servicios e interfaces. Aquí se muestra un ejemplo de cómo se realiza esta configuración:
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMudServices();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPersonService, PersonService>();

        // Otros servicios
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
## Pruebas Unitarias
Para asegurar la calidad y funcionalidad del código, este proyecto incluye un conjunto de pruebas unitarias utilizando xUnit como framework de pruebas y Moq para crear mocks de las dependencias. Las pruebas unitarias se enfocan en verificar la correcta implementación de los métodos del `PersonService`.


## Conclusión
Este proyecto es un testimonio de la capacidad para adaptarse a nuevas tecnologías y superar desafíos técnicos. La integración de MudBlazor y Blazor, junto con la aplicación de patrones y arquitecturas bien establecidos, resultó en una aplicación funcional y extensible. Los aprendizajes obtenidos de esta experiencia serán invaluables para futuros proyectos y mejoras continuas.