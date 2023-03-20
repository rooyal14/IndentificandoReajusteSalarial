using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdentificandoReajusteSalarial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            inicializar();
        }


        private void inicializar()
        {
            rbnCalouro.Checked = true;
            rbnMatutino.Checked = true;
            fmSalarioMinimo.Text = "0";
            fmHorasTrabalhadas.Text = "0";
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fmSalarioMinimo.Text) || String.IsNullOrEmpty(fmHorasTrabalhadas.Text))
            {
                MessageBox.Show("Insira corretamente os dados", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lbxValores.Items.Clear();
            RadioButton turnoSelecionado = gbxTurno.Controls.OfType<RadioButton>().SingleOrDefault(r => r.Checked);
            RadioButton categoriaSelecionado = gbxCategoria.Controls.OfType<RadioButton>().SingleOrDefault(r => r.Checked);
            double salarioMinimo = Convert.ToDouble(fmSalarioMinimo.Text);
            double horasTrabalhadas = Convert.ToDouble(fmHorasTrabalhadas.Text);

            fmClassificacao.Text = Processador.getCoeficiente(turnoSelecionado, salarioMinimo).ToString();

            RealizarProcessamento(turnoSelecionado, categoriaSelecionado, salarioMinimo, horasTrabalhadas);
        }

        private void RealizarProcessamento(RadioButton rbnTurno, RadioButton rbnCategoria, double salarioMinimo, double horasTrabalhadas)
        {
            double coeficiente = Processador.getCoeficiente(rbnTurno, salarioMinimo);
            double salarioBruto = Processador.getSalarioBruto(coeficiente, horasTrabalhadas);
            double imposto = Processador.getImposto(rbnCategoria, salarioBruto);
            double gratificacao = Processador.getGratificacao(rbnTurno, horasTrabalhadas);
            double auxilioAlimentacao = Processador.getAuxilioAlimentacao(rbnCategoria, salarioBruto, salarioMinimo);
            double salarioLiquido = Processador.getSalarioLiquido(salarioBruto, imposto, gratificacao, auxilioAlimentacao);
            string classificacao = Processador.getClassificacao(salarioLiquido);

            apresentarConteudo(coeficiente, salarioBruto, imposto, gratificacao, auxilioAlimentacao, salarioLiquido, classificacao);

        }

        private void apresentarConteudo(double coeficiente, double salarioBruto, double imposto, double gratificacao,
            double auxilioAlimentacao, double salarioLiquido, string classificacao)
        {
            lbxValores.Items.Add(String.Format("{0,-29}{1,12:C}", "Valor do coeficiente:", coeficiente));
            lbxValores.Items.Add(String.Format("{0,-29}{1,12:C}", "Salário bruto:", salarioBruto));
            lbxValores.Items.Add(String.Format("{0,-29}{1,12:C}", "Valor do imposto :", imposto));
            lbxValores.Items.Add(String.Format("{0,-29}{1,12:C}", "Valor da gratificação :", gratificacao));
            lbxValores.Items.Add(String.Format("{0,-29}{1,12:C}", "Valor auxilio alimentação :", auxilioAlimentacao));
            lbxValores.Items.Add(String.Format("{0,-29}{1,12:C}", "Salário líquido:", salarioLiquido));
            fmClassificacao.Text = classificacao;
        }

        private void fm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar.ToString() == ","))
            {
                e.Handled = true;
            }
        }
    }
}
