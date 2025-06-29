using NubankCase_RelatorioVendas.ObjectRepository;
using System;
using System.Collections.Generic;
using System.Data;

namespace NubankCase_RelatorioVendas.Class;

public class ErrorResponseApiConversao
{
    public int Status { get; set; }
    public string Sode { get; set; }
    public string Message { get; set; }
}