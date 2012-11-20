
using System;
using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Entities
{
   public class Procedimento:IAggregateRoot<int>
    {
       public virtual int Id { get; set; }
       public virtual TipoProcedimento TipoProcedimento { get; set; }
       public virtual DateTime DataProcedimento { get; set; }

      
    }
}
