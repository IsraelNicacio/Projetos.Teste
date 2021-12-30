// See https://aka.ms/new-console-template for more information
using Autofac;
using DIP.Entidades;
using DIP.Intefaces;
using DIP.Persistencia;
using System.ComponentModel;
using System.Web.Http;

Produto Produto = new Produto(new FirebirdConnection());
Produto.Recuperar2();


