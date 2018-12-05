using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Sigesoft.Node.WinClient.UI.Operations
{
    public partial class frmRecetaMedica : Form
    {
        //private List<DiagnosticRepositoryList> _tmpTotalDiagnosticByServiceIdList = null;
        public frmRecetaMedica(List<DiagnosticRepositoryList> ListaDX)
        {
            InitializeComponent();
            grdTotalDiagnosticos.DataSource = ListaDX;
        }

        private void grd_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void btnAgregarMedicamento_Click(object sender, EventArgs e)
        {
            Operations.frmaddmedicamento frm = new frmaddmedicamento();
            frm.ShowDialog();
        }

        private void btnVerEditarMedicamento_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoverMedicamento_Click(object sender, EventArgs e)
        {

        }

        private void frmRecetaMedica_Load(object sender, EventArgs e)
        {

        }
    }
}
