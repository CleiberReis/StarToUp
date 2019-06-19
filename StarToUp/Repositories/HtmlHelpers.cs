using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        public static string GraficoPizza(this HtmlHelper html,
        string nomeGrafico,
        Dictionary<object, object> colunas,
        Dictionary<object, object> linhas,
        int width,
        int height)
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("<div id='{0}'></div>", nomeGrafico);
            sb.AppendLine("<script language='javascript'>");
            sb.AppendLine("google.load('visualization', '1.0', { 'packages': ['corechart'] });");
            sb.AppendLine("google.setOnLoadCallback(drawChart);");
            sb.AppendLine("function drawChart() {");
            sb.AppendLine("var data = new google.visualization.DataTable();");
            foreach (var coluna in colunas)
                sb.AppendLine(String.Format("data.addColumn('{0}', '{1}');", coluna.Key,
                coluna.Value));
            foreach (var linha in linhas)
                sb.AppendLine(String.Format("data.addRow(['{0}', {1}]);", linha.Key,
                linha.Value));
            sb.AppendLine("var options = { 'title': 'Startups Cadastradas', 'pieSliceText': 'value',");
            sb.AppendLine(String.Format(" 'width': '{0}',", width));
            sb.AppendLine(String.Format(" 'height': '{0}' ", height));
            sb.AppendLine(" }; ");
            sb.AppendLine(String.Format("var chart = new google.visualization.PieChart(document.getElementById('{0}')); ", nomeGrafico));
            sb.AppendLine("chart.draw(data, options);");
            sb.AppendLine("}");
            sb.AppendLine("</script>");
            return sb.ToString();
        }
    }
}