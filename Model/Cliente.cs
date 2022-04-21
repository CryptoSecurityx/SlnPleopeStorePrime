using System;

namespace Model
{
    public class Cliente:Pessoa
    {
       

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (value.Length < 101)
                {
                    email = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade email de 100 caracteres foi excedido.");
                }
            }
        }

  

        public bool Isbrasileiro
        {
            get;
            set;

        }

        private decimal? rendaFamiliar;
        public decimal? RendaFamiliar
        {
            get
            {
                return rendaFamiliar;
            }
            set
            {
                if (value != null)
                {
                    if (value < 0)
                    {
                        throw new Exception("A renda familiar não pode ser negativa");
                    }
                }

                rendaFamiliar = value;
            }
        }

        private int quantidadeFilhos;
        public int QuantidadeFilhos
        {
            get
            {
                return quantidadeFilhos;
            }
            set
            {
                if (value > -1)
                {
                    quantidadeFilhos = value;
                }
                else
                {
                    throw new Exception("A quantidade de filhos deve ser 0 ou superior!");
                }
            }
        }

        
    }
}
