using System;

namespace Model
{
    public class Funcionario:Pessoa
    {
       

        private string funcao;
        public string Funcao
        {
            get
            {
                return funcao;
            }
            set
            {
                if (value.Length < 101)
                {
                    funcao = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade funcao de 100 caracteres foi excedido.");
                }
            }
        }

        private string departamento;
        public string Departamento
        {
            get
            {
                return departamento;
            }
            set
            {
                if (value.Length < 51)
                {
                    departamento = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade departamento de 50 caracteres foi excedido.");
                }


            }

        }

        private decimal salario;
        public decimal Salario
        {
            get
            {
                return salario;
            }
            set
            {
                if (value > 0)
                {
                    salario = value;
                }
                else
                {
                    throw new Exception("O valor da propriedade salario não pode ser negativo.");
                }
            }
        }
    }

}
