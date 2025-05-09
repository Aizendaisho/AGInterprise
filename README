# AGInterprise

**AGInterprise** es una solución integral de gestión de inventarios y facturación dirigida a pequeñas y medianas empresas. Combina un backend robusto en .NET Core con un frontend moderno en React, permitiendo administrar productos, almacenes, movimientos de stock, clientes y facturas de forma segura y escalable.

---

## 📋 Características principales

* **Gestión de productos**: CRUD completo, categorías, precio unitario y código de barras.
* **Control de almacenes**: registro de almacenes y ubicaciones físicas.
* **Inventarios inteligentes**: entradas, salidas y transferencias entre almacenes, con validación de stock.
* **Facturación**: creación de facturas con detalles de producto, cálculo automático de totales y control de NCF.
* **Usuarios y roles**: administración de cuentas con roles fijos (Administrador, Supervisor, Vendedor) y asignación de almacén a vendedores.
* **Seguridad**: autenticación JWT + ASP.NET Core Identity, validaciones con FluentValidation.
* **API documentada**: OpenAPI / Swagger para exploración y generación de clientes.
* **Cliente React tipado**: cliente HTTP generado con NSwag y TypeScript.
* **Testing**: xUnit con EF InMemory para asegurar lógica de negocio.
* **Logging**: Serilog para seguimiento de peticiones y eventos.

---

## 🛠️ Flujo de trabajo

1. **Inicialización**:

   * Al arrancar la aplicación se siembran roles fijos (Administrador, Supervisor, Vendedor) y un usuario admin por defecto.
   * Se crea un almacén predeterminado.
2. **Gestión de inventario**:

   * El administrador registra productos y almacenes.
   * Vendedores realizan movimientos (Entrada, Salida, Transferencia), ajustando existencias.
3. **Facturación**:

   * Registro de clientes.
   * Generación de facturas vinculadas a movimientos de stock.
4. **Consultas**:

   * Endpoints para listar y consultar productos, clientes, facturas e inventarios.
   * Auditoría de acciones por usuario.

---

## 🧰 Tecnologías

### Backend

* **.NET 9** & ASP.NET Core Web API
* **Entity Framework Core** (SQL Server)
* **ASP.NET Core Identity** + JWT
* **Serilog** (parquet/Console/File)
* **FluentValidation** para reglas de negocio
* **Swagger/Swashbuckle** (OpenAPI)
* **xUnit** & EF InMemory para pruebas unitarias

### Frontend

* **React 19** (Vite)
* **TypeScript**
* **Tailwind CSS** + shadcn/ui
* **React Router**
* **React Query**
* **Zustand** (estado global)
* **Framer Motion** (animaciones)
* **react-toastify** (notificaciones)
* **NSwag** (generación de cliente API)

---

## 📁 Estructura de carpetas

```
/backend
  ├─ AGInterprise.Domain
  ├─ AGInterprise.Application
  ├─ AGInterprise.Infrastructure
  ├─ AGInterprise.WebApi
  └─ AGInterprise.Tests
/frontend
  └─ src
      ├─ api            # Cliente TS generado por NSwag
      ├─ hooks
      ├─ components
      ├─ pages
      ├─ context
      └─ styles
```

---

## ⚙️ Generar cliente OpenAPI

```bash
git clone <repo-url>
cd frontend/src/api
swag run nswag.json /runtime:Net90
```

---

## ✅ Tareas completadas

* Autenticación & autorización con roles fijos
* Validaciones con FluentValidation
* Pruebas unitarias de servicios
* Cliente React básico + contexto de Auth

## 🔲 Tareas pendientes

* CRUD completo en frontend (Productos, Almacenes, Inventarios…)
* Dashboard y reportes de inventario
* Theming dinámico basado en el logo
* Dockerización de backend y frontend
* Configurar CI/CD (GitHub Actions)
* Despliegue en hosting (Azure, AWS, etc.)

---

© 2025 AGInterprise. Todos los derechos reservados.
