using NubankCase_RelatorioVendas.ObjectRepository;
using System;
using System.Collections.Generic;
using System.Data;

namespace NubankCase_RelatorioVendas.Class;

public class CepResponse
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public string Unidade { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }
    public string Estado { get; set; }
    public string Regiao { get; set; }
    public string Ibge { get; set; }
    public string Gia { get; set; }
    public string Ddd { get; set; }
    public string Siafi { get; set; }
}