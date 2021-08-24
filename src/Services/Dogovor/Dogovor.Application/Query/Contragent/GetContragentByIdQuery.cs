using System;

namespace Dogovor.Application.Query.Contragent
{
    public class GetContragentByIdQuery : QueryBase<Infrastructure.Database.Query.Model.Contragent>
    {
        public Guid Id { get; set; }
    }
}