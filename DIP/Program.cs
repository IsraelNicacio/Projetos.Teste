// See https://aka.ms/new-console-template for more information
using DIP;
using DIP.Entidades;
using DIP.Persistencia;

MyNinjectModule myNinjectModule = new MyNinjectModule();
myNinjectModule.Load();

Produto Produto = new Produto(new FirebirdConnection());
Produto.Recuperar2();


