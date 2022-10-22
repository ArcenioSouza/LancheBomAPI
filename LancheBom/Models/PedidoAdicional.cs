namespace LancheBom.Models
{
    public class PedidoAdicional
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int AdicionalId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Adicional Adicional { get; set; }
    }
}
