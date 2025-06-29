using NubankCase_RelatorioVendas.ObjectRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace NubankCase_RelatorioVendas.Class;

public static class CriacaoTabelaVendas
{
    public static DataTable CriarTabelaVendas(string Str_Vendas){
        // Cria a tabela se ainda não existir
        DataTable table = new DataTable();
        table.Columns.Add("INVOICE");
        table.Columns.Add("DATE");
        table.Columns.Add("POSTAL CODE");
        table.Columns.Add("VENDOR ID");
        table.Columns.Add("ITEM TYPE");
        table.Columns.Add("QTY");
        table.Columns.Add("UNIT COST");
        table.Columns.Add("UNIT CURRENCY");
        table.Columns.Add("TOTAL PRICE");
        table.Columns.Add("TOTAL CURRENCY");
        
        // Regex
        string padrao = @"(?<invoice>\S+)\s+(?<date>\d{2}[\/\-][a-zA-Z]{3}[\-\/]\d{2,4}|\d{2}[\/\-]\d{2}[\/\-]\d{4}|0000-00-00)\s+(?<postal>\S+)\s+(?<vendor>[A-Z0-9 ]{8,9})\s+(?<item>(?:[A-Za-z]+\s*)+?)\s+(?<qty>\d+)\s+(?<unitCost>[0-9.,]+)\s+(?<unitCurr>[A-Z]+)\s+(?<totalPrice>[0-9.,]+)\s+(?<totalCurr>[A-Z]+)";
        
        foreach (Match match in Regex.Matches(Str_Vendas, padrao))
        {
            table.Rows.Add(
                match.Groups["invoice"].Value,
                match.Groups["date"].Value,
                match.Groups["postal"].Value,
                match.Groups["vendor"].Value,
                match.Groups["item"].Value.Trim(),
                match.Groups["qty"].Value,
                match.Groups["unitCost"].Value,
                match.Groups["unitCurr"].Value,
                match.Groups["totalPrice"].Value,
                match.Groups["totalCurr"].Value
            );
        }
        
        return table;
    }
    
    public static DataTable AdicionarNovasColunas(this DataTable tabelaVendas){
        tabelaVendas.Columns.Add("Valor Unitário (BRL)");
        tabelaVendas.Columns.Add("Valor Total (BRL)");
        tabelaVendas.Columns.Add("Data/Hora Conversão do Câmbio");
        tabelaVendas.Columns.Add("Endereço");
        tabelaVendas.Columns.Add("Bairro");
        tabelaVendas.Columns.Add("Localidade/UF");
        tabelaVendas.Columns.Add("Erros Encontrados");
        
        return tabelaVendas;
    }
}