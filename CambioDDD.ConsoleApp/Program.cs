using CambioDDD.Application.Handlers;
using CambioDDD.Application.Services;
using CambioDDD.Domain.Client;
using CambioDDD.Domain.Enums;
using CambioDDD.Domain.Events;
using CambioDDD.Domain.Orders;
using CambioDDD.Infrastructure.Repositories;
using static CambioDDD.Domain.Enums.EnumStatus;
using static CambioDDD.Domain.Orders.OrdemDeCambio;


// 1. Criar o dispatcher
var dispatcher = new DomainEventDispatcher();

// 2. Registrar handlers
dispatcher.Register(new OrdemCriadaHandler());
dispatcher.Register<OrdemLiquidadaEvent>(new LogHandler());
dispatcher.Register(new NotificacaoHandler());

// 3. Simular disparo de eventos do domínio
var ordemCriada = new OrdemCriadaEvent(
    Guid.NewGuid(),
    Guid.NewGuid(),
    1000m,
    EnumStatus.EnumStatusOrdem.Criada,
    "Cliente Teste",
    new Moeda("USD", "Dolar"),
    new Moeda("BRL", "Real")
);
dispatcher.Dispatch(ordemCriada);

var ordemLiquidada = new OrdemLiquidadaEvent(
    ordemCriada.OrdemId,
    1000m
);
dispatcher.Dispatch(ordemLiquidada);

var ordemCancelada = new OrdemCanceladaEvent(
    ordemCriada.OrdemId,
    "Cliente desistiu"
);
dispatcher.Dispatch(ordemCancelada);

var ordemExpirada = new OrdemExpiradaEvent(
    ordemCriada.OrdemId
);
dispatcher.Dispatch(ordemExpirada);

Console.WriteLine("\n--- FIM DO TESTE ---");
Console.ReadKey();

