using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public abstract class Pessoa
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

            private string sobrenome;
            public string Sobrenome
            {
                get
                {
                    return sobrenome;
                }
                set
                {
                    if (value.Length < 101)
                    {
                        sobrenome = value;
                    }
                    else
                    {
                        throw new Exception("O tamanho da propriedade sobrenome de 100 caracteres foi excedido.");
                    }
                }
            }

            private string cpf;
            public string Cpf
            {
                get
                {
                    return cpf;
                }
                set
                {
                    if (value.Length < 12)
                    {
                        cpf = value;
                    }
                    else
                    {
                        throw new Exception("O tamanho da propriedade cpf de 11 caracteres foi excedido.");
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

            private string sexo;
            public string Sexo
            {
                get
                {
                    return sexo;
                }
                set
                {
                    if (value.ToUpper() == "F" || value.ToUpper() == "M")
                    {
                        sexo = value;
                    }
                    else
                    {
                        throw new Exception("O sexo deve ser F ou M.");
                    }
                }
            }
   

            private string rg;
            public string Rg
            {
                get
                {
                    return rg;
                }
                set
                {
                    if (value.Length < 51)
                    {
                        rg = value;
                    }
                    else
                    {
                        throw new Exception("O tamanho da propriedade rg de 50 caracteres foi excedido.");
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


