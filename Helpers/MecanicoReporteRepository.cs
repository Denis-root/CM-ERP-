using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.SqlClient;

using ConstruMarket_ERP_V0._1.Models.MecaRepo01;

namespace ConstruMarket_ERP_V0._1.Helpers
{
    internal class MecanicoReporteRepository
    {
        
        private string connectionString = @"data source=172.16.1.207;initial catalog=TALLER;persist security info=True;user id=sa;password=Jaiba2024$;MultipleActiveResultSets=True;App=EntityFramework";

        public (List<MecanicoReporte>, TotalTarifa) GetMecanicosReport()
        {
            var mecanicosReporte = new List<MecanicoReporte>();
            var totalTarifa = new TotalTarifa();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_mecanicos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mecanicosReporte.Add(new MecanicoReporte
                            {
                                Codigo = reader["codigo"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                TarifaHora = (decimal)reader["tarifaHora"]
                            });
                        }

                        if (reader.NextResult() && reader.Read())
                        {
                            totalTarifa.TotalTarifaHora = (decimal)reader["TotalTarifaHora"];
                        }
                    }
                }
            }

            return (mecanicosReporte, totalTarifa);
        }

    }
}
