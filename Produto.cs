using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula27_28_29_30
{
    public class Produto : IProduto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        private const string PATH = "Database/produto.csv";

        /// <summary>
        /// Configurar o diretório csv
        /// </summary>
        public Produto(){

            string pasta = PATH.Split('/')[0];
            if(!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }
        }
        
        /// <summary>
        /// Adicionar produtos
        /// </summary>
        /// <param name="p">Produtos adicionado</param>
        public void Cadastrar(Produto p){
            var linha = new string[] { p.PrepararLinha(p) };
            File.AppendAllLines(PATH, linha);
        }

        /// <summary>
        /// Ler o que tem no CSV
        /// </summary>
        /// <returns>Lista com nome, código e preço</returns>
        public List<Produto> Ler(){
            //Criar lista para guardar retorno
            List<Produto> prod = new List<Produto>();

            //Lendo as linhas
            string[] linhas = File.ReadAllLines(PATH);

            //Varremos nossas linhas
            foreach(string linha in linhas){
                string[] dado = linha.Split(";");

                Produto p = new Produto();
                p.Codigo = Int32.Parse( Separar(dado[0]) );
                p.Nome = Separar( dado[1] );
                p.Preco = float.Parse( Separar(dado[2]) );

                prod.Add(p);
            }
            //Ordernar a lista
            prod = prod.OrderBy(z => z.Nome).ToList();
            return prod;
        }

        /// <summary>
        /// Alterar um produto existente no CSV
        /// </summary>
        /// <param name="_produtoAlterado">Novo produto alterado</param>
        public void Alterar(Produto _produtoAlterado)
        {
        //Lista para salvar as linhas do csv
            List<string> linhas = new List<string>();

        //Abrir e fechar arquivo de dados
        using(StreamReader arquivo = new StreamReader(PATH))
        {
            //Ler arquivo
            Ler();
            //string linha;
            //while((linha = arquivo.ReadLine()) != null){
            //  linhas.Add(linha);  
            //}

            //Remover linha do código
            linhas.RemoveAll(z => z.Split(";")[0].Contains(_produtoAlterado.Codigo.ToString()));

            //Adicionamos a linha alterada
            linhas.Add( PrepararLinha(_produtoAlterado) );
        }

        //Reescrever o arquivo
        using(StreamWriter output = new StreamWriter(PATH))
        {
            output.Write(String.Join(Environment.NewLine, linhas.ToArray()));
        }

        }
    
        public void ReescreverCSV(List<string> linhas){
            using(StreamWriter output = new StreamWriter(PATH))
        {
            output.Write(String.Join(Environment.NewLine, linhas.ToArray()));
        }
        }
        /// <summary>
        /// Separa o símbolo de = da string do csv
        /// </summary>
        /// <param name="dado">Coluna do csv separada</param>
        /// <returns>string somente com o valor da coluna</returns>
        public string Separar(string dado)
        { 
            return dado.Split("=")[1];
        }

        /// <summary>
        /// Escrever linha no csv
        /// </summary>
        /// <returns>Linha com código, nome e preço do produto</returns>
        private string PrepararLinha(Produto produto){
            return $"codigo={produto.Codigo};nome={produto.Nome};preço={produto.Preco};";
        }
    
        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll( x => x.Nome == _nome);
        }


        /// <summary>
        /// Remover linhas do csv
        /// </summary>
        /// <param name="_termo">Utiliza um termo para excluir uma linha específica</param>
        public void Remover(string _termo)
        {
            List<string> linhas = new List<string>();

        using(StreamReader arquivo = new StreamReader(PATH))
        {
            string linha;
            while((linha = arquivo.ReadLine()) != null){
              linhas.Add(linha);  
            }
            linhas.RemoveAll(z => z.Contains(PATH));
        }

        //Reescrever o arquivo
        using(StreamWriter output = new StreamWriter(PATH))
        {
            output.Write(String.Join(Environment.NewLine, linhas.ToArray()));
        }
}

        string IProduto.PrepararLinha(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}