using CambioDDD.Application.Services;
using CambioDDD.Domain.Client;
using CambioDDD.Domain.Orders;
using CambioDDD.Infrastructure.Repositories;

var repo = new OrdemDeCambioRepositoryInMemory();
var service = new OrdemDeCambioService(repo);

var moedaOrigem = new Moeda("USD", "Dólar");
var moedaDestino = new Moeda("BRL", "Real");

List<Cliente> clientes = new List<Cliente>();

clientes.Add(new Cliente("Giu", "123456", "giu@email.com"));
clientes.Add(new Cliente("Joao", "123456", "giu@email.com"));
clientes.Add(new Cliente("Maria", "123456", "giu@email.com"));
clientes.Add(new Cliente("Lucas", "123456", "giu@email.com"));

foreach (Cliente icliente in clientes)
{
    service.Adicionar(icliente.ClientId, 10, moedaOrigem, moedaDestino, icliente.Nome);
}

foreach (var order in service.ObterTodas())
{
    Console.WriteLine($"Ordem: {order.OrderId} , valor: {order.ValorOperacao} , moeda origem: {order.MoedaOrigem.Nome} , moeda destino: {order.MoedaDestino.Nome} , \n " +
        $"IDCliente : {order.ClienteId}, Nome do Cliente: {order.NomeCliente}");
}

var primeiraOrdem = service.ObterTodas().First();
var busca = service.ObterPorId(primeiraOrdem.OrderId);
Console.WriteLine($"\nBusca por ID {primeiraOrdem.OrderId}: Valor {busca.ValorOperacao} {busca.MoedaOrigem.Codigo}->{busca.MoedaDestino.Codigo}");
