#**Sistema de Autenticación y Roles**

Este proyecto implementa un sistema robusto de Autenticación y Autorización utilizando ASP.NET Core Identity. La aplicación gestiona el acceso mediante tres niveles de roles (Admin, Editor, Cliente), protegiendo las rutas críticas y permitiendo la administración de usuarios en tiempo real.

🔐 **Matriz de Permisos y Roles**
| Rol | Descripción | Nivel de Acceso|
|--------------|--------------|--------------|
|Admin|	Superusuario con control total sobre el sistema y los usuarios.|	Total (Acceso a Panel Admin y Edición).|
| Editor|	Usuario con permisos para gestionar y modificar el contenido del sitio.|	Intermedio (Acceso a Vistas de Edición).|
|Cliente|	Usuario estándar que solo puede navegar y ver información.|	Limitado (Solo Lectura). |

🛤️ Rutas Protegidas
La aplicación utiliza atributos [Authorize] para segmentar el acceso a los controladores y acciones.

  1. Panel de Administración (/Admin)
      - Acceso: Solo Admin.
      - Funcionalidad: Listado completo de usuarios registrados, asignación de rol "Editor" y gestión de rol "Cliente".
  2. Gestión de Contenido (/Home/Edit/{id})
      - Acceso: Admin y Editor.
      - Funcionalidad: Formulario para la modificación de avisos, promociones y contenido general de la tienda.
      
  3. Áreas Públicas (/Account/Login, /Home/Index)
      - Acceso: Anónimo (Cualquier visitante).
      - Funcionalidad: Inicio de sesión, registro y visualización de la página principal.

🔑 Credenciales de Prueba
Para evaluar el sistema, se han configurado los siguientes usuarios predeterminados (vía DbInitializer):

**Nota:** Todas las contraseñas siguen el estándar de seguridad de Identity (Mayúscula, minúscula, número y carácter especial).

- Administrador:

   - User: admin@miapp.com
   - Pass: Admin123!

- Editor:

    - User: editor@prueba.com
    - Pass: Editor123

-Cliente Estándar:

  - User: cliente@prueba.com
  - Pass: Cliente123

🛠️ Tecnologías Utilizadas
- ASP.NET Core 8.0 (MVC).

- Entity Framework Core para la persistencia de datos y migraciones.

- ASP.NET Core Identity para la gestión de usuarios y roles.

- Bootstrap 5 para el diseño responsivo del Panel Administrativo.

¿Cómo probar el flujo de trabajo?
1. Inicie sesión como Admin.

2. Diríjase al Panel de Administración en el menú superior.

3. Localice un usuario con rol Cliente y presione "+ Editor".

4. Cierre sesión e inicie con ese usuario para verificar que ahora tiene acceso a las funciones de Modificar Contenido.
