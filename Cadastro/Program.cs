using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Cadastro
{
    class Program
    {
        static void Main(string[] args)
        {
            string nome = "",sobrenome = "",cpf = "",rg = "",sexo = "", data = "",email = "", isbrasileiro = "s", respostaUsuario = "s";
            int Qtdefilhos = 0;
            decimal renda = 0;

            Console.WriteLine("--Cadastro De Cliente--");
            Console.WriteLine();
            do
            {
                Console.Write("Insira um nome: ");
                nome = Console.ReadLine();
            } while (nome == "");

            do
            {
                Console.Write("Insira o Sobrenome: ");
                sobrenome = Console.ReadLine();
            } while (sobrenome == "");


            Console.Write("Insira um CPF: ");
            cpf = Console.ReadLine();

            Console.Write("Insira o RG: ");
            rg = Console.ReadLine();

            do
            {
                Console.Write("Sexo(F/M): ");
                sexo = Console.ReadLine().ToLower();
            } while (sexo != "f" && sexo != "m");

            do
            {
                Console.Write("Insira sua Data de Nascimento: ");
                data = Console.ReadLine();
            } while (data == "");

            Console.Write("Insira Seu Email: ");
            email = Console.ReadLine();

            do
            {
                Console.Write("Brasileiro (Digite S / N):");
                isbrasileiro = Console.ReadLine().ToLower();   
            } while (isbrasileiro != "s" && isbrasileiro != "n");

            int brasileiro = 0;

            if (isbrasileiro == "s")
            {
                brasileiro = 1;
            }
            else
            {
                brasileiro = 0;
            }
                Console.Write("Quantidade de Filhos: ");
                Qtdefilhos = Convert.ToInt32(Console.ReadLine());


            Console.Write("Insira sua Renda: ");
            renda = Convert.ToDecimal(Console.ReadLine());
            do
            {
                Console.Write("Deseja Incluir os dados no Sistema? (S / N)");
                respostaUsuario = Console.ReadLine().ToLower();
            } while (respostaUsuario != "s" && respostaUsuario != "n");

            if (respostaUsuario == "s")
            {
                using (SqlConnection conexao = new SqlConnection())
                {
                   conexao.ConnectionString = @"Server=KRYPT0;Database=PeopleStorePrime;Trusted_Connection=True;";
                   conexao.Open();

                   using (SqlCommand comando = new SqlCommand())
                   {
                       comando.Connection = conexao;
                       comando.CommandText = "insert into cliente (nome,sobrenome,cpf,rg,email,dtnascimento,sexo,isbrasileiro,rendafamiliar,qtdefilhos) values (@nome,@sobrenome,@cpf,@rg,@email,@dtnascimento,@sexo,@isbrasileiro,@rendafamiliar,@qtdefilhos);";
                       comando.Parameters.Add("@nome", SqlDbType.VarChar, 100).Value = nome;
                       comando.Parameters.Add("@sobrenome", SqlDbType.VarChar, 100).Value = sobrenome;
                       comando.Parameters.Add("@cpf", SqlDbType.Char, 11).Value = cpf;
                       comando.Parameters.Add("@rg", SqlDbType.Char, 20).Value = rg;
                       comando.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = email;
                       comando.Parameters.Add("@dtnascimento", SqlDbType.Date).Value = Convert.ToDateTime(data);
                       comando.Parameters.Add("@sexo", SqlDbType.Char, 1).Value = sexo;
                       comando.Parameters.Add("@isbrasileiro", SqlDbType.Bit).Value = brasileiro;
                       comando.Parameters.Add("@rendafamiliar", SqlDbType.Decimal).Value = renda;
                       comando.Parameters.Add("@qtdefilhos", SqlDbType.Int).Value = Qtdefilhos;

                       int linhasAfetadas = comando.ExecuteNonQuery();

                       if (linhasAfetadas > 0)
                       {
                           Console.Write("Cliente Inserido com Sucesso!");
                       }
                       else
                       {
                           Console.Write("Erro ao Incluir o Cliente");
                       }

                   }
                }
            }
            Console.ReadLine();
        }
    }
}
