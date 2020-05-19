using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACRRentalCarLiveCodingSegunda2020_1
{
    // colocando a classe com public para usá-la onde quisermos
    public class Conexao
    {
        // método que será chamado quando quisermos nos conectar com o banco
        public static SqlConnection GetConnection()
        {
            // objeto que recebe a string de conexão
            SqlConnection connection = 
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DB_RENTALCAR_DSS;Integrated Security=True;Pooling=False");
            // retorna a conexão com o BD
            return connection;
        }

    }
}
