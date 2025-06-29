using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExtensoesDataTable
{   
    public static class ExtensoesDataTable
    {
        /// <summary>
        /// Retorna a primeira DataRow que contenha o valor especificado na coluna especificada.
        /// Funciona para valores string
        /// </summary>
        /// <param name="tabela">A DataTable na qual a pesquisa será realizada.</param>
        /// <param name="nomeColuna">O nome da coluna na qual o valor será procurado.</param>
        /// <param name="valor">O valor a ser procurado na coluna.</param>
        /// <returns>
        /// Retorna a primeira DataRow que contenha o valor especificado na coluna especificada.
        /// Retorna null se nenhum valor correspondente for encontrado.
        /// </returns>
        public static DataRow LinhaComValor(this DataTable tabela, string nomeColuna, object valor)
        {
            // Verifica se a coluna existe na DataTable
            if (!tabela.Columns.Contains(nomeColuna))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' não existe na DataTable.");
            }
        
            // Obtém o índice da coluna
            int indiceColuna = tabela.Columns.IndexOf(nomeColuna);
        
            // Realiza a busca usando LINQ
            DataRow linhaEncontrada = tabela.AsEnumerable()
                                            .FirstOrDefault(linha => linha.ItemArray[indiceColuna].ToString() == valor.ToString());
        
            return linhaEncontrada;
        }
    
        /// <summary>
        /// Retorna uma DataTable contendo as linhas que possuem o valor especificado na coluna especificada.
        /// </summary>
        /// <param name="tabela">A DataTable na qual a pesquisa será realizada.</param>
        /// <param name="nomeColuna">O nome da coluna na qual o valor será procurado.</param>
        /// <param name="valor">O valor a ser procurado na coluna.</param>
        /// <returns>
        /// Retorna uma DataTable contendo as linhas que possuem o valor especificado na coluna especificada.
        /// Retorna null se nenhuma linha correspondente for encontrada.
        /// </returns>
        public static DataTable LinhasComValor(this DataTable tabela, string nomeColuna, object valor)
        {
            // Verifica se a coluna existe na DataTable
            if (!tabela.Columns.Contains(nomeColuna))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' não existe na DataTable.");
            }
            
            // Obtém o índice da coluna
            int indiceColuna = tabela.Columns.IndexOf(nomeColuna);
            
            IEnumerable<DataRow> linhasEncontradas = tabela.AsEnumerable().Where(linha => linha.ItemArray[indiceColuna].ToString() == valor.ToString());
    
            return linhasEncontradas.ToList().Count > 0 ? linhasEncontradas.CopyToDataTable() : null;
        }
        
        /// <summary>
        /// Busca o valor de uma coluna especificada (colunaAlvo) se outra coluna (colunaCondicional) tiver um valor especificado.
        /// </summary>
        /// <param name="tabela">A DataTable na qual a pesquisa será realizada.</param>
        /// <param name="colunaAlvo">O nome da coluna da qual o valor será buscado.</param>
        /// <param name="colunaCondicional">O nome da coluna que terá um valor específico para a busca.</param>
        /// <param name="valorCondicional">O valor que a coluna condicional deve ter para realizar a busca.</param>
        /// <returns>
        /// Retorna um object contendo o valor da coluna especificada (colunaAlvo) se a coluna condicional tiver o valor especificado.
        /// Retorna null se nenhum valor correspondente for encontrado.
        /// </returns>
        public static object ProcV(this DataTable tabela, string colunaAlvo, string colunaCondicional, object valorCondicional)
        {
            // Verifica se a coluna existe na DataTable
            if (!tabela.Columns.Contains(colunaAlvo))
            {
                throw new ArgumentException($"A coluna alvo '{colunaAlvo}' não existe na DataTable.");
            }
            if (!tabela.Columns.Contains(colunaCondicional))
            {
                throw new ArgumentException($"A coluna condicional '{colunaCondicional}' não existe na DataTable.");
            }
            
            // Obtém o índice da coluna
            int indiceColunaCondicional = tabela.Columns.IndexOf(colunaCondicional);
            int indiceColunaAlvo = tabela.Columns.IndexOf(colunaAlvo);
            
            return tabela.AsEnumerable()
                        .Where(linha => linha.ItemArray[indiceColunaCondicional].Equals(valorCondicional))
                        .Select(linha => linha.ItemArray[indiceColunaAlvo])
                        .FirstOrDefault();
        }
        
        /// <summary>
        /// Obtém o valor de uma célula na linha e coluna especificadas.
        /// </summary>
        /// <param name="tabela">A DataTable na qual o valor será coletado.</param>
        /// <param name="indiceLinha">O índice da linha onde o valor será coletado.</param>
        /// <param name="nomeColuna">O nome da coluna onde o valor será coletado.</param>
        /// <returns>
        /// Retorna um object contendo o valor da célula na linha e coluna especificadas.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Se o índice da linha estiver fora dos limites da DataTable.</exception>
        /// <exception cref="ArgumentException">Se a coluna especificada não existir na DataTable.</exception>
        public static object ColetarValor(this DataTable tabela, int indiceLinha, string nomeColuna)
        {
            if (indiceLinha < 0 || indiceLinha >= tabela.Rows.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(indiceLinha), "Índice de linha fora dos limites.");
            }
    
            if (!tabela.Columns.Contains(nomeColuna))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' não existe na DataTable.");
            }
    
            return tabela.Rows[indiceLinha][nomeColuna];
        }
        
        /// <summary>
        /// Verifica se um valor está presente em qualquer parte da DataTable.
        /// </summary>
        /// <param name="tabela">A DataTable na qual será realizada a verificação.</param>
        /// <param name="valor">O valor a ser verificado.</param>
        /// <returns>
        /// Retorna true se o valor estiver presente em qualquer parte da DataTable, caso contrário, retorna false.
        /// </returns>
        public static bool ValorExiste(this DataTable tabela, object valor)
        {
            // Realiza a busca usando LINQ
            bool valorExiste = tabela.AsEnumerable()
                         .SelectMany(row => tabela.Columns.Cast<DataColumn>(), (row, col) => new { Row = row, Column = col })
                         .Any(item => item.Row[item.Column] != DBNull.Value && item.Row[item.Column].ToString() == valor.ToString());
            
            return valorExiste;
        }
        
        /// <summary>
        /// Ordena a DataTable com base no valor de uma coluna específica em ordem ascendente.
        /// </summary>
        /// <param name="tabela">A DataTable a ser ordenada.</param>
        /// <param name="nomeColuna">O nome da coluna pela qual a ordenação será feita.</param>
        /// <returns>
        /// Retorna uma nova DataTable contendo as linhas da DataTable original ordenadas pelo valor da coluna especificada em ordem ascendente.
        /// </returns>
        public static DataTable OrdenaPelaColunaASC(this DataTable tabela, string nomeColuna)
        {
            var linhasOrdenadas = tabela.AsEnumerable()
                                  .OrderBy(linha => linha.Field<object>(nomeColuna))
                                  .CopyToDataTable();
    
            return linhasOrdenadas;
        }
    
        /// <summary>
        /// Ordena a DataTable com base no valor de uma coluna específica em ordem descendente.
        /// </summary>
        /// <param name="tabela">A DataTable a ser ordenada.</param>
        /// <param name="nomeColuna">O nome da coluna pela qual a ordenação será feita.</param>
        /// <returns>
        /// Retorna uma nova DataTable contendo as linhas da DataTable original ordenadas pelo valor da coluna especificada em ordem descendente.
        /// </returns>
        public static DataTable OrdenaPelaColunaDESC(this DataTable tabela, string nomeColuna)
        {
            var linhasOrdenadas = tabela.AsEnumerable()
                                  .OrderByDescending(linha => linha.Field<object>(nomeColuna))
                                  .CopyToDataTable();
    
            return linhasOrdenadas;
        }
        
        /// <summary>
        /// Calcula a soma dos valores em uma coluna específica da DataTable.
        /// </summary>
        /// <param name="tabela">A DataTable na qual a soma será calculada.</param>
        /// <param name="nomeColuna">O nome da coluna na qual a soma será calculada.</param>
        /// <returns>
        /// Retorna a soma dos valores na coluna especificada.
        /// </returns>
        public static decimal SomarValoresColuna(this DataTable tabela, string nomeColuna)
        {
            // Verifica se a coluna existe na DataTable
            if (!tabela.Columns.Contains(nomeColuna))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' não existe na DataTable.");
            }
        
            // Obtém o índice da coluna
            int indiceColuna = tabela.Columns.IndexOf(nomeColuna);
            Type tipoColuna = tabela.Columns[indiceColuna].DataType;
            
            if (tipoColuna != typeof(int) && tipoColuna != typeof(double) && tipoColuna != typeof(decimal))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' é do tipo {tipoColuna} que não é suportado para operações de soma.");
            }
            
            return tabela.AsEnumerable()
                        .Sum(linha => Convert.ToDecimal(linha[indiceColuna].ToString()));
        }
    
        /// <summary>
        /// Calcula a média dos valores em uma coluna específica da DataTable.
        /// </summary>
        /// <param name="tabela">A DataTable na qual a média será calculada.</param>
        /// <param name="nomeColuna">O nome da coluna na qual a média será calculada.</param>
        /// <returns>
        /// Retorna a média dos valores na coluna especificada.
        /// </returns>
        public static decimal ValorMedioColuna(this DataTable tabela, string nomeColuna)
        {
            // Verifica se a coluna existe na DataTable
            if (!tabela.Columns.Contains(nomeColuna))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' não existe na DataTable.");
            }
        
            // Obtém o índice da coluna
            int indiceColuna = tabela.Columns.IndexOf(nomeColuna);
            Type tipoColuna = tabela.Columns[indiceColuna].DataType;
            
            if (tipoColuna != typeof(int) && tipoColuna != typeof(double) && tipoColuna != typeof(decimal))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' é do tipo {tipoColuna} que não é suportado para operações de soma.");
            }
            
            return tabela.AsEnumerable()
                        .Average(linha => Convert.ToDecimal(linha[indiceColuna]));
        }
    
        /// <summary>
        /// Encontra o valor mínimo em uma coluna específica da DataTable.
        /// </summary>
        /// <param name="tabela">A DataTable na qual será procurado o valor mínimo.</param>
        /// <param name="nomeColuna">O nome da coluna na qual será procurado o valor mínimo.</param>
        /// <returns>
        /// Retorna o valor mínimo encontrado na coluna especificada.
        /// </returns>
        public static object ValorMinimoColuna(this DataTable tabela, string nomeColuna)
        {
            // Verifica se a coluna existe na DataTable
            if (!tabela.Columns.Contains(nomeColuna))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' não existe na DataTable.");
            }
        
            // Obtém o índice da coluna
            int indiceColuna = tabela.Columns.IndexOf(nomeColuna);
            Type tipoColuna = tabela.Columns[indiceColuna].DataType;
            
            if (tipoColuna != typeof(int) && tipoColuna != typeof(double) && tipoColuna != typeof(decimal))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' é do tipo {tipoColuna} que não é suportado para operações numéricas");
            }
            
            return tabela.AsEnumerable()
                        .Min(linha => linha.Field<object>(indiceColuna));
        }
    
        /// <summary>
        /// Encontra o valor máximo em uma coluna específica da DataTable.
        /// </summary>
        /// <param name="tabela">A DataTable na qual será procurado o valor máximo.</param>
        /// <param name="nomeColuna">O nome da coluna na qual será procurado o valor máximo.</param>
        /// <returns>
        /// Retorna o valor máximo encontrado na coluna especificada.
        /// </returns>
        public static object ValorMaximoColuna(this DataTable tabela, string nomeColuna)
        {
            // Verifica se a coluna existe na DataTable
            if (!tabela.Columns.Contains(nomeColuna))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' não existe na DataTable.");
            }
        
            // Obtém o índice da coluna
            int indiceColuna = tabela.Columns.IndexOf(nomeColuna);
            Type tipoColuna = tabela.Columns[indiceColuna].DataType;
            
            if (tipoColuna != typeof(int) && tipoColuna != typeof(double) && tipoColuna != typeof(decimal))
            {
                throw new ArgumentException($"A coluna '{nomeColuna}' é do tipo {tipoColuna} que não é suportado para operações numéricas.");
            }
            
            return tabela.AsEnumerable()
                        .Max(linha => linha.Field<object>(indiceColuna));
        }
        
        /// <summary>
        /// Agrupa os dados da DataTable com base em uma ou mais colunas.
        /// </summary>
        /// <param name="tabela">A DataTable a ser agrupada.</param>
        /// <param name="nomesColunas">Os nomes das colunas pelas quais os dados serão agrupados.</param>
        /// <returns>
        /// Retorna um dicionário onde as chaves são as combinações de valores das colunas especificadas e os valores são listas de DataRow correspondentes aos grupos.
        /// </returns>
        public static Dictionary<string, List<DataRow>> AgruparPorColunas(this DataTable tabela, params string[] nomesColunas)
        {
            var dadosAgrupados = tabela.AsEnumerable()
                                   .GroupBy(linha => ColetarChaveGrupo(linha, nomesColunas))
                                   .ToDictionary(grupo => grupo.Key, grupo => grupo.ToList());
    
            return dadosAgrupados;
        }
    
        /// <summary>
        /// Método auxiliar para criar uma chave de grupo com base nos valores das colunas.
        /// </summary>
        /// <param name="linha">A DataRow para a qual a chave de grupo será criada.</param>
        /// <param name="nomesColunas">Os nomes das colunas cujos valores serão usados para criar a chave de grupo.</param>
        /// <returns>
        /// Retorna uma string que representa a chave de grupo criada com base nos valores das colunas.
        /// </returns>
        private static string ColetarChaveGrupo(DataRow linha, string[] nomesColunas)
        {
            return string.Join("|", nomesColunas.Select(column => linha[column].ToString()));
        }
        
        /// <summary>
        /// Remove linhas duplicadas da DataTable.
        /// </summary>
        /// <param name="tabela">A DataTable da qual as linhas duplicadas serão removidas.</param>
        /// <returns>
        /// Retorna uma nova DataTable contendo apenas as linhas distintas da DataTable original.
        /// </returns>
        public static DataTable RemoverDuplicados(this DataTable tabela)
        {
            DataTable tabelaDistinta = tabela.Clone(); // Cria uma cópia da estrutura da DataTable original
    
            // Seleciona as linhas distintas
            var linhasDistintas = tabela.AsEnumerable().Distinct(DataRowComparer.Default);
    
            foreach (DataRow linha in linhasDistintas)
            {
                tabelaDistinta.ImportRow(linha); // Importa as linhas distintas para a nova DataTable
            }
    
            return tabelaDistinta;
        }
        
        /// <summary>
        /// Filtra a DataTable com base em múltiplos critérios especificados por um predicado. 
        /// DataTable tabelaFiltrada = tabelaExemplo.FiltrarPorCriterios(linha => linha.Field<string>("Categoria") == categoriaAlvo && linha.Field<decimal>("Prreco") <= precoMaximo);
        /// </summary>
        /// <param name="tabela">A DataTable a ser filtrada.</param>
        /// <param name="predicado">O predicado usado para filtrar as linhas.</param>
        /// <returns>
        /// Retorna uma nova DataTable contendo apenas as linhas que correspondem aos critérios especificados pelo predicado.
        /// </returns>
        public static DataTable FiltrarPorCriterios(this DataTable tabela, Func<DataRow, bool> predicado)
        {
            /* USO
            * DataTable tabelaFiltrada = tabelaExemplo.FiltrarPorCriterios(linha =>
                linha.Field<string>("Categoria") == categoriaAlvo &&
                linha.Field<decimal>("Prreco") <= precoMaximo);
            */
            
            DataTable tabelaFiltrada = tabela.Clone(); // Cria uma cópia da estrutura da DataTable original
    
            // Filtra as linhas com base no predicado
            var linhasFiltradas = tabela.AsEnumerable().Where(predicado);
    
            foreach (DataRow linha in linhasFiltradas)
            {
                tabelaFiltrada.ImportRow(linha); // Importa as linhas filtradas para a nova DataTable
            }
    
            return tabelaFiltrada;
        }
        
        /// <summary>
        /// Pagina os resultados da DataTable.
        /// DataTable tabelaPaginada = tabelaExemplo.Paginar(1, 10);
        /// </summary>
        /// <param name="tabela">A DataTable a ser paginada.</param>
        /// <param name="indexPagina">O índice da página desejada (começando em 0).</param>
        /// <param name="tamanhoPagina">O número de linhas por página.</param>
        /// <returns>
        /// Retorna uma nova DataTable contendo as linhas correspondentes à página especificada.
        /// Retorna null se não houver linhas para a página solicitada.
        /// </returns>
        public static DataTable Paginar(this DataTable tabela, int indexPagina, int tamanhoPagina)
        {
            /* USO
            DataTable tabelaPaginada = tabelaExemplo.Paginar(1, 10)
            Isso traria a segunda página da DataTable contendo 10 itens da DataTable, ou seja, do item 11 ao 20
            */
            
            // Calcula o índice inicial e o número de linhas a serem retornadas
            int indiceInicial = indexPagina * tamanhoPagina;
            int contagemLinhas = Math.Min(tamanhoPagina, tabela.Rows.Count - indiceInicial);
    
            if (contagemLinhas <= 0)
            {
                return null; // Retorna null se não houver linhas para a página solicitada
            }
    
            DataTable tabelaPaginada = tabela.Clone(); // Cria uma cópia da estrutura da DataTable original
    
            // Seleciona as linhas para a página atual
            for (int i = indiceInicial; i < indiceInicial + contagemLinhas; i++)
            {
                tabelaPaginada.ImportRow(tabela.Rows[i]); // Importa as linhas para a nova DataTable
            }
    
            return tabelaPaginada;
        }
        
        /// <summary>
        /// Divide os dados de uma DataTable em páginas de acordo com o tamanho da página especificado.
        /// </summary>
        /// <param name="tabela">A DataTable contendo os dados a serem paginados.</param>
        /// <param name="tamanhoPagina">O número de linhas por página.</param>
        /// <returns>Uma lista de DataTables, onde cada DataTable contém os dados de uma página.</returns>
        /// <remarks>
        /// Este método divide os dados de uma DataTable em páginas de acordo com o tamanho da página especificado.
        /// Cada DataTable na lista de retorno representa uma página com os dados correspondentes.
        /// </remarks>
        public static List<DataTable> ColetarPaginas(this DataTable tabela, int tamanhoPagina)
        {
            List<DataTable> paginas = new List<DataTable>();
        
            int totalLinhas = tabela.Rows.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalLinhas / tamanhoPagina);
        
            for (int pagina = 0; pagina < totalPaginas; pagina++)
            {
                DataTable paginaAtual = tabela.Clone(); // Cria uma cópia da estrutura da DataTable original
        
                // Calcula o índice inicial e o número de linhas a serem retornadas para a página atual
                int indiceInicial = pagina * tamanhoPagina;
                int contagemLinhas = Math.Min(tamanhoPagina, totalLinhas - indiceInicial);
        
                // Seleciona as linhas para a página atual
                for (int i = indiceInicial; i < indiceInicial + contagemLinhas; i++)
                {
                    paginaAtual.ImportRow(tabela.Rows[i]); // Importa as linhas para a nova DataTable
                }
        
                paginas.Add(paginaAtual); // Adiciona a página atual à lista de páginas
            }
        
            return paginas;
        }
    }
}