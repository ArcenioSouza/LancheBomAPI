using LancheBom.Models;

namespace LancheBom.DTO
{
   public class PedidoDTO
   {
      public int idLanche { get; set; }
      public int[] idAdicionais { get; set; } = new int[2];        
   }    
}