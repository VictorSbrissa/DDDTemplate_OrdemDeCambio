using System;
using Xunit;
using CambioDDD.Domain.Orders;
using CambioDDD.Domain.Enums;
using CambioDDD.Domain.Events;
using static CambioDDD.Domain.Enums.EnumStatus;


namespace CambioDDD.Tests
{
    public class OrdemDeCambioTests
    {
        private readonly Moeda _usd = new Moeda("USD", "Dolar");
        private readonly Moeda _brl = new Moeda("BRL", "Real");


        [Fact]
        public void CriarOrdem_DeveGerarStatusCriadaEEvento()
        {
            var ordem = new OrdemDeCambio(Guid.NewGuid(), Guid.NewGuid(), 1000m, _usd, _brl, "Cliente Teste");

            Assert.Equal(EnumStatusOrdem.Criada, ordem.StatusAtual);
            Assert.Single(ordem.Historico);
            Assert.Single(ordem.Eventos);
            Assert.IsType<OrdemCriadaEvent>(ordem.Eventos.First());
        }

        [Fact]
        public void LiquidarOrdem_DeveGerarEventoLiquidada()
        {
            var ordem = new OrdemDeCambio(Guid.NewGuid(), Guid.NewGuid(), 500m, _usd, _brl, "Cliente Teste");

            ordem.Liquidar(500m);

            Assert.Equal(EnumStatusOrdem.Liquidada, ordem.StatusAtual);
            Assert.Equal(2, ordem.Historico.Count);
            Assert.Equal(2, ordem.Eventos.Count);
            Assert.IsType<OrdemLiquidadaEvent>(ordem.Eventos.Last());
        }

        [Fact]
        public void CancelarOrdem_DeveGerarEventoCancelada()
        {
            var ordem = new OrdemDeCambio(Guid.NewGuid(), Guid.NewGuid(), 200m, _usd, _brl, "Cliente Teste");

            ordem.Cancelar("Cliente desistiu");

            Assert.Equal(EnumStatusOrdem.Cancelada, ordem.StatusAtual);
            Assert.Equal(2, ordem.Historico.Count);
            Assert.Equal(2, ordem.Eventos.Count);

            var evt = ordem.Eventos.Last() as OrdemCanceladaEvent;
            Assert.NotNull(evt);
            Assert.Equal("Cliente desistiu", evt.Motivo);
        }

        [Fact]
        public void ExpirarOrdem_DeveGerarEventoExpirada()
        {
            var ordem = new OrdemDeCambio(Guid.NewGuid(), Guid.NewGuid(), 300m, _usd, _brl, "Cliente Teste");

            ordem.Expirar();

            Assert.Equal(EnumStatusOrdem.Expirada, ordem.StatusAtual);
            Assert.Equal(2, ordem.Historico.Count);
            Assert.Equal(2, ordem.Eventos.Count);
            Assert.IsType<OrdemExpiradaEvent>(ordem.Eventos.Last());
        }

        [Fact]
        public void CriarOrdem_ComValorInvalido_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() =>
                new OrdemDeCambio(Guid.NewGuid(), Guid.NewGuid(), 0m, _usd, _brl, "Cliente Teste"));
        }

        [Fact]
        public void CriarOrdem_ComMoedaIgual_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() =>
                new OrdemDeCambio(Guid.NewGuid(), Guid.NewGuid(), 100m, _usd, _usd, "Cliente Teste"));
        }
    }
}