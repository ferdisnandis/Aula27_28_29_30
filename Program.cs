using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
             Produto p = new Produto();
            p.Codigo = 1;
            p.Nome = "Sapatilha";
            p.Preco = 49.99f;

            p.Cadastrar(p);

        List<Produto> lista = p.Ler();
        foreach (Produto item in lista){
            Console.WriteLine($"R$ {item.Preco} - {item.Nome}");
        
        }
        }
    }
}
