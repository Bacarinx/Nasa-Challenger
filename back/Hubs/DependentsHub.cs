using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace back.Hubs
{
    public class DependentsHub : Hub
    {
        public async Task SendDependentRequest(string targetUserId, string requesterName)
        {
            // Envia a notificação para o usuário alvo
            await Clients.User(targetUserId).SendAsync("ReceiveDependentRequest", requesterName);
        }

        // Chamado quando o dependente aceita o pedido
        public async Task AcceptDependentRequest(string requesterId)
        {
            await Clients.User(requesterId).SendAsync("DependentRequestAccepted");
        }
    }
}