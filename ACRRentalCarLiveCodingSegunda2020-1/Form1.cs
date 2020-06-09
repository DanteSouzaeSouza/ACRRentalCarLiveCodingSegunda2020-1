using System;
using System.Windows.Forms;

namespace ACRRentalCarLiveCodingSegunda2020_1
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Instanciar um novo formulário de Cadastro de clientes
            Form frm = new FrmCadastroCliente();

            // Definindo qual é o form pai dessa nova janela
            frm.MdiParent = this;

            // Exibindo o formulário
            frm.Show();
        }

        private void veículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Instanciar um novo formulário de Cadastro de clientes
            Form frm = new FrmCadastroVeiculo();

            // Definindo qual é o form pai dessa nova janela
            frm.MdiParent = this;

            // Exibindo o formulário
            frm.Show();
        }
    }
}