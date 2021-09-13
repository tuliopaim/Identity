using System;

namespace Identity.Business.Entities
{
    public interface IEntidadeBase
    {
        public DateTime DataDeCriacao { get; }
        public DateTime DataDeAtualizacao { get; }  
    }
}
