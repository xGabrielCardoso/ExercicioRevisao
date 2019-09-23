using System;

namespace Revisao
{
    public class Chamado
    {
        public DateTime DataAbertura;
        public string Descricao;
        public string Id;
        public Status status;
        
    }

    public enum Status
    {
        Aberto = 1,
        EmAndamento = 2,
        Fechado = 3
    }
}