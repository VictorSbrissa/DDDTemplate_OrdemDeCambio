using CambioDDD.Application.Services;
using CambioDDD.Domain.Orders;
using CambioDDD.Infrastructure.Repositories;

var repo = new OrdemDeCambioRepositoryInMemory();
var service = new OrdemDeCambioService(repo);

var moedaOrigem = new Moeda("USD", "Dólar");
var moedaDestino = new Moeda("BRL", "Real");


for (int i = 1; i < 5; i++)
{
    service.Adicionar(i, moedaOrigem, moedaDestino);
}

foreach (var order in service.ObterTodas())
{
    Console.WriteLine($"Ordem: {order.OrderId} , valor: {order.ValorOperacao} , moeda origem: {order.MoedaOrigem.Nome} , moeda destino: {order.MoedaDestino.Nome}");
}

var primeiraOrdem = service.ObterTodas().First();
var busca = service.ObterPorId(primeiraOrdem.OrderId);
Console.WriteLine($"\nBusca por ID {primeiraOrdem.OrderId}: Valor {busca.ValorOperacao} {busca.MoedaOrigem.Codigo}->{busca.MoedaDestino.Codigo}");
