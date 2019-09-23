using System;
using System.Collections.Generic;

namespace Revisao
{
    class Program
    {
        static void Main(string[] args)
        {
            string opc;
            Chamado[] chamados = new Chamado[10];
            Analista[] analistas = new Analista[3];
            int quantidadeChamados = 0;
            int quantidadeAnalistas = 0;

            do
            {
                Console.WriteLine("");
                Console.WriteLine("Escolha as opções abaixo disponiveis:");
                Console.WriteLine("");
                Console.WriteLine("1. Cadastrar um novo chamado.");
                Console.WriteLine("2. Cadastrar um analista.");
                Console.WriteLine("3. Associar chamado à analista.");
                Console.WriteLine("4. Fechar chamado.");
                Console.WriteLine("5. Sair do sistema.");
                Console.Write("Opção: ");
                opc = Console.ReadLine();
                Console.WriteLine("");

                if (opc == "1")
                {
                    if (quantidadeChamados < 10)
                    {
                        Chamado chamado = new Chamado
                        {
                            DataAbertura = DateTime.Now,
                            Id = Guid.NewGuid().ToString(),
                            status = Status.Aberto

                        };
                        Console.WriteLine("");
                        Console.Write("Digite a descriação do Chamado: ");
                        chamado.Descricao = Console.ReadLine();

                        chamados[quantidadeChamados] = chamado;
                        quantidadeChamados++;
                    }
                    else
                        Console.WriteLine("Excedeu o numero máximo para o cadastro de Chamados.");
                }

                else if (opc == "2")
                {
                    if (quantidadeAnalistas < 3)
                    {
                        Analista analista = new Analista
                        {
                            Id = Guid.NewGuid().ToString()
                        };

                        Console.Write("Digite o Nome do Analista: ");
                        analista.Nome = Console.ReadLine();
                        Console.Write("Digite o Email do Analista: ");
                        analista.Email = Console.ReadLine();
                        Console.Write("Digite a data de nascimento do Analista: ");
                        string dataNasc = Console.ReadLine();
                        analista.DataNascimento = DateTime.Parse(dataNasc);
                        analista.Chamado = new List<Chamado>();

                        analistas[quantidadeAnalistas] = analista;
                        quantidadeAnalistas++;
                    }
                    else
                        Console.WriteLine("Excedeu o numero máximo para o cadastro de Analistas.");
                }

                else if (opc == "3")
                {
                    if (chamados != null && analistas != null)
                    {
                        Console.WriteLine("Chamados Cadastrados:");
                        foreach (var chamado in chamados)
                        {
                            if (chamado != null && chamado.status == Status.Aberto)
                            {
                                Console.WriteLine("Código do Chamado: " + chamado.Id + ", Descrição: " + chamado.Descricao + ", Status: " + chamado.status + ", Data da Abertura: " + chamado.DataAbertura.ToString("dd/MM/yyyy"));
                            }

                        }
                        Console.WriteLine("");
                        Console.WriteLine("Analistas Disponíveis:");
                        foreach (var analista in analistas)
                        {
                            if (analista != null)
                                Console.WriteLine("Código do Analista: " + analista.Id + ", Nome: " + analista.Nome + ", Data de Nascimento: " + analista.DataNascimento.ToString("dd/MM/yyyy") + ", Email: " + analista.Email);
                        }

                        Console.WriteLine("");
                        Console.WriteLine("Associe o Chamado em Aberto ao Analista Disponível.");
                        Console.Write("Digite o codigo do Chamado: ");
                        string idChamado = Console.ReadLine();
                        Console.Write("Digite o codigo do Analista: ");
                        string idAnalista = Console.ReadLine();

                        foreach (var chamado in chamados)
                        {
                            if (chamado != null && chamado.Id == idChamado)
                            {
                                foreach (var analista in analistas)
                                {
                                    if (analista != null && analista.Id == idAnalista)
                                    {
                                        if (chamado.status == Status.Aberto)
                                        {
                                            chamado.status = Status.EmAndamento;
                                            analista.Chamado.Add(chamado);
                                        }
                                        else
                                            Console.WriteLine("O chamado já foi associado a um Analista.");

                                    }
                                }
                            }
                        }
                    }
                    else
                        Console.WriteLine("Para Associar cadastre pelo menos um Chamado e um Analista.");
                }
                else if (opc == "4")
                {
                    foreach (var chamado in chamados)
                    {
                        if (chamado != null && chamado.status == Status.EmAndamento)
                        {
                            Console.WriteLine("Código do Chamado: " + chamado.Id + ", Descrição: " + chamado.Descricao + ", Status: " + chamado.status + ", Data da Abertura: " + chamado.DataAbertura);
                        }
                    }
                    Console.WriteLine("");
                    Console.Write("Digite o ID do chamado que deseja Fechar: ");
                    string idChamado = Console.ReadLine();

                    foreach (var chamado in chamados)
                        if (chamado != null && chamado.status == Status.EmAndamento && chamado.Id == idChamado)
                        {
                            chamado.status = Status.Fechado;
                        }
                }

                else if (opc == "5")
                {
                    Console.WriteLine("");
                    Console.WriteLine("O sistema está sendo finalizado...");
                    if (analistas[0] != null && chamados[0] != null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Chamados associados ao Analista:");
                        foreach (var analista in analistas)
                        {
                            if (analista != null && analista.Chamado != null)
                            {
                                Console.WriteLine("*********************************************************************************************************************************************");
                                Console.WriteLine("Nome: " + analista.Nome);
                                Console.WriteLine("");

                                foreach (var chamado in chamados)
                                {
                                    if (chamado != null)
                                    {

                                        foreach (var chamadoAssociado in analista.Chamado)
                                        {
                                            if (chamadoAssociado != null && chamadoAssociado.Id.Trim() == chamado.Id.Trim())
                                            {
                                                Console.WriteLine("Código do Chamado: " + chamado.Id + ", Descrição: " + chamado.Descricao + ", Status: " + chamado.status + ", Data da Abertura: " + chamado.DataAbertura.ToString("dd/MM/yyyy"));
                                            }

                                        }
                                    }

                                }
                                Console.WriteLine("**********************************************************************************************************************************************");
                            }
                        }
                    }
                }
            }
            while (opc != "5");
            Console.ReadLine();
        }
    }
}
