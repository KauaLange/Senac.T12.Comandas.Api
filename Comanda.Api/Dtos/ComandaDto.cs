namespace Comanda.Api.Dtos
{
    public class ComandaDto
    {
        public int NumeroMesa {  get; set; }
        public string NomeCliente { get; set; }
        // propriedade Array(vetor) int
        public int[] CardapioItens { get; set; } = [];

    }
}
