# 🤖 Nubank RPA Case – Robô de Automação de Relatório de Vendas

Este projeto foi desenvolvido como solução para o **Case RPA** proposto pela Nubank, com foco em automação de relatórios de vendas a partir de fontes diversas como **PDFs, planilhas Excel e dados externos via API**.

---

## 🧠 Objetivo do Robô

Automatizar todo o processo de:

1. **Leitura e validação de dados de vendas** (extraídos de PDF SalesList.pdf).
2. **Verificação de cadastro de vendedores  (Vendor List.xlsx)**.
3. **Validação de regras de negócio.**
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
- 🌐 API pública de câmbio para conversão de moedas via https://buscacepinter.correios.com.br/app/endereco/index.php?t
- 📮 API de endereço (Correios e IBGE) via https://viacep.com.br/

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
- Os relatórios em **PDF** são **protegidos por senha** conforme a regra estabelecida.
- Um e-mail automático com os relatórios (em formato de planilha e PDF) sobre suas vendas anexo para casos válidos.
- Um e-mail automático com o relatório de erro (em formato de planilha) para casos inválidos.

---

## 🛠️ Desenvolvido com

- **UiPath Studio** (baseado no **REFramework adaptado**)
- C# para expressões, regex e lógica avançada
- Uso de Arquivo de origem de código C# obtendo o melhor em relação a desempenho de execução
- APIs externas (via HTTP Request) com tratamento para tentativas
- Uso de bibliotecas oficiais UiPath - Excel e PDF Activities

---

## 💡 Diferenciais

- Argumentos de entrada para personalização no ambiente Orchestrator, bem estruturado.
- Extração de dados inteligente com OCR, Regex e tratamento de exceções.
- Estratégia escalável para uso com Orchestrator e múltiplos robôs.

## Pontos de Melhoria

- Nas requisições À APIs, valores já obtidos anteriormente poderiam ser armazenados par evitar consultas extras (exemplo: armazenar valor de câmbio para evitar mudanças de valor durante a execução);
- A leitura dos arquivos PDF poderiam ser escaláveis em paginação, permitindo obter mais informações em casos que houverem mais de uma página;
- Item de transação utilizado pode ser mais rastreável se utilizado uma base de dados para controle das vendas por vendedores;
- Documentos usados como Input poderiam ser copiados para um ambiente ou disponibilizados em nuvem, evitando possíveis corrompimentos;
- Ajustar a convenção de nomenclatura utilizada para a oficial UiPath e C# gerando mais clareza;
- A API utilizada para obtenção dos CEPs é atualizada mensalmente, caso necessário atualização mais frequente, pode ser necessário migrar para outra API mais atualizada;

---

**Desenvolvido por:** Levi Matheus Guerreiro Sange
**Case Técnico:** Nubank – RPA Seleção Técnica