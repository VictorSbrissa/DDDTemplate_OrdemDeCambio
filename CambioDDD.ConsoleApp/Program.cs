using CambioDDD.Application.Handlers;
using CambioDDD.Application.Services;
using CambioDDD.Domain.Client;
using CambioDDD.Domain.Events;
using CambioDDD.Domain.Orders;
using CambioDDD.Infrastructure.Repositories;
using static CambioDDD.Domain.Enums.EnumStatus;
using static CambioDDD.Domain.Orders.OrdemDeCambio;


    // 1. Criar dispatcher e registrar handlers
    var dispatcher = new DomainEventDispatcher();
    dispatcher.Register(new OrdemCriadaHandler());

    // 2. Criar uma ordem
    var ordem = new OrdemDeCambio(
        Guid.NewGuid(),
        Guid.NewGuid(),
        1000m,
        new Moeda("USD", "Dólar Americano"),
        new Moeda("BRL", "Real Brasileiro"),
        "João da Silva"
    );

    // 3. Disparar eventos da ordem
    foreach (var evento in ordem.Eventos)
    {
        dispatcher.Dispatch(evento);
    }

    Console.ReadLine();
