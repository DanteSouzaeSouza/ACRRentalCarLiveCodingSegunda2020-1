using System;
using System.Data.SqlClient;
using System.Windows.Forms;

// Adicionando dependência para o SqlClient

namespace ACRRentalCarLiveCodingSegunda2020_1
{
    public partial class FrmCadastroVeiculo : Form
    {
        public FrmCadastroVeiculo()
        {
            InitializeComponent();
        }

        private void Habilitar()
        {
            txtAno.Enabled = true;
            txtCor.Enabled = true;
            txtFabricante.Enabled = true;
            txtPlaca.Enabled = true;
            txtModelo.Enabled = true;
        }

        private void Desabilitar()
        {
            txtAno.Enabled = false;
            txtCor.Enabled = false;
            txtFabricante.Enabled = false;
            txtPlaca.Enabled = false;
            txtModelo.Enabled = false;
        }

        private void LimparControles()
        {
            txtAno.Clear();
            txtCor.Clear();
            txtFabricante.Clear();
            txtPlaca.Clear();
            txtModelo.Clear();
        }


        private bool ValidaDados()
        {
            if (string.IsNullOrEmpty(txtPlaca.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo Placa é de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o controle
                txtPlaca.Clear();

                //coloca o cursor no controle
                txtPlaca.Focus();

                //retorna falso
                return false;
            }

            //declaração da variável para guardar as instruções SQL
            string sqlQuery;

            //cria conexão chamando o método getConnection da classe Conexao
            var conPlaca = Conexao.GetConnection();

            //cria a instrução sql, parametrizada
            sqlQuery = "SELECT placa FROM veiculo WHERE placa = @placa";

            //Tratamento de exceções
            try
            {
                //abre a conexão com o banco de dados
                conPlaca.Open();

                //cria um objeto do tipo SqlCommand com a instrução SQL e a conexão
                var cmd = new SqlCommand(sqlQuery, conPlaca);

                //define, adiciona os parametros
                cmd.Parameters.Add(new SqlParameter("@placa", txtPlaca.Text));

                using (cmd)
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Checando se há dados retornados
                        if (reader.HasRows)
                        {
                            MessageBox.Show("Veículo já cadastrado",
                                "ACR Rental Car", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            //limpa o controle
                            txtPlaca.Clear();

                            //coloca o cursor no controle
                            txtPlaca.Focus();

                            //retorna falso
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex) // se houve alguma exceção dentro do bloco try
            {
                MessageBox.Show("Problema ao carregar dados! " + ex,
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally // independente se houve exceção ou não o a parte "FINALLY"
                // do bloco try é sempre executado
            {
                //se conexão não for nula, fecha conexão
                if (conPlaca != null) conPlaca.Close();
            }


            // verifica se o txtFabricante está preenchido,
            // Se for nulo ou vazio retorna falso
            if (string.IsNullOrEmpty(txtFabricante.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo Fabricante é de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtFabricante.Clear();

                //coloca o cursor no txtNome
                txtFabricante.Focus();

                //retorna falso
                return false;
            }

            // verifica se o txtFabricante está preenchido,
            // Se for nulo ou vazio retorna falso
            if (string.IsNullOrEmpty(txtModelo.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo Modelo é de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtModelo.Clear();

                //coloca o cursor no txtNome
                txtModelo.Focus();

                //retorna falso
                return false;
            }

            int result;
            if (int.TryParse(txtAno.Text, out result) == false)
            {
                //mensagem ao usuário
                MessageBox.Show("Valor inválido para o ano!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtAno.Clear();

                //coloca o cursor no txtNome
                txtAno.Focus();

                //retorna falso
                return false;
            }

            // verifica se o txtFabricante está preenchido,
            // Se for nulo ou vazio retorna falso
            if (string.IsNullOrEmpty(txtCor.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo cor é de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtCor.Clear();

                //coloca o cursor no txtNome
                txtCor.Focus();

                //retorna falso
                return false;
            }

            return true;
        }

        private bool ValidaDadosAlt()
        {
            // verifica se o txtFabricante está preenchido,
            // Se for nulo ou vazio retorna falso
            if (string.IsNullOrEmpty(txtFabricante.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo Fabricante é de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtFabricante.Clear();

                //coloca o cursor no txtNome
                txtFabricante.Focus();

                //retorna falso
                return false;
            }

            // verifica se o txtFabricante está preenchido,
            // Se for nulo ou vazio retorna falso
            if (string.IsNullOrEmpty(txtModelo.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo Modelo é de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtModelo.Clear();

                //coloca o cursor no txtNome
                txtModelo.Focus();

                //retorna falso
                return false;
            }

            int result;
            if (int.TryParse(txtAno.Text, out result) == false)
            {
                //mensagem ao usuário
                MessageBox.Show("Valor inválido para o ano!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtAno.Clear();

                //coloca o cursor no txtNome
                txtAno.Focus();

                //retorna falso
                return false;
            }

            // verifica se o txtFabricante está preenchido,
            // Se for nulo ou vazio retorna falso
            if (string.IsNullOrEmpty(txtCor.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo cor é de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtCor.Clear();

                //coloca o cursor no txtNome
                txtCor.Focus();

                //retorna falso
                return false;
            }

            return true;
        }






        private void btnIncluir_Click(object sender, EventArgs e)
        {
            // antes de incluir é preciso validar os dados de preenchimento obrigatório
            // chama o método para validar a entrada de dados
            // se retornou falso, interrompe o processamento para incluir no banco de dados

            if (ValidaDados() == false) return; //interrompe a sub-rotina

            //declaração da variável para guardar as instruções SQL
            string sqlQuery;

            //cria conexão chamando o método getConnection da classe Conexao
            var conVeiculo = Conexao.GetConnection();

            //cria a instrução sql, parametrizada
            sqlQuery =
                "INSERT INTO veiculo(placa, fabricante, modelo, ano, cor) VALUES(@placa, @fabricante, @modelo, @ano, @cor)";

            //Tratamento de exceções
            try
            {
                //abre a conexão com o banco de dados
                conVeiculo.Open();

                //cria um objeto do tipo SqlCommand com a instrução SQL e a conexão
                var cmd = new SqlCommand(sqlQuery, conVeiculo);

                //define, adiciona os parametros
                cmd.Parameters.Add(new SqlParameter("@placa", txtPlaca.Text));
                cmd.Parameters.Add(new SqlParameter("@fabricante", txtFabricante.Text));
                cmd.Parameters.Add(new SqlParameter("@modelo", txtModelo.Text));
                cmd.Parameters.Add(new SqlParameter("@ano", txtAno.Text));
                cmd.Parameters.Add(new SqlParameter("@cor", txtCor.Text));
                //executa o commando
                //ExecuteNonQuery envia instruções para o banco de dados que estão em cmd
                cmd.ExecuteNonQuery();

                MessageBox.Show("Veículo incluído com sucesso",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //Limpa os campos para nova entrada de dados
                LimparControles();
            }
            catch (Exception ex) // se houve alguma exceção dentro do bloco try
            {
                MessageBox.Show("Problema ao incluir veículo!" + ex,
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally // independente se houve exceção ou não o bloco try é sempre executado
            {
                //se conexão não for nula, fecha conexão
                if (conVeiculo != null) conVeiculo.Close();
            }
        }

        private void FrmCadastroVeiculo_Load(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // criar uma instância do formulário de consulta
            Form frm = new FrmConsultaVeiculo(this);

            // definindo quem será a janela pai do novo form
            frm.MdiParent = MdiParent;

            // Exibir o form:
            frm.Show();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // verificando se há código de usuário preenchido no form
            if (string.IsNullOrEmpty(txtPlaca.Text))
                // caso não haja, não fará nada
                return;

            // verificando se o formulário está devidamente preenchido
            if (ValidaDadosAlt() == false)
                // caso não esteja devidamente preenchido, não faça nada
                return;

            // criando a string SQL pra processar a alteração dos dados
            var sqlQuery =
                "UPDATE veiculo SET placa=@placa, fabricante=@fabricante, modelo=@modelo, ano=@ano, cor=@cor WHERE placa=@placa";

            // criando conexão com o banco
            var conVeiculo = Conexao.GetConnection();

            try
            {
                // abrindo a conexão
                conVeiculo.Open();

                var command = new SqlCommand(sqlQuery, conVeiculo);

                // fazendo o binding e adicionando ao comando
                command.Parameters.Add(new SqlParameter("@placa", txtPlaca.Text));
                command.Parameters.Add(new SqlParameter("@fabricante", txtFabricante.Text));
                command.Parameters.Add(new SqlParameter("@modelo", txtModelo.Text));
                command.Parameters.Add(new SqlParameter("@ano", txtAno.Text));
                command.Parameters.Add(new SqlParameter("@cor", txtCor.Text));

                // executar o comando
                command.ExecuteNonQuery();

                // Mostrando janela confirmando cadastro
                MessageBox.Show("Veículo alterado com sucesso!",
                    "ACR Rental Car",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                // limpando o form
                this.Close();
            }
            catch (Exception ex) // se houve alguma exceção dentro do bloco try
            {
                MessageBox.Show("Problema ao alterar veículo: " + ex,
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally // independente se houve exceção ou não o a parte "FINALLY"
                    // do bloco try é sempre executado
            {
                //se conexão não for nula, fecha conexão
                if (conVeiculo != null) conVeiculo.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // verificando se há código de usuário preenchido no form
            if (string.IsNullOrEmpty(txtPlaca.Text))
                // caso não haja, não fará nada
                return;

            if (MessageBox.Show("Deseja realmente excluir permanentemente o registro?",
                "ACR Rental", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                // criando a string SQL pra processar a alteração dos dados
                var sqlQuery =
                    "DELETE FROM veiculo WHERE placa=@placa";

                // criando conexão com o banco
                var conVeiculo = Conexao.GetConnection();

                try
                {
                    // abrindo a conexão
                    conVeiculo.Open();

                    var command = new SqlCommand(sqlQuery, conVeiculo);

                    // fazendo o binding e adicionando ao comando
                    command.Parameters.Add(
                        new SqlParameter("@placa",txtPlaca.Text));

                    // executar o comando
                    command.ExecuteNonQuery();

                    // Mostrando janela confirmando cadastro
                    MessageBox.Show("Veículo excluído com sucesso!",
                        "ACR Rental Car",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    // limpando o form
                    this.Close();
                }
                catch (Exception ex) // se houve alguma exceção dentro do bloco try
                {
                    MessageBox.Show("Problema ao excluir o veículo: " + ex,
                        "ACR Rental Car", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                finally // independente se houve exceção ou não o a parte "FINALLY"
                        // do bloco try é sempre executado
                {
                    //se conexão não for nula, fecha conexão
                    if (conVeiculo != null) conVeiculo.Close();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}