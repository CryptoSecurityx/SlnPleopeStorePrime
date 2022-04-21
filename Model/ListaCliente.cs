using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ListaCliente
    {
        public int Id
        {
            get;
            set;
        }

        private string nome;
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                if (value.Length < 101)
                {
                    nome = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade nome de 100 caracteres foi excedido.");
                }
            }
        }

        private DateTime dataNascimento;
        public DateTime DataNascimento
        {

            get
            {
                return dataNascimento;
            }
            set
            {
                if (value < DateTime.Now.AddDays(1))
                {
                    dataNascimento = value;

                }
                else
                {
                    throw new Exception("A data de nascimento não pode ser maior do que o dia atual.");
                }
            }
        }

        public int Idade
        {
            get
            {
                return Convert.ToInt32(Math.Truncate(DateTime.Now.Subtract(dataNascimento).Days / 365.25));
            }


        }
   

    }
}

    