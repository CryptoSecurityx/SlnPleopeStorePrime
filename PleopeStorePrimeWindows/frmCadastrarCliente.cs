using System;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace PleopeStorePrimeWindows
{
    public partial class frmCadastrarCliente : Form
    {
        public int id = 0;

        public frmCadastrarCliente()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            string mensagem = this.validar();
            
            if (mensagem.Length > 0)
            {
                mensagem = String.Concat("Os campos abaixo são obrigatórios: \n", mensagem);
                MessageBox.Show(mensagem, "Aviso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                decimal? rendaFamiliar = null;
                if (txtRendaFamiliar.Text != "")
                {
                    rendaFamiliar = Convert.ToDecimal(txtRendaFamiliar.Text);
                }

                Model.Cliente cliente = new Model.Cliente();
                cliente.Nome = txtNome.Text;
                cliente.Sobrenome = txtSobrenome.Text;
                cliente.Cpf = mskCPF.Text;
                cliente.Rg = txtRG.Text;
                cliente.Email = txtEmail.Text;
                cliente.DataNascimento = Convert.ToDateTime(mskDataNascimento.Text);
                cliente.Sexo = cboSexo.Text;
                cliente.Isbrasileiro = rdbSim.Checked;
                cliente.RendaFamiliar = rendaFamiliar;
                cliente.QuantidadeFilhos = Convert.ToInt32(nudQtdeFilhos.Value);

                DataAccess.Cliente daCliente = new DataAccess.Cliente();
                bool isOK = daCliente.incluir(cliente);

                if (isOK)
                {
                    MessageBox.Show("Cliente incluído com sucesso!", "Aviso...",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.limparControles();
                }
                else
                {
                    MessageBox.Show("Erro ao incluir cliente!", "Aviso...",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                               
            }
        }

        private void limparControles()
        {
            /*txtNome.Clear();
            txtNome.Text = String.Empty;
            txtNome.Text = string.Empty;*/
            txtNome.Text = "";
            txtSobrenome.Text = "";
            mskCPF.Text = "";
            txtRG.Text = "";
            txtEmail.Text = "";
            mskDataNascimento.Text = "";
            cboSexo.Text = "";
            nudQtdeFilhos.Value = 0;
            txtRendaFamiliar.Text = "";
            rdbSim.Checked = true;
        }

        private bool validarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            
            if (cpf.Length != 11)
                return false;
            
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        private string validar()
        {
            StringBuilder mensagem = new StringBuilder();

            if (txtNome.Text == "")
            {
                mensagem.Append("\nNome;");
            }
            
            if (txtSobrenome.Text == "")
            {
                mensagem.Append("\nSobrenome;");
            }

            if (!mskDataNascimento.MaskCompleted)
            {
                mensagem.Append("\nData Nascimento;");
            }

            if (cboSexo.Text == "")
            {
                mensagem.Append("\nSexo;");
            }

            return mensagem.ToString();
        }

        private void mskDataNascimento_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                MessageBox.Show("Data inválida!", "Aviso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskDataNascimento.Focus();
            }
            else
            {
                DateTime dataNascimento = Convert.ToDateTime(mskDataNascimento.Text);
                if (dataNascimento > System.DateTime.Now)
                {
                    MessageBox.Show("Data de nascimento deve ser menor ou igual a data atual!", "Aviso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskDataNascimento.Focus();
                }
            }
        }

        private void nudQtdeFilhos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void mskCPF_Leave(object sender, EventArgs e)
        {
            mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (mskCPF.Text != "")
            {
                if (!this.validarCPF(mskCPF.Text))
                {
                    MessageBox.Show("CPF inválido!", "Aviso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCPF.Focus();
                }
            }

            mskCPF.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void frmCadastrarCliente_Load(object sender, EventArgs e)
        {
            if (id > 0)
            {
                btnIncluir.Visible = false;
                btnApagar.Visible = true;
                btnAlterar.Visible = true;

                using (SqlConnection conexao = new SqlConnection())
                {
                    string setting = ConfigurationManager.AppSettings["setting1"];
                    string conn = ConfigurationManager.ConnectionStrings["prod"].ConnectionString;

                    using (SqlCommand comando =  new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "select nome,sobrenome,cpf,rg,email,dtnascimento,sexo,isbrasileiro,rendafamiliar,qtdefilhos from cliente where id = @id;";
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        using (SqlDataReader drcliente = comando.ExecuteReader())
                        {
                            if (drcliente.Read())
                            {
                                txtNome.Text = Convert.ToString(drcliente["nome"]);
                                txtSobrenome.Text = Convert.ToString(drcliente["sobrenome"]);
                                mskCPF.Text = Convert.ToString(drcliente["cpf"]);
                                txtRG.Text = Convert.ToString(drcliente["RG"]);
                                txtEmail.Text = Convert.ToString(drcliente["email"]);
                                mskDataNascimento.Text = Convert.ToString(drcliente["dtnascimento"]);
                                cboSexo.Text = Convert.ToString(drcliente["sexo"]);
                                if (Convert.ToInt32(drcliente["isbrasileiro"]) == 1)
                                {
                                    rdbSim.Checked = true;
                                }
                                else
                                {
                                    rdbNao.Checked = true;
                                }

                                txtRendaFamiliar.Text = Convert.ToString(drcliente["rendafamiliar"]);
                                nudQtdeFilhos.Value = Convert.ToInt32(drcliente["qtdefilhos"]);


                            }
                        }
                    }
                }

            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja excluir esse cliente?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Model.Cliente cliente = new Model.Cliente();
                cliente.Id = id;

                DataAccess.Cliente daCliente = new DataAccess.Cliente();
                bool isOK = daCliente.excluir(cliente);

                if (isOK)
                {
                    MessageBox.Show("Cliente excluido com sucesso!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.limparControles();
                    Close();
                }
                else
                {
                    MessageBox.Show("Erro ao Cliente excluir!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string mensagem = this.validar();

            if (mensagem.Length > 0)
            {
                mensagem = String.Concat("Os campos abaixo são obrigatórios: \n", mensagem);
                MessageBox.Show(mensagem, "Aviso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                decimal? rendaFamiliar = null;
                if (txtRendaFamiliar.Text != "")
                {
                    rendaFamiliar = Convert.ToDecimal(txtRendaFamiliar.Text);
                }

                Model.Cliente cliente = new Model.Cliente();
                cliente.Id =  id;
                cliente.Nome = txtNome.Text;
                cliente.Sobrenome = txtSobrenome.Text;
                cliente.Cpf = mskCPF.Text;
                cliente.Rg = txtRG.Text;
                cliente.Email = txtEmail.Text;
                cliente.DataNascimento = Convert.ToDateTime(mskDataNascimento.Text);
                cliente.Sexo = cboSexo.Text;
                cliente.Isbrasileiro = rdbSim.Checked;
                cliente.RendaFamiliar = rendaFamiliar;
                cliente.QuantidadeFilhos = Convert.ToInt32(nudQtdeFilhos.Value);

                DataAccess.Cliente daCliente = new DataAccess.Cliente();
                bool isOK = daCliente.alterar(cliente);

                if (isOK)
                {
                    MessageBox.Show("Cliente alterado!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Erro ao alterar cliente!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
                  
        }
    }
}
