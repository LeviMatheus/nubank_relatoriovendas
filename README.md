# 🤖 Nubank RPA Case – Robô de Automação de Relatório de Vendas

Este projeto foi desenvolvido como solução para o **Case RPA** proposto pela Nubank, com foco em automação de relatórios de vendas a partir de fontes diversas como **PDFs, planilhas Excel e dados externos via API**.

---

## 🧠 Objetivo do Robô

Automatizar todo o processo de:

1. **Leitura e validação de dados de vendas** (extraídos de PDF).
2. **Verificação de cadastro de vendedores e regras de negócio.**
3. **Geração de relatórios individuais por vendedor**, com:
   - Descontos aplicáveis
   - Taxas regionais
   - Conversão de moedas em tempo real
4. **Criação de relatórios em PDF protegidos por senha**.
5. **Envio automático dos relatórios via e-mail.**

---

## ⚙️ Fontes de Dados Utilizadas

- 📄 `Sales List.pdf` (lista de vendas)
- 📊 `Vendor List.xlsx` (vendedores e status)
- 📋 `Sales Report_Template.xlsx` (modelo de relatório)
- 🌐 API pública de câmbio para conversão de moedas
- 📮 API de endereço (Correios) para enriquecimento de dados (se necessário)

---

## ✅ Regras de Negócio Implementadas

- Apenas vendedores com status `Checked` e notas válidas são processados.
- Descontos aplicados: **10% para vendas acima de R$2 milhões**.
- Taxas por região:
  - **São Paulo:** 5%
  - **Rio de Janeiro:** 2%
  - **Minas Gerais:** 1%
- Todas as invoices são consideradas com prazo de até **D+30**.

---

## 🔐 Segurança e Confiabilidade

- Geração de **PDFs com senha por vendedor**.
- Verificações robustas para datas, valores, moedas e cadastros.
- Log de erros e exceções tratado com fallback e notificações.

---

## 📬 Entrega Final

Cada vendedor recebe:
- Os relatórios **protegidos por senha**, em **PDF** são **protegidos por senha**
- Um e-mail automático com os relatórios (em formato de planilha e PDF) sobre suas vendas anexo.

---

## 🛠️ Desenvolvido com

- **UiPath Studio** (baseado no **REFramework adaptado**)
- C# para expressões, regex e lógica avançada
- APIs externas (via HTTP Request)
- Excel e PDF Activities

---

## 💡 Diferenciais

- Argumentos de entrada para personalização no ambiente Orchestrator, bem estruturado.
- Extração de dados inteligente com OCR, Regex e tratamento de exceções.
- Estratégia escalável para uso com Orchestrator e múltiplos robôs.

---

**Desenvolvido por:** Levi  
**Case Técnico:** Nubank – RPA Seleção Técnica