using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucCuestionarioIstas : UserControl
    {
        bool _isChangueValueControl = false;
        List<ServiceComponentFieldValuesList> _listOfAtencionAdulto1 = new List<ServiceComponentFieldValuesList>();
        ServiceComponentFieldValuesList _UserControlValores = null;

        #region "------------- Public Events -------------"
        /// <summary>
        /// Se desencadena cada vez que se cambia un valor del examen de Audiometria.
        /// </summary>
        public event EventHandler<AudiometriaAfterValueChangeEventArgs> AfterValueChange;
        protected void OnAfterValueChange(AudiometriaAfterValueChangeEventArgs e)
        {
            if (AfterValueChange != null)
                AfterValueChange(this, e);
        }
        #endregion

        #region "--------------- Properties --------------------"
        public string PersonId { get; set; }
        public string ServiceId { get; set; }

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
                SaveValueControlForInterfacingESO(Constants.ValorApartado1, txtValorApartado1.Text);
                SaveValueControlForInterfacingESO(Constants.ValorApartado2, txtValorApartado2.Text);
                SaveValueControlForInterfacingESO(Constants.ValorApartado3, txtValorApartado3.Text);
                SaveValueControlForInterfacingESO(Constants.ValorApartado4, txtValorApartado4.Text);
                SaveValueControlForInterfacingESO(Constants.ValorApartado5, txtValorApartado5.Text);
                SaveValueControlForInterfacingESO(Constants.ValorApartado6, txtValorApartado6.Text);
            
                return _listOfAtencionAdulto1;
            }
            set
            {
                if (value != _listOfAtencionAdulto1)
                {
                    ClearValueControl();
                    _listOfAtencionAdulto1 = value;
                    SearchControlAndFill(value);
                }
            }
        }

        public void ClearValueControl()
        {
            _isChangueValueControl = false;
        }

        public bool IsChangeValueControl { get { return _isChangueValueControl; } }
        #endregion

        public ucCuestionarioIstas()
        {
            InitializeComponent();
        }

        private void ucCuestionarioIstas_Load(object sender, EventArgs e)
        {
            rb_1_4.Name = "N009-IST00000001";
            rb_1_3.Name = "N009-IST00000001";
            rb_1_2.Name = "N009-IST00000001";
            rb_1_1.Name = "N009-IST00000001";
            rb_1_0.Name = "N009-IST00000001";

            rb_2_4.Name = "N009-IST00000002";
            rb_2_3.Name = "N009-IST00000002";
            rb_2_2.Name = "N009-IST00000002";
            rb_2_1.Name = "N009-IST00000002";
            rb_2_0.Name = "N009-IST00000002";

            rb_3_4.Name = "N009-IST00000003";
            rb_3_3.Name = "N009-IST00000003";
            rb_3_2.Name = "N009-IST00000003";
            rb_3_1.Name = "N009-IST00000003";
            rb_3_0.Name = "N009-IST00000003";

            rb_4_4.Name = "N009-IST00000004";
            rb_4_3.Name = "N009-IST00000004";
            rb_4_2.Name = "N009-IST00000004";
            rb_4_1.Name = "N009-IST00000004";
            rb_4_0.Name = "N009-IST00000004";

            rb_5_4.Name = "N009-IST00000005";
            rb_5_3.Name = "N009-IST00000005";
            rb_5_2.Name = "N009-IST00000005";
            rb_5_1.Name = "N009-IST00000005";
            rb_5_0.Name = "N009-IST00000005";

            rb_6_4.Name = "N009-IST00000006";
            rb_6_3.Name = "N009-IST00000006";
            rb_6_2.Name = "N009-IST00000006";
            rb_6_1.Name = "N009-IST00000006";
            rb_6_0.Name = "N009-IST00000006";

            rb_7_4.Name = "N009-IST00000007";
            rb_7_3.Name = "N009-IST00000007";
            rb_7_2.Name = "N009-IST00000007";
            rb_7_1.Name = "N009-IST00000007";
            rb_7_0.Name = "N009-IST00000007";

            rb_8_4.Name = "N009-IST00000008";
            rb_8_3.Name = "N009-IST00000008";
            rb_8_2.Name = "N009-IST00000008";
            rb_8_1.Name = "N009-IST00000008";
            rb_8_0.Name = "N009-IST00000008";

            rb_9_4.Name = "N009-IST00000009";
            rb_9_3.Name = "N009-IST00000009";
            rb_9_2.Name = "N009-IST00000009";
            rb_9_1.Name = "N009-IST00000009";
            rb_9_0.Name = "N009-IST00000009";

            rb_10_4.Name = "N009-IST00000010";
            rb_10_3.Name = "N009-IST00000010";
            rb_10_2.Name = "N009-IST00000010";
            rb_10_1.Name = "N009-IST00000010";
            rb_10_0.Name = "N009-IST00000010";

            rb_11_4.Name = "N009-IST00000011";
            rb_11_3.Name = "N009-IST00000011";
            rb_11_2.Name = "N009-IST00000011";
            rb_11_1.Name = "N009-IST00000011";
            rb_11_0.Name = "N009-IST00000011";

            rb_12_4.Name = "N009-IST00000012";
            rb_12_3.Name = "N009-IST00000012";
            rb_12_2.Name = "N009-IST00000012";
            rb_12_1.Name = "N009-IST00000012";
            rb_12_0.Name = "N009-IST00000012";

            rb_13_4.Name = "N009-IST00000013";
            rb_13_3.Name = "N009-IST00000013";
            rb_13_2.Name = "N009-IST00000013";
            rb_13_1.Name = "N009-IST00000013";
            rb_13_0.Name = "N009-IST00000013";

            rb_14_4.Name = "N009-IST00000014";
            rb_14_3.Name = "N009-IST00000014";
            rb_14_2.Name = "N009-IST00000014";
            rb_14_1.Name = "N009-IST00000014";
            rb_14_0.Name = "N009-IST00000014";

            rb_15_4.Name = "N009-IST00000015";
            rb_15_3.Name = "N009-IST00000015";
            rb_15_2.Name = "N009-IST00000015";
            rb_15_1.Name = "N009-IST00000015";
            rb_15_0.Name = "N009-IST00000015";

            rb_16_4.Name = "N009-IST00000016";
            rb_16_3.Name = "N009-IST00000016";
            rb_16_2.Name = "N009-IST00000016";
            rb_16_1.Name = "N009-IST00000016";
            rb_16_0.Name = "N009-IST00000016";

            rb_17_4.Name = "N009-IST00000017";
            rb_17_3.Name = "N009-IST00000017";
            rb_17_2.Name = "N009-IST00000017";
            rb_17_1.Name = "N009-IST00000017";
            rb_17_0.Name = "N009-IST00000017";

            rb_18_4.Name = "N009-IST00000018";
            rb_18_3.Name = "N009-IST00000018";
            rb_18_2.Name = "N009-IST00000018";
            rb_18_1.Name = "N009-IST00000018";
            rb_18_0.Name = "N009-IST00000018";

            rb_19_4.Name = "N009-IST00000019";
            rb_19_3.Name = "N009-IST00000019";
            rb_19_2.Name = "N009-IST00000019";
            rb_19_1.Name = "N009-IST00000019";
            rb_19_0.Name = "N009-IST00000019";

            rb_20_4.Name = "N009-IST00000020";
            rb_20_3.Name = "N009-IST00000020";
            rb_20_2.Name = "N009-IST00000020";
            rb_20_1.Name = "N009-IST00000020";
            rb_20_0.Name = "N009-IST00000020";

            rb_21_4.Name = "N009-IST00000021";
            rb_21_3.Name = "N009-IST00000021";
            rb_21_2.Name = "N009-IST00000021";
            rb_21_1.Name = "N009-IST00000021";
            rb_21_0.Name = "N009-IST00000021";

            rb_22_4.Name = "N009-IST00000022";
            rb_22_3.Name = "N009-IST00000022";
            rb_22_2.Name = "N009-IST00000022";
            rb_22_1.Name = "N009-IST00000022";
            rb_22_0.Name = "N009-IST00000022";

            rb_23_4.Name = "N009-IST00000023";
            rb_23_3.Name = "N009-IST00000023";
            rb_23_2.Name = "N009-IST00000023";
            rb_23_1.Name = "N009-IST00000023";
            rb_23_0.Name = "N009-IST00000023";

            rb_24_4.Name = "N009-IST00000024";
            rb_24_3.Name = "N009-IST00000024";
            rb_24_2.Name = "N009-IST00000024";
            rb_24_1.Name = "N009-IST00000024";
            rb_24_0.Name = "N009-IST00000024";

            rb_25_4.Name = "N009-IST00000025";
            rb_25_3.Name = "N009-IST00000025";
            rb_25_2.Name = "N009-IST00000025";
            rb_25_1.Name = "N009-IST00000025";
            rb_25_0.Name = "N009-IST00000025";

            rb_26_4.Name = "N009-IST00000026";
            rb_26_3.Name = "N009-IST00000026";
            rb_26_2.Name = "N009-IST00000026";
            rb_26_1.Name = "N009-IST00000026";
            rb_26_0.Name = "N009-IST00000026";

            rb_27_4.Name = "N009-IST00000027";
            rb_27_3.Name = "N009-IST00000027";
            rb_27_2.Name = "N009-IST00000027";
            rb_27_1.Name = "N009-IST00000027";
            rb_27_0.Name = "N009-IST00000027";

            rb_28_4.Name = "N009-IST00000028";
            rb_28_3.Name = "N009-IST00000028";
            rb_28_2.Name = "N009-IST00000028";
            rb_28_1.Name = "N009-IST00000028";
            rb_28_0.Name = "N009-IST00000028";

            rb_29_4.Name = "N009-IST00000029";
            rb_29_3.Name = "N009-IST00000029";
            rb_29_2.Name = "N009-IST00000029";
            rb_29_1.Name = "N009-IST00000029";
            rb_29_0.Name = "N009-IST00000029";

            rb_30_4.Name = "N009-IST00000030";
            rb_30_3.Name = "N009-IST00000030";
            rb_30_2.Name = "N009-IST00000030";
            rb_30_1.Name = "N009-IST00000030";
            rb_30_0.Name = "N009-IST00000030";

            rb_31_4.Name = "N009-IST00000031";
            rb_31_3.Name = "N009-IST00000031";
            rb_31_2.Name = "N009-IST00000031";
            rb_31_1.Name = "N009-IST00000031";
            rb_31_0.Name = "N009-IST00000031";

            rb_32_4.Name = "N009-IST00000032";
            rb_32_3.Name = "N009-IST00000032";
            rb_32_2.Name = "N009-IST00000032";
            rb_32_1.Name = "N009-IST00000032";
            rb_32_0.Name = "N009-IST00000032";

            rb_33_4.Name = "N009-IST00000033";
            rb_33_3.Name = "N009-IST00000033";
            rb_33_2.Name = "N009-IST00000033";
            rb_33_1.Name = "N009-IST00000033";
            rb_33_0.Name = "N009-IST00000033";

            rb_34_4.Name = "N009-IST00000034";
            rb_34_3.Name = "N009-IST00000034";
            rb_34_2.Name = "N009-IST00000034";
            rb_34_1.Name = "N009-IST00000034";
            rb_34_0.Name = "N009-IST00000034";

            rb_35_4.Name = "N009-IST00000035";
            rb_35_3.Name = "N009-IST00000035";
            rb_35_2.Name = "N009-IST00000035";
            rb_35_1.Name = "N009-IST00000035";
            rb_35_0.Name = "N009-IST00000035";

            rb_36_4.Name = "N009-IST00000036";
            rb_36_3.Name = "N009-IST00000036";
            rb_36_2.Name = "N009-IST00000036";
            rb_36_1.Name = "N009-IST00000036";
            rb_36_0.Name = "N009-IST00000036";

            rb_37_4.Name = "N009-IST00000037";
            rb_37_3.Name = "N009-IST00000037";
            rb_37_2.Name = "N009-IST00000037";
            rb_37_1.Name = "N009-IST00000037";
            rb_37_0.Name = "N009-IST00000037";

            rb_38_4.Name = "N009-IST00000038";
            rb_38_3.Name = "N009-IST00000038";
            rb_38_2.Name = "N009-IST00000038";
            rb_38_1.Name = "N009-IST00000038";
            rb_38_0.Name = "N009-IST00000038";

            rb_5_SI.Name = "N009-IST00000045";
            rb_5_NO.Name = "N009-IST00000045";

            txtValorApartado1.Name = "N009-IST00000039";
            txtValorApartado2.Name = "N009-IST00000040";
            txtValorApartado3.Name = "N009-IST00000041";
            txtValorApartado4.Name = "N009-IST00000042";
            txtValorApartado5.Name = "N009-IST00000043";
            txtValorApartado6.Name = "N009-IST00000044";
           

        }

        #region Logic

        private void txtValorApartado1_KeyDown(object sender, KeyEventArgs e)
        {
            int preg1 = rb_1_4.Checked ? 4 : rb_1_3.Checked ? 3 : rb_1_2.Checked ? 2 : rb_1_1.Checked ? 1 : 0;
            int preg2 = rb_2_4.Checked ? 4 : rb_2_3.Checked ? 3 : rb_2_2.Checked ? 2 : rb_2_1.Checked ? 1 : 0;
            int preg3 = rb_3_4.Checked ? 4 : rb_3_3.Checked ? 3 : rb_3_2.Checked ? 2 : rb_3_1.Checked ? 1 : 0;
            int preg4 = rb_4_4.Checked ? 4 : rb_4_3.Checked ? 3 : rb_4_2.Checked ? 2 : rb_4_1.Checked ? 1 : 0;
            int preg5 = rb_5_4.Checked ? 4 : rb_5_3.Checked ? 3 : rb_5_2.Checked ? 2 : rb_5_1.Checked ? 1 : 0;
            int preg6 = rb_6_4.Checked ? 4 : rb_6_3.Checked ? 3 : rb_6_2.Checked ? 2 : rb_6_1.Checked ? 1 : 0;
            txtValorApartado1.Text = (preg1 + preg2 + preg3 + preg4 + preg5 + preg6).ToString();
            lbl_ptje1.Text = txtValorApartado1.Text;
            if (int.Parse(txtValorApartado1.Text) >= 0 && int.Parse(txtValorApartado1.Text) <= 7)
            {
                chk_1_Verde.Checked = true; chk_1_Amarillo.Checked = false; chk_1_Rojo.Checked = false;
                chk_1_Verde.Visible = true; chk_1_Amarillo.Visible = false; chk_1_Rojo.Visible = false;
                
            }
            else if (int.Parse(txtValorApartado1.Text) >= 8 && int.Parse(txtValorApartado1.Text) <= 10)
            {
                chk_1_Amarillo.Checked = true; chk_1_Verde.Checked = false; chk_1_Rojo.Checked = false;
                chk_1_Amarillo.Visible = true; chk_1_Verde.Visible = false; chk_1_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado1.Text) >= 11 && int.Parse(txtValorApartado1.Text) <= 24)
            {
                chk_1_Rojo.Checked = true; chk_1_Verde.Checked = false; chk_1_Amarillo.Checked = false;
                chk_1_Rojo.Visible = true; chk_1_Verde.Visible = false; chk_1_Amarillo.Visible = false;
            }
        }

        private void txtValorApartado2_KeyDown(object sender, KeyEventArgs e)
        {
            int preg7 = rb_7_4.Checked ? 4 : rb_7_3.Checked ? 3 : rb_7_2.Checked ? 2 : rb_7_1.Checked ? 1 : 0;
            int preg8 = rb_8_4.Checked ? 4 : rb_8_3.Checked ? 3 : rb_8_2.Checked ? 2 : rb_8_1.Checked ? 1 : 0;
            int preg9 = rb_9_4.Checked ? 4 : rb_9_3.Checked ? 3 : rb_9_2.Checked ? 2 : rb_9_1.Checked ? 1 : 0;
            int preg10 = rb_10_4.Checked ? 4 : rb_10_3.Checked ? 3 : rb_10_2.Checked ? 2 : rb_10_1.Checked ? 1 : 0;
            int preg11 = rb_11_4.Checked ? 4 : rb_11_3.Checked ? 3 : rb_11_2.Checked ? 2 : rb_11_1.Checked ? 1 : 0;
            int preg12 = rb_12_4.Checked ? 4 : rb_12_3.Checked ? 3 : rb_12_2.Checked ? 2 : rb_12_1.Checked ? 1 : 0;
            int preg13 = rb_13_4.Checked ? 4 : rb_13_3.Checked ? 3 : rb_13_2.Checked ? 2 : rb_13_1.Checked ? 1 : 0;
            int preg14 = rb_14_4.Checked ? 4 : rb_14_3.Checked ? 3 : rb_14_2.Checked ? 2 : rb_14_1.Checked ? 1 : 0;
            int preg15 = rb_15_4.Checked ? 4 : rb_15_3.Checked ? 3 : rb_15_2.Checked ? 2 : rb_15_1.Checked ? 1 : 0;
            int preg16 = rb_16_4.Checked ? 4 : rb_16_3.Checked ? 3 : rb_16_2.Checked ? 2 : rb_16_1.Checked ? 1 : 0;
            txtValorApartado2.Text = (preg7 + preg8 + preg9 + preg10 + preg11 + preg12 + preg13 + preg14 + preg15 + preg16).ToString();
            lbl_ptje2.Text = txtValorApartado2.Text;
            if (int.Parse(txtValorApartado2.Text) >= 26 && int.Parse(txtValorApartado2.Text) <= 40)
            {
                chk_2_Verde.Checked = true; chk_2_Amarillo.Checked = false; chk_2_Rojo.Checked = false;
                chk_2_Verde.Visible = true; chk_2_Amarillo.Visible = false; chk_2_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado2.Text) >= 21 && int.Parse(txtValorApartado2.Text) <= 25)
            {
                chk_2_Amarillo.Checked = true; chk_2_Verde.Checked = false; chk_2_Rojo.Checked = false;
                chk_2_Amarillo.Visible = true; chk_2_Verde.Visible = false; chk_2_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado2.Text) >= 0 && int.Parse(txtValorApartado2.Text) <= 20)
            {
                chk_2_Rojo.Checked = true; chk_2_Verde.Checked = false; chk_2_Amarillo.Checked = false;
                chk_2_Rojo.Visible = true; chk_2_Verde.Visible = false; chk_2_Amarillo.Visible = false;
            }
        }

        private void txtValorApartado3_KeyDown(object sender, KeyEventArgs e)
        {
            int preg17 = rb_17_4.Checked ? 4 : rb_17_3.Checked ? 3 : rb_17_2.Checked ? 2 : rb_17_1.Checked ? 1 : 0;
            int preg18 = rb_18_4.Checked ? 4 : rb_18_3.Checked ? 3 : rb_18_2.Checked ? 2 : rb_18_1.Checked ? 1 : 0;
            int preg19 = rb_19_4.Checked ? 4 : rb_19_3.Checked ? 3 : rb_19_2.Checked ? 2 : rb_19_1.Checked ? 1 : 0;
            int preg20 = rb_20_4.Checked ? 4 : rb_20_3.Checked ? 3 : rb_20_2.Checked ? 2 : rb_20_1.Checked ? 1 : 0;
            txtValorApartado3.Text = (preg17 + preg18 + preg19 + preg20).ToString();
            lbl_ptje3.Text = txtValorApartado3.Text;
            if (int.Parse(txtValorApartado3.Text) >= 0 && int.Parse(txtValorApartado3.Text) <= 1)
            {
                chk_3_Verde.Checked = true; chk_3_Amarillo.Checked = false; chk_3_Rojo.Checked = false;
                chk_3_Verde.Visible = true; chk_3_Amarillo.Visible = false; chk_3_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado3.Text) >= 2 && int.Parse(txtValorApartado3.Text) <= 5)
            {
                chk_3_Amarillo.Checked = true; chk_3_Verde.Checked = false; chk_3_Rojo.Checked = false;
                chk_3_Amarillo.Visible = true; chk_3_Verde.Visible = false; chk_3_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado3.Text) >= 6 && int.Parse(txtValorApartado3.Text) <= 20)
            {
                chk_3_Rojo.Checked = true; chk_3_Verde.Checked = false; chk_3_Amarillo.Checked = false;
                chk_3_Rojo.Visible = true; chk_3_Verde.Visible = false; chk_3_Amarillo.Visible = false;
            }
        }

        private void txtValorApartado4_KeyDown(object sender, KeyEventArgs e)
        {
            int preg21 = rb_21_4.Checked ? 4 : rb_21_3.Checked ? 3 : rb_21_2.Checked ? 2 : rb_21_1.Checked ? 1 : 0;
            int preg22 = rb_22_4.Checked ? 4 : rb_22_3.Checked ? 3 : rb_22_2.Checked ? 2 : rb_22_1.Checked ? 1 : 0;
            int preg23 = rb_23_4.Checked ? 4 : rb_23_3.Checked ? 3 : rb_23_2.Checked ? 2 : rb_23_1.Checked ? 1 : 0;
            int preg24 = rb_24_4.Checked ? 4 : rb_24_3.Checked ? 3 : rb_24_2.Checked ? 2 : rb_24_1.Checked ? 1 : 0;
            int preg25 = rb_25_4.Checked ? 4 : rb_25_3.Checked ? 3 : rb_25_2.Checked ? 2 : rb_25_1.Checked ? 1 : 0;
            int preg26 = rb_26_4.Checked ? 4 : rb_26_3.Checked ? 3 : rb_26_2.Checked ? 2 : rb_26_1.Checked ? 1 : 0;
            int preg27 = rb_27_4.Checked ? 4 : rb_27_3.Checked ? 3 : rb_27_2.Checked ? 2 : rb_27_1.Checked ? 1 : 0;
            int preg28 = rb_28_4.Checked ? 4 : rb_28_3.Checked ? 3 : rb_28_2.Checked ? 2 : rb_28_1.Checked ? 1 : 0;
            int preg29 = rb_29_4.Checked ? 4 : rb_29_3.Checked ? 3 : rb_29_2.Checked ? 2 : rb_29_1.Checked ? 1 : 0;
            int preg30 = rb_30_4.Checked ? 4 : rb_30_3.Checked ? 3 : rb_30_2.Checked ? 2 : rb_30_1.Checked ? 1 : 0;
            txtValorApartado4.Text = (preg21 + preg22 + preg23 + preg24 + preg25 + preg26 + preg27 + preg28 + preg29 + preg30).ToString();
            lbl_ptje4.Text = txtValorApartado4.Text;
            if (int.Parse(txtValorApartado4.Text) >= 29 && int.Parse(txtValorApartado4.Text) <= 40)
            {
                chk_4_Verde.Checked = true; chk_4_Amarillo.Checked = false; chk_4_Rojo.Checked = false;
                chk_4_Verde.Visible = true; chk_4_Amarillo.Visible = false; chk_4_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado4.Text) >= 24 && int.Parse(txtValorApartado4.Text) <= 28)
            {
                chk_4_Amarillo.Checked = true; chk_4_Verde.Checked = false; chk_4_Rojo.Checked = false;
                chk_4_Amarillo.Visible = true; chk_4_Verde.Visible = false; chk_4_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado4.Text) >= 0 && int.Parse(txtValorApartado4.Text) <= 23)
            {
                chk_4_Rojo.Checked = true; chk_4_Verde.Checked = false; chk_4_Amarillo.Checked = false;
                chk_4_Rojo.Visible = true; chk_4_Verde.Visible = false; chk_4_Amarillo.Visible = false;
            }
        }

        private void txtValorApartado5_KeyDown(object sender, KeyEventArgs e)
        {
            if (rb_5_NO.Checked)
            {
                int preg31 = rb_31_4.Checked ? 4 : rb_31_3.Checked ? 3 : rb_31_2.Checked ? 2 : rb_31_1.Checked ? 1 : 0;
                int preg32 = rb_32_4.Checked ? 4 : rb_32_3.Checked ? 3 : rb_32_2.Checked ? 2 : rb_32_1.Checked ? 1 : 0;
                int preg33 = rb_33_4.Checked ? 4 : rb_33_3.Checked ? 3 : rb_33_2.Checked ? 2 : rb_33_1.Checked ? 1 : 0;
                int preg34 = rb_34_4.Checked ? 4 : rb_34_3.Checked ? 3 : rb_34_2.Checked ? 2 : rb_34_1.Checked ? 1 : 0;
                txtValorApartado5.Text = (preg31 + preg32 + preg33 + preg34).ToString();
                lbl_ptje5.Text = txtValorApartado5.Text;
                if (int.Parse(txtValorApartado5.Text) >= 0 && int.Parse(txtValorApartado5.Text) <= 3)
                {
                    chk_5_Verde.Checked = true; chk_5_Amarillo.Checked = false; chk_5_Rojo.Checked = false;
                    chk_5_Verde.Visible = true; chk_5_Amarillo.Visible = false; chk_5_Rojo.Visible = false;
                }
                else if (int.Parse(txtValorApartado5.Text) >= 4 && int.Parse(txtValorApartado5.Text) <= 6)
                {
                    chk_5_Amarillo.Checked = true; chk_5_Verde.Checked = false; chk_5_Rojo.Checked = false;
                    chk_5_Amarillo.Visible = true; chk_5_Verde.Visible = false; chk_5_Rojo.Visible = false;
                }
                else if (int.Parse(txtValorApartado5.Text) >= 7 && int.Parse(txtValorApartado5.Text) <= 16)
                {
                    chk_5_Rojo.Checked = true; chk_5_Verde.Checked = false; chk_5_Amarillo.Checked = false;
                    chk_5_Rojo.Visible = true; chk_5_Verde.Visible = false; chk_5_Amarillo.Visible = false;
                }
            }

        }

        private void txtValorApartado6_KeyDown(object sender, KeyEventArgs e)
        {
            int preg35 = rb_35_4.Checked ? 4 : rb_35_3.Checked ? 3 : rb_35_2.Checked ? 2 : rb_35_1.Checked ? 1 : 0;
            int preg36 = rb_36_4.Checked ? 4 : rb_36_3.Checked ? 3 : rb_36_2.Checked ? 2 : rb_36_1.Checked ? 1 : 0;
            int preg37 = rb_37_4.Checked ? 0 : rb_37_3.Checked ? 1 : rb_37_2.Checked ? 2 : rb_37_1.Checked ? 3 : 4;
            int preg38 = rb_38_4.Checked ? 4 : rb_38_3.Checked ? 3 : rb_38_2.Checked ? 2 : rb_38_1.Checked ? 1 : 0;
            txtValorApartado6.Text = (preg35 + preg36 + preg37 + preg38).ToString();
            lbl_ptje6.Text = txtValorApartado6.Text;
            if (int.Parse(txtValorApartado6.Text) >= 13 && int.Parse(txtValorApartado6.Text) <= 16)
            {
                chk_6_Verde.Checked = true; chk_6_Amarillo.Checked = false; chk_6_Rojo.Checked = false;
                chk_6_Verde.Visible = true; chk_6_Amarillo.Visible = false; chk_6_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado6.Text) >= 11 && int.Parse(txtValorApartado6.Text) <= 12)
            {
                chk_6_Amarillo.Checked = true; chk_6_Verde.Checked = false; chk_6_Rojo.Checked = false;
                chk_6_Amarillo.Visible = true; chk_6_Verde.Visible = false; chk_6_Rojo.Visible = false;
            }
            else if (int.Parse(txtValorApartado6.Text) >= 0 && int.Parse(txtValorApartado6.Text) <= 10)
            {
                chk_6_Rojo.Checked = true; chk_6_Verde.Checked = false; chk_6_Amarillo.Checked = false;
                chk_6_Rojo.Visible = true; chk_6_Verde.Visible = false; chk_6_Amarillo.Visible = false;
            }
        }

        private void rb_5_SI_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_5_SI.Checked)
            {
                gb_5_1.Enabled = false; gb_5_2.Enabled = false;
            }
            if (rb_5_SI.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.Apartado5_SINO, "1");
            }
        }

        private void rb_5_NO_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_5_NO.Checked)
            {
                gb_5_1.Enabled = true; gb_5_2.Enabled = true;
            }
            if (rb_5_NO.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.Apartado5_SINO, "0");
            }
        }

        #endregion

        private void lbl_Leave(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;

            SaveValueControlForInterfacingESO(senderCtrl.Name, senderCtrl.Text.ToString());

            _isChangueValueControl = true;
        }

        private void SaveValueControlForInterfacingESO(string name, string value)
        {
            #region Capturar Valor del campo

            _listOfAtencionAdulto1.RemoveAll(p => p.v_ComponentFieldId == name);

            _UserControlValores = new ServiceComponentFieldValuesList();

            _UserControlValores.v_ComponentFieldId = name;
            _UserControlValores.v_Value1 = value;
            _UserControlValores.v_ComponentId = Constants.CUESTIONARIO_ISTAS_2;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        private void SearchControlAndFill(List<ServiceComponentFieldValuesList> DataSource)
        {
            if (DataSource == null || DataSource.Count == 0) return;
            // Ordenar Lista Datasource
            var DataSourceOrdenado = DataSource.OrderBy(p => p.v_ComponentFieldId).ToList();

            // recorrer la lista que viene de la BD
            foreach (var item in DataSourceOrdenado)
            {
                var matchedFields = this.Controls.Find(item.v_ComponentFieldId, true);

                if (matchedFields.Length > 0)
                {
                    var field = matchedFields[0];

                    if (field is TextBox)
                    {
                        if (field.Name == item.v_ComponentFieldId)
                        {
                            ((TextBox)field).Text = item.v_Value1;
                        }
                        if (item.v_ComponentFieldId == Constants.ValorApartado1)
                        {
                            if (item.v_Value1 != "" || item.v_Value1 != null)
                            {
                                lbl_ptje1.Text = item.v_Value1;
                            }
                        }
                        if (item.v_ComponentFieldId == Constants.ValorApartado2)
                        {
                            if (item.v_Value1 != "" || item.v_Value1 != null)
                            {
                                lbl_ptje2.Text = item.v_Value1;
                            }
                        }
                        if (item.v_ComponentFieldId == Constants.ValorApartado3)
                        {
                            if (item.v_Value1 != "" || item.v_Value1 != null)
                            {
                                lbl_ptje3.Text = item.v_Value1;
                            }
                        }
                        if (item.v_ComponentFieldId == Constants.ValorApartado4)
                        {
                            if (item.v_Value1 != "" || item.v_Value1 != null)
                            {
                                lbl_ptje4.Text = item.v_Value1;
                            }
                        }
                        if (item.v_ComponentFieldId == Constants.ValorApartado5)
                        {
                            if (item.v_Value1 != "" || item.v_Value1 != null)
                            {
                                lbl_ptje5.Text = item.v_Value1;
                            }
                        }
                        if (item.v_ComponentFieldId == Constants.ValorApartado6)
                        {
                            if (item.v_Value1 != "" || item.v_Value1 != null)
                            {
                                lbl_ptje6.Text = item.v_Value1;
                            }
                        }
                    }

                    else if (field is RadioButton)
                    {
                        if (field.Name == item.v_ComponentFieldId)
                        {
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_1)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_1_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_1_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_1_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_1_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_1_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_2)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_2_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_2_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_2_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_2_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_2_4.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.PREGUNTA_3)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_3_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_3_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_3_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_3_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_3_4.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.PREGUNTA_4)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_4_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_4_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_4_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_4_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_4_4.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.PREGUNTA_5)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_5_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_5_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_5_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_5_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_5_4.Checked = true;
                                }
                            }

                            if (item.v_ComponentFieldId == Constants.PREGUNTA_6)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_6_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_6_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_6_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_6_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_6_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_7)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_7_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_7_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_7_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_7_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_7_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_8)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_8_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_8_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_8_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_8_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_8_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_9)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_9_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_9_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_9_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_9_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_9_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_10)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_10_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_10_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_10_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_10_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_10_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_11)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_11_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_11_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_11_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_11_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_11_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_12)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_12_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_12_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_12_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_12_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_12_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_13)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_13_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_13_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_13_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_13_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_13_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_14)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_14_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_14_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_14_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_14_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_14_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_15)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_15_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_15_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_15_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_15_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_15_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_16)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_16_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_16_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_16_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_16_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_16_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_17)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_17_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_17_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_17_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_17_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_17_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_18)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_18_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_18_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_18_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_18_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_18_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_19)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_19_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_19_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_19_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_19_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_19_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_20)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_20_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_20_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_20_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_20_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_20_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_21)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_21_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_21_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_21_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_21_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_21_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_22)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_22_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_22_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_22_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_22_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_22_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_23)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_23_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_23_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_23_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_23_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_23_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_24)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_24_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_24_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_24_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_24_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_24_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_25)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_25_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_25_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_25_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_25_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_25_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_26)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_26_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_26_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_26_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_26_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_26_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_27)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_27_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_27_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_27_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_27_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_27_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_28)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_28_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_28_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_28_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_28_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_28_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_29)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_29_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_29_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_29_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_29_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_29_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_30)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_30_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_30_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_30_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_30_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_30_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_31)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_31_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_31_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_31_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_31_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_31_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_32)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_32_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_32_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_32_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_32_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_32_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_33)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_33_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_33_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_33_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_33_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_33_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_34)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_34_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_34_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_34_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_34_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_34_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_35)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_35_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_35_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_35_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_35_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_35_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_36)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_36_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_36_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_36_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_36_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_36_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_37)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_37_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_37_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_37_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_37_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_37_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.PREGUNTA_38)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_38_0.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_38_1.Checked = true;
                                }
                                else if (item.v_Value1 == "2")
                                {
                                    rb_38_2.Checked = true;
                                }
                                else if (item.v_Value1 == "3")
                                {
                                    rb_38_3.Checked = true;
                                }
                                else if (item.v_Value1 == "4")
                                {
                                    rb_38_4.Checked = true;
                                }
                            }
                            if (item.v_ComponentFieldId == Constants.Apartado5_SINO)
                            {
                                if (item.v_Value1 == "0")
                                {
                                    rb_5_NO.Checked = true;
                                }
                                else if (item.v_Value1 == "1")
                                {
                                    rb_5_SI.Checked = true;
                                }
                                
                            }
                           
                           
                        }
                    }
                }
            }
        }

        #region Events


        private void rb_1_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_1, "4");
            }
        }
        private void rb_1_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_1, "3");
            }
        }
        private void rb_1_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_1, "2");
            }
        }
        private void rb_1_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_1, "1");
            }
        }
        private void rb_1_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_1, "0");
            }
        }

        private void rb_2_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_2_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_2, "4");
            }

        }
        private void rb_2_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_2_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_2, "3");
            }

        }
        private void rb_2_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_2_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_2, "2");
            }
        }
        private void rb_2_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_2_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_2, "1");
            }
        }
        private void rb_2_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_2_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_2, "0");
            }
        }
        private void rb_3_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_3_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_3, "4");
            }
        }
        private void rb_3_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_3_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_3, "3");
            }
        }
        private void rb_3_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_3_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_3, "2");
            }
        }
        private void rb_3_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_3_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_3, "1");
            }
        }
        private void rb_3_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_3_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_3, "0");
            }
        }
        private void rb_4_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_4_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_4, "4");
            }
        }
        private void rb_4_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_4_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_4, "3");
            }
        }
        private void rb_4_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_4_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_4, "2");
            }
        }
        private void rb_4_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_4_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_4, "1");
            }
        }
        private void rb_4_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_4_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_4, "0");
            }
        }
        private void rb_5_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_5_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_5, "4");
            }
        }
        private void rb_5_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_5_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_5, "3");
            }
        }
        private void rb_5_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_5_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_5, "2");
            }
        }
        private void rb_5_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_5_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_5, "1");
            }
        }
        private void rb_5_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_5_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_5, "0");
            }
        }
        private void rb_6_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_6_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_6, "4");
            }
        }
        private void rb_6_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_6_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_6, "3");
            }
        }
        private void rb_6_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_6_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_6, "2");
            }
        }
        private void rb_6_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_6_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_6, "1");
            }
        }
        private void rb_6_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_6_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_6, "0");
            }
        }
        private void rb_7_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_7_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_7, "4");
            }
        }
        private void rb_7_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_7_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_7, "3");
            }
        }

        private void rb_7_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_7_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_7, "2");
            }
        }

        private void rb_7_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_7_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_7, "1");
            }
        }

        private void rb_7_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_7_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_7, "0");
            }
        }

        private void rb_8_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_8_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_8, "4");
            }
        }

        private void rb_8_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_8_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_8, "3");
            }
        }

        private void rb_8_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_8_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_8, "2");
            }
        }

        private void rb_8_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_8_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_8, "1");
            }
        }

        private void rb_8_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_8_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_8, "0");
            }
        }

        private void rb_9_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_9_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_9, "4");
            }
        }

        private void rb_9_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_9_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_9, "3");
            }
        }

        private void rb_9_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_9_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_9, "2");
            }
        }

        private void rb_9_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_9_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_9, "1");
            }
        }

        private void rb_9_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_9_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_9, "0");
            }
        }

        private void rb_10_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_10_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_10, "4");
            }
        }

        private void rb_10_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_10_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_10, "3");
            }
        }

        private void rb_10_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_10_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_10, "2");
            }
        }

        private void rb_10_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_10_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_10, "1");
            }
        }

        private void rb_10_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_10_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_10, "0");
            }
        }

        private void rb_11_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_11_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_11, "4");
            }
        }

        private void rb_11_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_11_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_11, "3");
            }
        }

        private void rb_11_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_11_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_11, "2");
            }
        }

        private void rb_11_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_11_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_11, "1");
            }
        }

        private void rb_11_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_11_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_11, "0");
            }
        }

        private void rb_12_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_12_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_12, "4");
            }
        }

        private void rb_12_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_12_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_12, "3");
            }
        }

        private void rb_12_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_12_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_12, "2");
            }
        }

        private void rb_12_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_12_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_12, "1");
            }
        }

        private void rb_12_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_12_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_12, "0");
            }
        }

        private void rb_13_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_13_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_13, "4");
            }
        }

        private void rb_13_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_13_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_13, "3");
            }
        }

        private void rb_13_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_13_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_13, "2");
            }
        }

        private void rb_13_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_13_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_13, "1");
            }
        }

        private void rb_13_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_13_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_13, "0");
            }
        }

        private void rb_14_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_14_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_14, "4");
            }
        }

        private void rb_14_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_14_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_14, "3");
            }
        }

        private void rb_14_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_14_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_14, "2");
            }
        }

        private void rb_14_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_14_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_14, "1");
            }
        }

        private void rb_14_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_14_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_14, "0");
            }
        }

        private void rb_15_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_15_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_15, "4");
            }
        }

        private void rb_15_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_15_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_15, "3");
            }
        }

        private void rb_15_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_15_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_15, "2");
            }
        }

        private void rb_15_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_15_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_15, "1");
            }
        }

        private void rb_15_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_15_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_15, "0");
            }
        }

        private void rb_16_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_16_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_16, "4");
            }
        }

        private void rb_16_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_16_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_16, "3");
            }
        }

        private void rb_16_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_16_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_16, "2");
            }
        }

        private void rb_16_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_16_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_16, "1");
            }
        }

        private void rb_16_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_16_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_16, "0");
            }
        }

        private void rb_17_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_17_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_17, "4");
            }
        }

        private void rb_17_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_17_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_17, "3");
            }
        }

        private void rb_17_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_17_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_17, "2");
            }
        }

        private void rb_17_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_17_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_17, "1");
            }
        }

        private void rb_17_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_17_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_17, "0");
            }
        }

        private void rb_18_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_18_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_18, "4");
            }
        }

        private void rb_18_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_18_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_18, "3");
            }
        }

        private void rb_18_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_18_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_18, "2");
            }
        }

        private void rb_18_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_18_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_18, "1");
            }
        }

        private void rb_18_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_18_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_18, "0");
            }
        }

        private void rb_19_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_19_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_19, "4");
            }
        }

        private void rb_19_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_19_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_19, "3");
            }
        }

        private void rb_19_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_19_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_19, "2");
            }
        }

        private void rb_19_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_19_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_19, "1");
            }
        }

        private void rb_19_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_19_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_19, "0");
            }
        }

        private void rb_20_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_20_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_20, "4");
            }
        }

        private void rb_20_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_20_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_20, "3");
            }
        }

        private void rb_20_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_20_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_20, "2");
            }
        }

        private void rb_20_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_20_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_20, "1");
            }
        }

        private void rb_20_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_20_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_20, "0");
            }
        }

        private void rb_21_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_21_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_21, "4");
            }
        }

        private void rb_21_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_21_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_21, "3");
            }
        }

        private void rb_21_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_21_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_21, "2");
            }
        }

        private void rb_21_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_21_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_21, "1");
            }
        }

        private void rb_21_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_21_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_21, "0");
            }
        }

        private void rb_22_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_22_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_22, "4");
            }
        }

        private void rb_22_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_22_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_22, "3");
            }
        }

        private void rb_22_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_22_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_22, "2");
            }
        }

        private void rb_22_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_22_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_22, "1");
            }
        }

        private void rb_22_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_22_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_22, "0");
            }
        }

        private void rb_23_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_23_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_23, "4");
            }
        }

        private void rb_23_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_23_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_23, "3");
            }
        }

        private void rb_23_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_23_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_23, "2");
            }
        }

        private void rb_23_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_23_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_23, "1");
            }
        }

        private void rb_23_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_23_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_23, "0");
            }
        }

        private void rb_24_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_24_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_24, "4");
            }
        }

        private void rb_24_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_24_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_24, "3");
            }
        }

        private void rb_24_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_24_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_24, "2");
            }
        }

        private void rb_24_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_24_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_24, "1");
            }
        }

        private void rb_24_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_24_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_24, "0");
            }
        }

        private void rb_25_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_25_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_25, "4");
            }
        }

        private void rb_25_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_25_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_25, "3");
            }
        }

        private void rb_25_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_25_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_25, "2");
            }
        }

        private void rb_25_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_25_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_25, "1");
            }
        }

        private void rb_25_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_25_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_25, "0");
            }
        }

        private void rb_26_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_26_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_26, "4");
            }
        }

        private void rb_26_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_26_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_26, "3");
            }
        }

        private void rb_26_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_26_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_26, "2");
            }
        }

        private void rb_26_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_26_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_26, "1");
            }
        }

        private void rb_26_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_26_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_26, "0");
            }
        }

        private void rb_27_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_27_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_27, "4");
            }
        }

        private void rb_27_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_27_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_27, "3");
            }
        }

        private void rb_27_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_27_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_27, "2");
            }
        }

        private void rb_27_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_27_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_27, "1");
            }
        }

        private void rb_27_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_27_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_27, "0");
            }
        }

        private void rb_28_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_28_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_28, "4");
            }
        }

        private void rb_28_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_28_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_28, "3");
            }
        }

        private void rb_28_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_28_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_28, "2");
            }
        }

        private void rb_28_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_28_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_28, "1");
            }
        }

        private void rb_28_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_28_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_28, "0");
            }
        }

        private void rb_29_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_29_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_29, "4");
            }
        }

        private void rb_29_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_29_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_29, "3");
            }
        }

        private void rb_29_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_29_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_29, "2");
            }
        }

        private void rb_29_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_29_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_29, "1");
            }
        }

        private void rb_29_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_29_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_29, "0");
            }
        }

        private void rb_30_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_30_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_30, "4");
            }
        }

        private void rb_30_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_30_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_30, "3");
            }
        }

        private void rb_30_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_30_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_30, "2");
            }
        }

        private void rb_30_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_30_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_30, "1");
            }
        }

        private void rb_30_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_30_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_30, "0");
            }
        }

        private void rb_31_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_31_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_31, "4");
            }
        }

        private void rb_31_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_31_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_31, "3");
            }
        }

        private void rb_31_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_31_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_31, "2");
            }
        }

        private void rb_31_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_31_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_31, "1");
            }
        }

        private void rb_31_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_31_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_31, "0");
            }
        }

        private void rb_32_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_32_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_32, "4");
            }
        }

        private void rb_32_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_32_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_32, "3");
            }
        }

        private void rb_32_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_32_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_32, "2");
            }
        }

        private void rb_32_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_32_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_32, "1");
            }
        }

        private void rb_32_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_32_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_32, "0");
            }
        }

        private void rb_33_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_33_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_33, "4");
            }
        }

        private void rb_33_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_33_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_33, "3");
            }
        }

        private void rb_33_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_33_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_33, "2");
            }
        }

        private void rb_33_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_33_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_33, "1");
            }
        }

        private void rb_33_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_33_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_33, "0");
            }
        }

        private void rb_34_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_34_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_34, "4");
            }
        }

        private void rb_34_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_34_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_34, "3");
            }
        }

        private void rb_34_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_34_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_34, "2");
            }
        }

        private void rb_34_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_34_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_34, "1");
            }
        }

        private void rb_34_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_34_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_34, "0");
            }
        }

        private void rb_35_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_35_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_35, "4");
            }
        }

        private void rb_35_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_35_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_35, "3");
            }
        }

        private void rb_35_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_35_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_35, "2");
            }
        }

        private void rb_35_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_35_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_35, "1");
            }
        }

        private void rb_35_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_35_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_35, "0");
            }
        }

        private void rb_36_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_36_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_36, "4");
            }
        }

        private void rb_36_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_36_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_36, "3");
            }
        }

        private void rb_36_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_36_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_36, "2");
            }
        }

        private void rb_36_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_36_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_36, "1");
            }
        }

        private void rb_36_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_36_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_36, "0");
            }
        }

        private void rb_37_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_37_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_37, "0");
            }
        }

        private void rb_37_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_37_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_37, "1");
            }
        }

        private void rb_37_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_37_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_37, "2");
            }
        }

        private void rb_37_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_37_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_37, "3");
            }
        }

        private void rb_37_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_37_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_37, "4");
            }
        }

        private void rb_38_4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_38_4.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_38, "4");
            }
        }

        private void rb_38_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_38_3.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_38, "3");
            }
        }

        private void rb_38_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_38_2.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_38, "2");
            }
        }

        private void rb_38_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_38_1.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_38, "1");
            }
        }

        private void rb_38_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_38_0.Checked)
            {
                SaveValueControlForInterfacingESO(Constants.PREGUNTA_38, "0");
            }
        }

        #endregion

    }
}
