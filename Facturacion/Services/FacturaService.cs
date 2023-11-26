using Facturacion.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Facturacion.Services
{
    public class FacturaService
    {
        private readonly string _connectionString;

        public FacturaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevLabConnection");
        }

        public void GuardarFactura(FacturaViewModel facturaViewModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var facturaId = GuardarFacturaEnBaseDeDatos(facturaViewModel, connection, transaction);

                        
                        GuardarDetallesDeFacturaEnBaseDeDatos(facturaViewModel, facturaId, connection, transaction);

                        
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                       
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Cliente> ObtenerClientes()
        {
           
            return new List<Cliente>(); 
        }

        public List<Producto> ObtenerProductos()
        {    
            return new List<Producto>(); 
        }

       
        public FacturaViewModel ObtenerFacturaPorId(int facturaId)
        {
            return new FacturaViewModel(); 
        }

        private int GuardarFacturaEnBaseDeDatos(FacturaViewModel facturaViewModel, SqlConnection connection, SqlTransaction transaction)
        {
           
            using (var command = new SqlCommand("GuardarFactura", connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClienteId", facturaViewModel.ClienteId);
                command.Parameters.AddWithValue("@NumeroFactura", facturaViewModel.NumeroFactura);

                var facturaIdParameter = new SqlParameter("@FacturaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(facturaIdParameter);

                command.ExecuteNonQuery();

                return (int)facturaIdParameter.Value;
            }
        }

        private void GuardarDetallesDeFacturaEnBaseDeDatos(FacturaViewModel facturaViewModel, int facturaId, SqlConnection connection, SqlTransaction transaction)
        {
          
            foreach (var detalle in facturaViewModel.DetalleFacturaViewModel)
            {
                using (var command = new SqlCommand("GuardarDetalleFactura", connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@FacturaId", facturaId);
                    command.Parameters.AddWithValue("@ProductoId", detalle.Producto.Id); 
                    command.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);

                    // Otros parámetros según tu modelo

                    command.ExecuteNonQuery();
                }
            }
        }

       
    }
}
