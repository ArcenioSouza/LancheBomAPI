namespace LancheBom.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Lanche Lanche { get; set; }
        public List<Adicional> Adicionais { get; set; }
        public double ValorPedido { get; set; }
    }
}
