using Libreria.LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace apiJMBROWS.Hubs
{
    public class NotificacionesHub : Hub
    {

        [Authorize(Roles = "Administrador")]
        public override async Task OnConnectedAsync()
        {
            var usuario = Context.User;
            if (usuario?.Identity?.IsAuthenticated == true)
            {
                // Verificamos si tiene el rol de administrador
                if (usuario.IsInRole("Administrador"))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Administradores");
                }
            }

            await base.OnConnectedAsync();
        }

        [Authorize(Roles = "Administrador")]
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var usuario = Context.User;

            if (usuario?.Identity?.IsAuthenticated == true)
            {
                if (usuario.IsInRole("Administrador"))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Administradores");
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}