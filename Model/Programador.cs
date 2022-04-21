using System;

namespace Model
{
    public class Programador:Funcionario
    {
      

        private string linguagem;
        public string Linguagem
        {
            get
            {
                return linguagem;
            }
            set
            {
                if(value.Length < 101)
                {
                    linguagem = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade linguagem de 100 caracteres foi excedido.");
                }
            }
        }

        private string experiencia;
        public string Experiencia
        {
            get
            {
                return experiencia;
            }
            set
            {
                if (value.Length < 101)
                {
                    experiencia = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade experiencia de 100 caracteres foi excedido.");
                }


            }
        }

        public bool Certificacao
        {
            get;
            set;
        }
    }
}
