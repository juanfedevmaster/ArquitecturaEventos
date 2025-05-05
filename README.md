# 🩺 Arquitectura de Eventos con NATS, .NET y SignalR

Este repositorio presenta una arquitectura orientada a eventos para monitoreo de dispositivos médicos en tiempo real, utilizando:

- **NATS** como sistema de mensajería
- **JetStream** para persistencia y durabilidad de mensajes
- **ASP.NET Core WebAPI** como backend
- **SignalR** para comunicación en tiempo real con el frontend
- **JavaScript** en el frontend para visualización de eventos

## 📦 Estructura del Proyecto

```
ArquitecturaEventos/
├── backend-dotnet/        # Proyecto ASP.NET Core WebAPI
│   ├── Hubs/              # Hubs de SignalR
│   ├── Services/          # Servicios de escucha de NATS
│   └── Program.cs         # Configuración de la aplicación
├── frontend/              # Aplicación frontend en JavaScript
│   └── index.html         # Interfaz de usuario
└── publisher/             # Simulador de dispositivos médicos
    └── Program.cs         # Publicador de eventos a NATS
```

## 🚀 ¿Cómo Funciona?

1. **Simulador de Dispositivos Médicos**: Una aplicación de consola (`publisher/`) que envía datos simulados de dispositivos médicos a un *subject* de NATS (`dispositivo.datos`).

2. **Backend en .NET**: Una aplicación ASP.NET Core WebAPI (`backend-dotnet/`) que:
   - Se conecta a NATS utilizando credenciales `.creds`.
   - Escucha eventos en el *subject* `dispositivo.datos` mediante JetStream.
   - Reenvía los eventos recibidos a todos los clientes conectados a través de SignalR.

3. **Frontend en JavaScript**: Una aplicación web (`frontend/`) que:
   - Se conecta al Hub de SignalR expuesto por el backend.
   - Muestra en tiempo real los eventos recibidos desde los dispositivos médicos.

## 🛠️ Requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js](https://nodejs.org/) (opcional, si se desea utilizar herramientas de desarrollo frontend)
- [NATS CLI](https://docs.nats.io/nats-tools/nats_cli) (para pruebas y administración)
- Cuenta en [Synadia NGS](https://app.ngs.global/) para utilizar NATS en la nube

## ⚙️ Configuración

1. **Clonar el repositorio**:

   ```bash
   git clone https://github.com/juanfedevmaster/ArquitecturaEventos.git
   cd ArquitecturaEventos
   ```

2. **Configurar el Backend**:

   - Navegar a la carpeta `backend-dotnet/`.
   - Restaurar paquetes y compilar el proyecto:

     ```bash
     dotnet restore
     dotnet build
     ```

   - Ejecutar la aplicación:

     ```bash
     dotnet run
     ```

3. **Configurar el Frontend**:

   - Navegar a la carpeta `frontend/`.
   - Abrir el archivo `index.html` en un navegador web.

4. **Ejecutar el Simulador de Dispositivos Médicos**:

   - Navegar a la carpeta `publisher/`.
   - Restaurar paquetes y compilar el proyecto:

     ```bash
     dotnet restore
     dotnet build
     ```

   - Ejecutar la aplicación:

     ```bash
     dotnet run
     ```

## 🧪 Pruebas

- Al ejecutar el simulador, se enviarán datos simulados a NATS.
- El backend recibirá estos eventos y los retransmitirá a través de SignalR.
- El frontend mostrará en tiempo real los datos recibidos.

## 📚 Recursos Adicionales

- [Documentación de NATS](https://docs.nats.io/)
- [JetStream - Persistencia en NATS](https://docs.nats.io/nats-concepts/jetstream)
- [ASP.NET Core SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction)
- [SignalR JavaScript Client](https://learn.microsoft.com/en-us/aspnet/core/signalr/javascript-client)

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Si deseas mejorar este proyecto, por favor abre un *issue* o envía un *pull request*.
