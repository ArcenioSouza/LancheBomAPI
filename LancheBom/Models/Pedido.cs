namespace LancheBom.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Lanche Lanche { get; set; }
        public virtual ICollection<PedidoAdicional> PedidoAdicional { get; set; }
        public double ValorPedido { get; set; }
        public Boolean Status { get; set; }

        public Pedido()
        {
            PedidoAdicional = new HashSet<PedidoAdicional>();
            Status = true;
        }
    }
}
