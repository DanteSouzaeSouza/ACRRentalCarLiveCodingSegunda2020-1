using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private FrmCadastroVeiculo veiculo;

        public FrmConsultaVeiculo(FrmCadastroVeiculo frmCadastroVeiculo)
        {
            veiculo = frmCadastroVeiculo;
            InitializeComponent();
        }
    }
}
