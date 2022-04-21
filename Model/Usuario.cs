using System;

namespace Model
{
    public class Usuario
    {
        public int Id
        {
            get;
            set;
        }

        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                if (value.Length < 101)
                {
                    login = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade login de 100 caracteres foi excedido.");
                }
            }
        }

        private string senha;
        public string Senha
        {
            get
            {
                return senha;
            }
            set
            {
                if (value.Length < 101)
                {
                    senha = value;
                }
                else
                {
                    throw new Exception("O tamanho da propriedade senha de 100 caracteres foi excedido.");
                }
            }
        }
    }
}
