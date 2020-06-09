using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACRRentalCarLiveCodingSegunda2020_1
{
    public partial class FrmConsultaVeiculo : Form
    {
        // Criando uma variável global que receberá o
        // formulário vindo do parâmetro do construtor

        private readonly FrmCadastroVeiculo veiculo;

        public FrmConsultaVeiculo(FrmCadastroVeiculo frmCadastroVeiculo)
        {
            veiculo = frmCadastroVeiculo;
            InitializeComponent();
        }

        private void FrmConsultaVeiculo_Load(object sender, EventArgs e)
        {
            // criando a string de consulta
            var sqlQuery = "SELECT * FROM veiculo ORDER BY placa";

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
                dgvVeiculo.DataSource = dataTable;


                // formatar o DataGridView para
                // que mostre cores alternadas linha a linha
                dgvVeiculo.RowsDefaultCellStyle.BackColor = Color.White;
                dgvVeiculo.AlternatingRowsDefaultCellStyle.BackColor = Color.Aquamarine;

                dgvVeiculo.Columns[0].HeaderCell.Value = "Placa";
                dgvVeiculo.Columns[1].HeaderCell.Value = "Fabricante";
                dgvVeiculo.Columns[2].HeaderCell.Value = "Modelo";
                dgvVeiculo.Columns[3].HeaderCell.Value = "Ano";
                dgvVeiculo.Columns[4].HeaderCell.Value = "Cor";
            }
            catch (Exception ex) // se houve alguma exceção dentro do bloco try
            {
                MessageBox.Show("Problema ao carregar dados: " + ex,
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            var placaVeiculo = dgvVeiculo.CurrentRow.Cells[0].Value.ToString();


            // criando a string de consulta
            var sqlQuery =
                "SELECT * FROM veiculo WHERE placa=@placa";

            var connectionC = Conexao.GetConnection();

            // criar um SQLDataReader
            SqlDataReader sqlDataReader = null;


            try
            {
                connectionC.Open();

                var sqlCommand = new SqlCommand(sqlQuery, connectionC);

                sqlCommand.Parameters.Add("@placa", placaVeiculo);

                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    veiculo.txtPlaca.Text = sqlDataReader["PLACA"].ToString();
                    veiculo.txtFabricante.Text = sqlDataReader["FABRICANTE"].ToString();
                    veiculo.txtModelo.Text = sqlDataReader["MODELO"].ToString();
                    veiculo.txtAno.Text = sqlDataReader["ANO"].ToString();
                    veiculo.txtCor.Text = sqlDataReader["COR"].ToString();

                    veiculo.txtPlaca.Enabled = false;
                    veiculo.Focus();
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
    }
}
