using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula27_28_29_30
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        private const string PATH = "Database/produto.csv";

        //public Produto(int codigo, string nome, float preco) 
        //{
        //    this.Codigo = codigo;
        //        this.Nome = nome;
        //        this.Preco = preco;
        // }

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
        
        public void Cadastrar(Produto p){
            var linha = new string[] { p.PrepararLinha(p) };
            File.AppendAllLines(PATH, linha);
        }

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
            prod = prod.OrderBy(z => z.Nome).ToList();
            return prod;
        }

        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll( x => x.Nome == _nome);
        }

        //Remover linhas
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

        //Separar as linhas do arquivo em ";" e "="
        public string Separar(string dado)
        { 
            return dado.Split("=")[1];
        }

        //1,Sapato,34.50
        private string PrepararLinha(Produto produto){
            return $"codigo={produto.Codigo};nome={produto.Nome};preço={produto.Preco};";
        }
    }
}