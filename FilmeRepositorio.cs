using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQL_com_C_
{

    public class FilmeRepositorio
    {
        private string connectionString;

        public FilmeRepositorio(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Cadastrar(Filme filme)
        {
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "INSERT INTO Filmes (Titulo, Genero, Ano) VALUES (@titulo, @genero, @ano)";
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@titulo", filme.Titulo);
            cmd.Parameters.AddWithValue("@genero", filme.Genero);
            cmd.Parameters.AddWithValue("@ano", filme.Ano);
            cmd.ExecuteNonQuery();
        }

        public List<Filme> Listar()
        {
            var filmes = new List<Filme>();
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "SELECT IdFilme, Titulo, Genero, Ano FROM Filmes";
             var cmd = new SqlCommand(sql, con);
            
             var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                filmes.Add(new Filme
                {
                    IdFilme = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Genero = reader.GetString(2),
                    Ano = reader.GetInt32(3)
                });
            }
            return filmes;
        }

        public void Atualizar(Filme filme)
        {
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "UPDATE Filmes SET Titulo = @titulo, Genero = @genero, Ano = @ano WHERE IdFilme = @id";
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@titulo", filme.Titulo);
            cmd.Parameters.AddWithValue("@genero", filme.Genero);
            cmd.Parameters.AddWithValue("@ano", filme.Ano);
            cmd.Parameters.AddWithValue("@id", filme.IdFilme);
            cmd.ExecuteNonQuery();
        }

        public void Excluir(int idFilme)
        {
             var con = new SqlConnection(connectionString);
            con.Open();

            string sql = "DELETE FROM Filmes WHERE IdFilme = @id";
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", idFilme);
            cmd.ExecuteNonQuery();
        }
    }
}


