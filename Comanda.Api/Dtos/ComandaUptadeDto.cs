namespace Comanda.Api.Dtos
{
    public class ComandaUptadeDto
    {
        public int Id {  get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; }
        public int[] CardapioItens { get; set; } = [];
    }
}
