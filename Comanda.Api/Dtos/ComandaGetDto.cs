namespace Comanda.Api.Dtos
{
    public class ComandaGetDto
    {
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; }
        public List<ComandaitensGetDto> ComandaItens { get; set;} = new List<ComandaitensGetDto>();


    }

    public class ComandaitensGetDto
    {
        public int Id{get; set;}
        public string Titulo { get; set; }
    }
}
