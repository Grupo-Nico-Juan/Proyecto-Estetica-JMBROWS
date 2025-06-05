# Proyecto-Estetica-JMBROWS
Proyecto final de carrera para el cliente JMBROWS local de estetica
# 🧼 JMBROWS Web API

API REST desarrollada en .NET 8 para la gestión de turnos, servicios, empleados y clientes de una estética.  
Incluye autenticación JWT, control de acceso por roles y despliegue automatizado a Azure.

---

## 🚀 Despliegue automático (CI/CD)

Este repositorio utiliza **GitHub Actions** para compilar y desplegar la Web API en **Azure App Service** cada vez que se hace `push` a la rama `main`.

### 🔧 Configuración

#### 1. Crear un Service Principal en Azure

```bash
az ad sp create-for-rbac --name "github-deploy-jmbrows" --role contributor \
  --scopes /subscriptions/<SUBSCRIPTION_ID>/resourceGroups/<RESOURCE_GROUP>/providers/Microsoft.Web/sites/<APP_SERVICE_NAME> \
  --sdk-auth

El workflow:

Compila el proyecto con .NET 8

Publica los archivos a /publish

Inicia sesión en Azure con el secreto

Despliega automáticamente a Azure App Service

📦 Requisitos
Azure App Service creado (ej: apiJMBROWSV1)

.NET 8 instalado en el entorno de deploy

Archivo appsettings.json configurado correctamente para producción

📁 Proyecto
La solución incluye:

apiJMBROWS: Web API con endpoints REST

LogicaNegocio: Entidades y validaciones de dominio

LogicaAccesoDatos: Repositorios con EF Core

LogicaAplicacion: Casos de uso y DTOs

🔐 Seguridad
Autenticación con JWT

Contraseñas hasheadas con PasswordHasher<T>

Middleware de manejo de errores personalizados

🧪 Testing
(En construcción)
