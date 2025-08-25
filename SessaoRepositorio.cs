using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQL_com_C_
{

    public class SessaoRepositorio
    {
        private string connectionString;

        public SessaoRepositorio(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Cadastrar(Sessao sessao)
        {
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "INSERT INTO Sessoes (IdFilme, Data, Hora) VALUES (@idFilme, @data, @hora)";
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@idFilme", sessao.IdFilme);
            cmd.Parameters.AddWithValue("@data", sessao.Data);
            cmd.Parameters.AddWithValue("@hora", sessao.Hora);
            cmd.ExecuteNonQuery();
        }

        public List<Sessao> ListarPorFilme(int idFilme)
        {
            var sessoes = new List<Sessao>();
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "SELECT IdSessao, IdFilme, Data, Hora FROM Sessoes WHERE IdFilme = @idFilme";
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@idFilme", idFilme);

             var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sessoes.Add(new Sessao
                {
                    IdSessao = reader.GetInt32(0),
                    IdFilme = reader.GetInt32(1),
                    Data = reader.GetDateTime(2),
                    Hora = (TimeSpan)reader[3]
                });
            }

            return sessoes;
        }

        public void Atualizar(Sessao sessao)
        {
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "UPDATE Sessoes SET Data = @data, Hora = @hora WHERE IdSessao = @idSessao";
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@data", sessao.Data);
            cmd.Parameters.AddWithValue("@hora", sessao.Hora);
            cmd.Parameters.AddWithValue("@idSessao", sessao.IdSessao);
            cmd.ExecuteNonQuery();
        }

        public void Excluir(int idSessao)
        {
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "DELETE FROM Sessoes WHERE IdSessao = @id";
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", idSessao);
            cmd.ExecuteNonQuery();
        }
    }

}