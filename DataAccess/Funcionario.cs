using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAccess
{
    public class Funcionario
    {
        public bool incluir(Model.Funcionario funcionario)
        {
            bool isOK;
            using (SqlConnection conexao = new SqlConnection())
            {
                string setting = ConfigurationManager.AppSettings["setting1"];
                string conn = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;

                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "insert into funcionario (nome,sobrenome,cpf,rg,datanascimento,sexo,funcao,departamento,salario) values(@nome,@sobrenome,@cpf,@rg,@datanascimento,@sexo,@funcao,@departamento,@salario);";
                    comando.Parameters.Add("@nome", SqlDbType.VarChar, 100).Value = funcionario.Nome;
                    comando.Parameters.Add("@sobrenome", SqlDbType.VarChar, 100).Value = funcionario.Sobrenome;
                    comando.Parameters.Add("@cpf", SqlDbType.Char, 11).Value = funcionario.Cpf;
                    comando.Parameters.Add("@rg", SqlDbType.Char, 50).Value = funcionario.Rg;
                    comando.Parameters.Add("@datanascimento", SqlDbType.Date).Value = funcionario.DataNascimento;
                    comando.Parameters.Add("@sexo", SqlDbType.Char, 1).Value = funcionario.Sexo;
                    comando.Parameters.Add("@funcao", SqlDbType.VarChar, 100).Value = funcionario.Funcao;
                    comando.Parameters.Add("@departamento", SqlDbType.VarChar, 50).Value = funcionario.Departamento;
                    comando.Parameters.Add("@salario", SqlDbType.Decimal).Value = funcionario.Salario;


                     isOK = (comando.ExecuteNonQuery() > 0);

                }
                
            }
            return isOK;
        }
        public bool alterar(Model.Funcionario funcionario)
        {
            bool isOK;
            using (SqlConnection conexao = new SqlConnection())
            {
                conexao.ConnectionString = @"Server=KRYPT0;Database=PeopleStorePrime;Trusted_Connection=True;";
                conexao.Open();

                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "update funcionario set nome = @nome,sobrenome = @sobrenome,cpf = @cpf ,rg = @rg,datanascimento = @datanascimento,sexo = @sexo,funcao = @funcao,departamento = @departamento,salario = @salario where id = @id;";
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = funcionario.Id;
                    comando.Parameters.Add("@nome", SqlDbType.VarChar, 100).Value = funcionario.Nome;
                    comando.Parameters.Add("@sobrenome", SqlDbType.VarChar, 100).Value = funcionario.Sobrenome;
                    comando.Parameters.Add("@cpf", SqlDbType.Char, 11).Value = funcionario.Cpf;
                    comando.Parameters.Add("@rg", SqlDbType.Char, 50).Value = funcionario.Rg;
                    comando.Parameters.Add("@datanascimento", SqlDbType.Date).Value = funcionario.DataNascimento;
                    comando.Parameters.Add("@sexo", SqlDbType.Char, 1).Value = funcionario.Sexo;
                    comando.Parameters.Add("@funcao", SqlDbType.VarChar, 100).Value = funcionario.Funcao;
                    comando.Parameters.Add("@departamento", SqlDbType.VarChar, 50).Value = funcionario.Departamento;
                    comando.Parameters.Add("@salario", SqlDbType.Decimal).Value = funcionario.Salario;

                     isOK = (comando.ExecuteNonQuery() > 0);


                }
            }
            return isOK;
        }
        public bool apagar(Model.Funcionario funcionario)
        {
            bool isOK;
            using (SqlConnection conexao = new SqlConnection())
            {
                conexao.ConnectionString = @"Server=KRYPT0;Database=PeopleStorePrime;Trusted_Connection=True;";
                conexao.Open();

                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "delete from funcionario where id = @id";
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = funcionario.Id;

                    isOK = (comando.ExecuteNonQuery() > 0);


                }
            }
            return isOK;
        }
        public DataTable pesquisar(string nome)
        {
            DataTable dtFuncionario = new DataTable();
            using (SqlConnection conexao = new SqlConnection())
            {
                conexao.ConnectionString = @"Server=KRYPT0;Database=PeopleStorePrime;Trusted_Connection=True;";
                conexao.Open();

                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select id, nome from funcionario";

                    if (nome != "")
                    {
                        comando.CommandText += " where nome like @nome";
                        comando.Parameters.Add("@nome", SqlDbType.VarChar, 100).Value = "%" + nome + "%";
                    }
                    using (SqlDataReader drFuncionario = comando.ExecuteReader())
                    {

                        dtFuncionario.Load(drFuncionario);
                    }
                   
                }
            }
            return dtFuncionario;
        }
    }
}
