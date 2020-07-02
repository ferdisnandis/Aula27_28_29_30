using System.Collections.Generic;

namespace Aula27_28_29_30
{
    public interface IProduto
    {
        void Cadastrar(Produto p);
        List<Produto> Ler();
        void Remover(string _termo);
        void Alterar(Produto _produtoAlterado);
        void ReescreverCSV(List<string> linhas);
        string Separar(string dado);
        string PrepararLinha(Produto produto);
        List<Produto> Filtrar(string _nome);
        
    }
}