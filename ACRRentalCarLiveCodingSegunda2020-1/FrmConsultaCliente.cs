using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ACRRentalCarLiveCodingSegunda2020_1
{
    public partial class FrmConsultaCliente : Form
    {
        private readonly FrmCadastroCliente cliente;

        // foi adicionado o parâmetro para receber o frmCadastroCliente
        // ao método construtor abaixo:
        public FrmConsultaCliente(FrmCadastroCliente frmCadastroCliente)
        {
            cliente = frmCadastroCliente;
            InitializeComponent();
        }

        private void FrmConsultaCliente_Load(object sender, EventArgs e)
        {
            // criando a string de consulta
            var sqlQuery = "SELECT id_cliente, nome, cpf, data_nasc FROM cliente ORDER BY nome";

            var connection = Conexao.GetConnection();

            // como retornam muitos resultados e precisamos jogá-los em um DataGridView,
            // usamos uma Adapter
            var dataAdapter = new SqlDataAdapter(sqlQuery, connection);

            var dataTable = new DataTable();

            try
            {
                // chamar o dataAdapter para preencher o dataTable:
                dataAdapter.Fill(dataTable);

                // configurar a fonte de dados do DataGridView
                dgvCliente.DataSource = dataTable;


                // formatar o DataGridView para
                // que mostre cores alternadas linha a linha
                dgvCliente.RowsDefaultCellStyle.BackColor = Color.White;
                dgvCliente.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

                dgvCliente.Columns[0].HeaderCell.Value = "Código do cliente";
                dgvCliente.Columns[1].HeaderCell.Value = "Nome";
                dgvCliente.Columns[2].HeaderCell.Value = "CPF";
                dgvCliente.Columns[3].HeaderCell.Value = "Data Nascimento";
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
                if (connection != null) connection.Close();
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            var codigoCliente = dgvCliente.CurrentRow.Cells[0].Value.ToString();


            // criando a string de consulta
            var sqlQuery =
                "SELECT id_cliente, nome, cpf, data_nasc FROM cliente WHERE id_cliente=@id_cliente";

            var connectionC = Conexao.GetConnection();

            // criar um SQLDataReader
            SqlDataReader sqlDataReader = null;


            try
            {
                connectionC.Open();

                var sqlCommand = new SqlCommand(sqlQuery, connectionC);

                sqlCommand.Parameters.Add("@id_cliente",
                    Convert.ToInt32(codigoCliente));

                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    cliente.txtCodigo.Text = sqlDataReader["ID_CLIENTE"].ToString();
                    cliente.txtNome.Text = sqlDataReader["NOME"].ToString();
                    cliente.mskDtNasc.Text = sqlDataReader["DATA_NASC"].ToString();
                    cliente.mskCpf.Text = sqlDataReader["CPF"].ToString();

                    cliente.Focus();
                }
            }
            catch (Exception ex) // se houve alguma exceção dentro do bloco try
            {
                MessageBox.Show("Problema ao carregar dados!" + ex,
                    "ACR Rental Car", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally // independente se houve exceção ou não o a parte "FINALLY"
                // do bloco try é sempre executado
            {
                if (sqlDataReader != null) sqlDataReader.Close();


                //se conexão não for nula, fecha conexão
                if (connectionC != null) connectionC.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}