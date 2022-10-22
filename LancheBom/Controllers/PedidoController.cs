using LancheBom.Database;
using LancheBom.Models;
using LancheBom.Services;
using Microsoft.AspNetCore.Mvc;
using LancheBom.DTO;

namespace LancheBom.Controllers
{
    [ApiController]
    [Route("/api/v1/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _service;

        public PedidoController(PedidoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> enviarPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                Pedido pedido = await _service.gerarPedido(pedidoDTO.idLanche, pedidoDTO.idAdicionais);
                return Created("", pedido);
            }
            catch(ArgumentNullException ex)
            {
                return NotFound(ex.ParamName);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<ActionResult> obterPedidos()
        {
            try
            {
                List<Pedido> pedidos = await _service.buscarPedidos();

                var resposta = pedidos.Select(pedido => new
                {
                    Id = pedido.Id,
                    Lanche = pedido.Lanche,
                    Adicionais = pedido.PedidoAdicional.Select(a => a.AdicionalId).ToList(),
                    ValorPedido = pedido.ValorPedido
                });

                return Ok(resposta);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.ParamName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }            
        }       

        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult> atualizarPedido(int id, PedidoDTO pedidoDTO)
        {
            try
            {
                var resposta = await _service.atualizarPedido(id, pedidoDTO.idLanche, pedidoDTO.idAdicionais);               
                return Ok(resposta);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.ParamName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirRegistro(int id)
        {
            try
            {
                string resposta = await _service.apagarPedido(id);
                return Ok(resposta);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.ParamName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}