using System;
using System.Data.SqlClient;
using System.Windows.Forms; // Adicionando dependência para o SqlClient

namespace ACRRentalCarLiveCodingSegunda2020_1
{
    public partial class FrmCadastroCliente : Form
    {
        public FrmCadastroCliente()
        {
            InitializeComponent();
        }


        // Rotina que habilita os controles
        private void Habilitar()
        {
            // txtCódigo sempre desabilitado
            txtCodigo.Enabled = false;

            // habilitando os demais controles
            txtNome.Enabled = true;
            mskCpf.Enabled = true;
            mskDtNasc.Enabled = true;
        }

        //sub-rotina para desabiitar os controles
        private void Desabilitar()
        {
            //txtCodigo sempre será desabilitado
            txtCodigo.Enabled = false;

            //altera a propriedade Enabled dos controles para ficarem desabilitados
            txtNome.Enabled = false;
            mskCpf.Enabled = false;
            mskDtNasc.Enabled = false;
        }

        //sub-rotina para limpar os controles do formulário
        private void LimparControles()
        {
            //desabilita o TextBox
            txtCodigo.Enabled = false;

            //limpa os textos dos TextBox e MaskedTextBox
            txtNome.Clear();
            txtCodigo.Clear();
            mskCpf.Clear();
            mskDtNasc.Clear();
            //coloca o foco no mskCPF
            mskCpf.Focus();
        }

        //Função para validar os campos de entrada.
        //retorna True ou False
        private bool ValidaDados()
        {
            //verificar se mskCPF está preenchido, se não estiver preenchido
            if (string.IsNullOrEmpty(mskCpf.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo de preenchimento obrigatório!",
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o mskCPF
                mskCpf.Clear();

                //coloca o cursor no mskCPF
                mskCpf.Focus();

                //retorna falso
                return false;
            }

            //verifica se o que foi digitado em data de nascimento é uma data válida 
            DateTime auxData; //variável auxiliar
            //se não for uma data válida ou se não digitar nenhuma data
            if (!DateTime.TryParse(mskDtNasc.Text, out auxData))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo de preenchimento obrigatório!", "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o mskDtNasc
                mskDtNasc.Clear();

                //coloca o cursor no mskDtNasc
                mskDtNasc.Focus();

                //retorna falso
                return false;
            }

            //verifica se o txtNome está preenchido, Se for nulo ou vazio retorna falso
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                //mensagem ao usuário
                MessageBox.Show("Campo de preenchimento obrigatório!", "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //limpa o txtNome
                txtNome.Clear();

                //coloca o cursor no txtNome
                txtNome.Focus();

                //retorna falso
                return false;
            }

            //se todas as validações passaram no teste, retorna verdadeiro
            return true;
        }

        private void FrmCadastroCliente_Load(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                //se txtCodigo não estiver vazio, significa que já foi consultado um cliente.
                // a instrução a seguir captura se foi clicado o botão Yes (SIM) como resposta da pergunta.
                if (MessageBox.Show("Você está editando um registro existente. Deseja incluir um registro novo?",
                    "ACR Rental Car", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    LimparControles();
                return; //encerra a sub-rotina
            }

            // antes de incluir é preciso validar os dados de preenchimento obrigatório
            // chama o método para validar a entrada de dados
            // se retornou falso, interrompe o processamento para incluir no banco de dados

            if (ValidaDados() == false) return; //interrompe a sub-rotina

            //declaração da variável para guardar as instruções SQL
            string sqlQuery;

            //cria conexão chamando o método getConnection da classe Conexao
            var conCliente = Conexao.GetConnection();

            //cria a instrução sql, parametrizada
            sqlQuery = "INSERT INTO cliente(nome,data_nasc,cpf) VALUES(@nome,@data_nasc,@cpf)";

            //Tratamento de exceções
            try
            {
                //abre a conexão com o banco de dados
                conCliente.Open();

                //cria um objeto do tipo SqlCommand com a instrução SQL e a conexão
                var cmd = new SqlCommand(sqlQuery, conCliente);

                //define, adiciona os parametros
                cmd.Parameters.Add(new SqlParameter("@nome", txtNome.Text));
                cmd.Parameters.Add(new SqlParameter("@data_nasc", 
                    Convert.ToDateTime(mskDtNasc.Text)));
                cmd.Parameters.Add(new SqlParameter("@cpf", mskCpf.Text));

                //executa o commando
                //ExecuteNonQuery envia instruções para o banco de dados que estão em cmd
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente incluído com sucesso", 
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //Limpa os campos para nova entrada de dados
                LimparControles();
            }
            catch (Exception ex) // se houve alguma exceção dentro do bloco try
            {
                MessageBox.Show("Problema ao incluir cliente " + ex, 
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally // independente se houve exceção ou não o bloco try é sempre executado
            {
                //se conexão não for nula, fecha conexão
                if (conCliente != null) conCliente.Close();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // criar uma instância do formulário de consulta
            Form frm = new FrmConsultaCliente(this);

            // definindo quem será a janela pai do novo form
            frm.MdiParent = MdiParent;

            // Exibir o form:
            frm.Show();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // verificando se há código de usuário preenchido no form
            if (string.IsNullOrEmpty(txtCodigo.Text))
                // caso não haja, não fará nada
                return;

            // verificando se o formulário está devidamente preenchido
            if (ValidaDados() == false)
                // caso não esteja devidamente preenchido, não faça nada
                return;

            // criando a string SQL pra processar a alteração dos dados
            var sqlQuery =
                "UPDATE cliente SET nome=@nome, data_nasc=@data_nasc, cpf=@cpf WHERE id_cliente=@id_cliente";

            // criando conexão com o banco
            var conCliente = Conexao.GetConnection();

            try
            {
                // abrindo a conexão
                conCliente.Open();

                var command = new SqlCommand(sqlQuery, conCliente);

                // fazendo o binding e adicionando ao comando
                command.Parameters.Add(new SqlParameter("@nome", txtNome.Text));
                command.Parameters.Add(
                    new SqlParameter("@data_nasc", 
                        Convert.ToDateTime(txtNome.Text)));
                command.Parameters.Add(new SqlParameter("@cpf", mskCpf.Text));
                command.Parameters.Add(
                    new SqlParameter("@id_cliente", 
                        Convert.ToInt32(txtCodigo.Text)));

                // executar o comando
                command.ExecuteNonQuery();

                // Mostrando janela confirmando cadastro
                MessageBox.Show("Cliente alterado com sucesso",
                    "ACR Rental Car",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                // limpando o form
                LimparControles();
            }
            catch (Exception ex) // se houve alguma exceção dentro do bloco try
            {
                MessageBox.Show("Problema ao alterar cliente " + ex, 
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally // independente se houve exceção ou não o a parte "FINALLY"
                // do bloco try é sempre executado
            {
                //se conexão não for nula, fecha conexão
                if (conCliente != null) conCliente.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // verificando se há código de usuário preenchido no form
            if (string.IsNullOrEmpty(txtCodigo.Text))
                // caso não haja, não fará nada
                return;

            if (MessageBox.Show("Deseja realmente excluir permanentemente o registro?",
                "ACR Rental", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                // criando a string SQL pra processar a alteração dos dados
                var sqlQuery =
                    "DELETE FROM cliente WHERE id_cliente=@id_cliente";

                // criando conexão com o banco
                var conCliente = Conexao.GetConnection();

                try
                {
                    // abrindo a conexão
                    conCliente.Open();

                    var command = new SqlCommand(sqlQuery, conCliente);

                    // fazendo o binding e adicionando ao comando
                    command.Parameters.Add(
                        new SqlParameter("@id_cliente",
                            Convert.ToInt32(txtCodigo.Text)));

                    // executar o comando
                    command.ExecuteNonQuery();

                    // Mostrando janela confirmando cadastro
                    MessageBox.Show("Cliente excluído com sucesso",
                        "ACR Rental Car",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    // limpando o form
                    LimparControles();
                }
                catch (Exception ex) // se houve alguma exceção dentro do bloco try
                {
                    MessageBox.Show("Problema ao excluir o cliente " + ex,
                        "ACR Rental Car", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                finally // independente se houve exceção ou não o a parte "FINALLY"
                    // do bloco try é sempre executado
                {
                    //se conexão não for nula, fecha conexão
                    if (conCliente != null) conCliente.Close();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}