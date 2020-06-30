using System;
using System.Collections.Generic;
using System.IO;
namespace Aula27_28_29_30
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }


        private const string PATH = "Database/produto.csv";

        public Produto(){

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
            return prod;
        }
        
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