# 🤖 Nubank RPA Case – Robô de Automação de Relatório de Vendas

Este projeto foi desenvolvido como solução para o **Case RPA** proposto pela Nubank, com foco em automação de relatórios de vendas a partir de fontes diversas como **PDFs, planilhas Excel e dados externos via API**.

---

## 🧠 Objetivo do Robô

Automatizar todo o processo de:

1. **Leitura e validação de dados de vendas** (extraídos de PDF SalesList.pdf).
2. **Verificação de cadastro de vendedores  (Vendor List.xlsx)**.
3. **Validação de regras de negócio** e geração de relatórios individuais por vendedor, com:
   - Descontos aplicáveis
   - Taxas regionais
   - Conversão de moedas em tempo real
4. **Criação de relatórios em PDF protegidos por senha** e envio automático via e-mail.

---

## ⚙️ Fontes de Dados Utilizadas

- 📄 `Sales List.pdf` (lista de vendas)
- 📊 `Vendor List.xlsx` (vendedores e status)
- 📋 `Sales Report_Template.xlsx` (modelo de relatório)
- 🌐 **API de Câmbio:** Para conversão de moedas em tempo real via 
http://economia.awesomeapi.com.br/json/last/USD-BRL
- 📮 **API de Endereço (ViaCEP):** Para consulta de dados de endereço a partir do CEP via https://viacep.com.br/

---

## ✅ Regras de Negócio Implementadas

- Apenas vendedores com status `Checked` e notas válidas são processados.
- Descontos aplicados: **10% para vendas acima de R$2 milhões**.
- Taxas por região:
  - **São Paulo:** 5%
  - **Rio de Janeiro:** 2%
  - **Minas Gerais:** 1%
- **Premissa de Pagamento:** Todas as faturas (invoices) são consideradas com prazo de pagamento de até **30 dias (D+30)**.

---

## 🔐 Segurança e Confiabilidade

- Geração de **PDFs com senha por vendedor**.
- Verificações robustas para datas, valores, moedas e cadastros.
- Log de erros e exceções tratado com fallback e notificações.

---

## 📬 Entrega Final

Cada vendedor recebe:
- Os relatórios em **PDF** são **protegidos por senha** conforme a regra estabelecida.
- Um e-mail automático com os relatórios (em formato de planilha e PDF) sobre suas vendas anexo para casos válidos.
- Um e-mail automático com o relatório de erro (em formato de planilha) para casos inválidos.

---

## 🛠️ Desenvolvido com

- **UiPath Studio** (baseado no **REFramework adaptado**)
- C# para expressões, regex e lógica avançada
- **C#:** Utilizado para expressões complexas, criação de objetos e manipulação avançada de dados (Regex) para melhor performance e manutenibilidade.
- **APIs Externas:** Consumo de serviços REST via `HTTP Request` com lógica de `retry` para maior resiliência.
- Uso de bibliotecas oficiais UiPath - Excel e PDF Activities

---

## 💡 Diferenciais

- Argumentos de entrada para personalização no ambiente Orchestrator, bem estruturado.
- Extração de dados inteligente com OCR, Regex e tratamento de exceções.
- Estratégia escalável para uso com Orchestrator e múltiplos robôs.

## Pontos de Melhoria

- **Cache de API:** Implementar um mecanismo de cache para requisições à API de câmbio. Isso evitaria múltiplas chamadas com os mesmos parâmetros e garantiria uma taxa de conversão consistente durante toda a execução do robô.
- **Escalabilidade na Leitura de PDF:** Aprimorar a extração de dados de PDFs para suportar documentos com múltiplas páginas, garantindo que o robô processe relatórios de qualquer tamanho.
- **Rastreabilidade de Transações:** Migrar o controle de itens de transação para uma base de dados (ex: SQL Server, Oracle). Isso aumentaria a rastreabilidade, a capacidade de recuperação e facilitaria a geração de relatórios de auditoria.
- **Gestão de Artefatos de Entrada:** Implementar uma rotina para copiar os arquivos de entrada para uma pasta de trabalho versionada (`Input/{timestamp}`), prevenindo a corrupção dos arquivos originais e garantindo a reprodutibilidade do processo.
- **Padronização de Código:** Refatorar nomes de variáveis e atividades para seguir estritamente as convenções de nomenclatura da UiPath (ex: `in_Argumento`, `out_Argumento`, `strVariavel`) e do C#, aumentando a legibilidade e a manutenibilidade do código.
- **Estratégia de APIs:** Avaliar e documentar a política de atualização das APIs consumidas. Para casos que exijam dados com maior frequência de atualização, planejar a substituição ou o uso de APIs pagas com SLAs definidos.

---
**Desenvolvido por:** Levi Matheus Guerreiro Sange
**Case Técnico:** Nubank – RPA Seleção Técnica