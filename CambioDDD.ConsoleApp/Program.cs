using CambioDDD.Application.Services;
using CambioDDD.Domain.Client;
using CambioDDD.Domain.Orders;
using CambioDDD.Infrastructure.Repositories;

// 1️⃣ Criar repositórios
var clienteRepo = new ClienteRepositoryInMemory();
var ordemRepo = new OrdemDeCambioRepositoryInMemory();

// 2️⃣ Criar services
var clienteService = new ClienteService(clienteRepo);
var ordemService = new OrdemDeCambioService(ordemRepo);

// 3️⃣ Criar clientes
clienteService.AdicionarCliente("Giu", "123456", "giu@email.com");
clienteService.AdicionarCliente("Joao", "654321", "joao@email.com");
clienteService.AdicionarCliente("Maria", "789123", "maria@email.com");
clienteService.AdicionarCliente("Lucas", "321987", "lucas@email.com");

// 4️⃣ Obter lista de clientes
var clientes = clienteService.ObterTodos().ToList();

// 5️⃣ Criar ordens de câmbio para cada cliente
var moedaOrigem = new Moeda("USD", "Dólar");
var moedaDestino = new Moeda("BRL", "Real");
foreach (var cliente in clientes)
{
    ordemService.Adicionar(
        cliente.ClientId,
        100m,
        moedaOrigem,
        moedaDestino,
        cliente.Nome
    );
}

// 6️⃣ Listar todas as ordens
foreach (var ordem in ordemService.ObterTodas())
{
    Console.WriteLine($"Ordem: {ordem.OrderId} , Valor: {ordem.ValorOperacao} , Moeda Origem: {ordem.MoedaOrigem.Nome} , Moeda Destino: {ordem.MoedaDestino.Nome} , " +
        $"IDCliente: {ordem.ClienteId}, Nome do Cliente: {ordem.NomeCliente}");
}

// 7️⃣ Buscar uma ordem por ID
var primeiraOrdem = ordemService.ObterTodas().First();
var busca = ordemService.ObterPorId(primeiraOrdem.OrderId);
Console.WriteLine($"\nBusca por ID {primeiraOrdem.OrderId}: Valor {busca.ValorOperacao} {busca.MoedaOrigem.Codigo}->{busca.MoedaDestino.Codigo}");
