using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdentificandoReajusteSalarial
{
    class Processador
    {
        public static double getCoeficiente (RadioButton rbnTurno, double salarioMinimo)
        {
            string turno = rbnTurno.Text;
            switch (turno)
            {
                case "Matutino":
                    return salarioMinimo * 0.01;
                case "Vespertino":
                    return salarioMinimo * 0.02;
                case "Noturno":
                    return salarioMinimo * 0.03;
            }
            return 0;
        }

        public static double getSalarioBruto (double coeficiente, double horasTrabalhadas)
        {
            return coeficiente * horasTrabalhadas;
        }

        public static double getImposto (RadioButton rbnCategoria, double salarioBruto)
        {
            string categoria = rbnCategoria.Text;
            
            switch (categoria)
            {
                case "Calouro":
                    if (salarioBruto < 300)
                    {
                        return salarioBruto * 0.01;
                    } else if (salarioBruto >= 300)
                    {
                        return salarioBruto * 0.02;
                    }
                    break;
                case "Veterano":
                    if (salarioBruto < 400)
                    {
                        return salarioBruto * 0.03;
                    }
                    else if (salarioBruto >= 400)
                    {
                        return salarioBruto * 0.04;
                    }
                    break;

            }
            return 0;
        }

        public static double getGratificacao (RadioButton rbnTurno, double horasTrabalhadas)
        {
            string turno = rbnTurno.Text;
            if (turno == "Noturno" && horasTrabalhadas > 80)
            {
                return 50;
            } else
            {
                return 30;
            }

        }

        public static double getAuxilioAlimentacao (RadioButton rbnCategoria, double salarioBruto, double salarioMinimo)
        {
            string categoria = rbnCategoria.Text;
            if (categoria == "calouro" || salarioBruto < (salarioMinimo/2))
            {
                return salarioBruto / 3;
            } else
            {
                return salarioBruto / 6;
            }

        }

        public static double getSalarioLiquido (double salarioBruto, double imposto, double gratificacao, double auxilioAlimentacao)
        {
            return salarioBruto - imposto + gratificacao + auxilioAlimentacao;

        }

        public static string getClassificacao (double salarioLiquido)
        {
            if (salarioLiquido < 350)
            {
                return "Mal Remunerado";

            } else if (salarioLiquido >= 350 && salarioLiquido <= 600)
            {
                return "Normal";

            } else if (salarioLiquido > 600)
            {
                return "Bem Remunerado";
            }
            return "Undefined";
        }

    }
}
