﻿
using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Entities
{
   public class TipoProcedimento :IAggregateRoot<int>
    {
       public virtual int Id { get; set; }
       public virtual string Nome { get; set; }
    }
}
 