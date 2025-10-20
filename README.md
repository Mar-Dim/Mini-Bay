## 🛍 MiniBay

MiniBay es una aplicación fullstack desarrollada con *.NET 9, **Blazor WebAssembly* y *Onion Architecture*, que implementa una estructura modular para mantener una clara separación de responsabilidades entre capas.

---

## 🧩 Estructura del Proyecto

El proyecto está organizado de la siguiente forma:

MiniBay/
├── MiniBay.API/ → Capa backend (Web API con .NET 9)
├── MiniBay.Client/ → Capa frontend (Blazor WebAssembly)
├── MiniBay.Shared/ → Modelos y contratos compartidos entre Client y API
├── MiniBay.Domain/ → Entidades y lógica de negocio
├── MiniBay.Application/ → Casos de uso, servicios y validaciones
├── MiniBay.Infrastructure/ → Acceso a datos (EF Core, repositorios, SQL)


Esta arquitectura promueve:
- *Separación de capas y responsabilidades*
- *Escalabilidad y mantenibilidad*
- *Reutilización de modelos compartidos (Shared)*

---

## 🐳 Base de Datos con Docker

La base de datos de *MiniBay* se ejecuta en un contenedor *SQL Server 2022*.

## 🧱 1. Crear el contenedor

Ejecuta este comando (asegúrate de tener Docker Desktop corriendo):
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Clave123" \
-p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

Esto levantará un contenedor con SQL Server escuchando en el puerto 1433.

---

## ⚙ 2. Configuración de conexión

El archivo appsettings.json dentro de MiniBay.API incluye la cadena de conexión:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=MiniBayDb;User Id=sa;Password=Clave123;TrustServerCertificate=True"
}

Y el contexto se configura en Program.cs:
builder.Services.AddDbContext<MiniBayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

---

## 3. Migraciones y actualización de la base


Para aplicar las migraciones de Entity Framework:

dotnet ef database update --project MiniBay.Infrastructure
dotnet tool install --global dotnet-ef

---


## 🚀 Ejecución del Proyecto

Asegúrate de tener:

Docker en ejecución (con el contenedor de SQL Server activo)

.NET 9 SDK instalado

🔹 Opción 1: Ejecutar todo desde Visual Studio

Establece MiniBay.API como proyecto de inicio.

Ejecuta con F5 o Ctrl+F5 para levantar API + Client (si está configurado el proxy).

🔹 Opción 2: Ejecutar manualmente desde CLI

# Levantar la API
dotnet run --project MiniBay.API

# (En otra terminal) Levantar el cliente Blazor
dotnet run --project MiniBay.Client

Luego abre en tu navegador:
👉 Frontend (Blazor): https://localhost:5195
