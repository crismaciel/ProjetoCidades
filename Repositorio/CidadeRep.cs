using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProjetoCidades.Models;

namespace ProjetoCidades.Repositorio
{
    public class CidadeRep
    {

        string connectionString = @"Data source=.\SqlExpress;Initial Catalog=ProjetoCidades;uid=sa;pwd=senai@123";

        public List<Cidade> Listar(){
            List<Cidade> listCidades = new List<Cidade>();

            SqlConnection con = new SqlConnection(connectionString);

            string SqlQuery = "Select * from Cidades";

            SqlCommand cmd = new SqlCommand(SqlQuery,con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            while(sdr.Read()){
                Cidade cidade = new Cidade();

                cidade.Id = Convert.ToInt16(sdr["id"]);
                cidade.Nome = sdr["Nome"].ToString();
                cidade.Estado = sdr["Estado"].ToString();
                cidade.Habitantes = Convert.ToInt16(sdr["Habitantes"]);

                listCidades.Add(cidade);

                }

            con.Close();

            return listCidades;   
        }

        public void Cadastrar(Cidade cidade){
            SqlConnection con = new SqlConnection(connectionString);
            string SqlQuery = "insert into cidades(Nome, Estado, Habitantes) values('"+ cidade.Nome +"','" + cidade.Estado +"'," + cidade.Habitantes + ")";

            
            SqlCommand cmd = new SqlCommand(SqlQuery,con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public string Editar(Cidade cidade){
            SqlConnection con = new SqlConnection(connectionString);
            string msg = "";
            try{
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Updte Cidades set nome = @n, estado = @e, habitantes = @h where id @id";
                cmd.Parameters.AddWithValue("@n", cidade.Nome);
                cmd.Parameters.AddWithValue("@e", cidade.Estado);
                cmd.Parameters.AddWithValue("@h", cidade.Habitantes);
                cmd.Parameters.AddWithValue("@i", cidade.Id);
                con.Open();
                int r = cmd.ExecuteNonQuery();

                if (r>0)
                    msg = "Atualização efetuada";
                else
                    msg = "Não foi possível atualizar";
                cmd.Parameters.Clear();
            }
            
            catch(SqlException se){
                throw new Exception("Erro ao tentar atualizar dados" + se.Message);
            }

            catch(Exception e){
                throw new Exception("Erro inesperado" + e.Message);
            }

            finally{
                con.Close();

            }

           return msg;

            //System.Console.WriteLine(msg);
        }

    }
}