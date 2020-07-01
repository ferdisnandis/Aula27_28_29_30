using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
             Produto p = new Produto();
            p.Codigo = 5;
            p.Nome = "Welcome to night vale";
            p.Preco = 39.99f;

            p.Cadastrar(p);

        List<Produto> lista = p.Ler();
        foreach (Produto item in lista){
            Console.WriteLine($"R$ {item.Preco} - {item.Nome}");
        
        }
        }
        }
    }
