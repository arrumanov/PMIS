using System;

namespace Dogovor.CrossCutting.Interfaces
{
    public interface IDomain
    {
        Guid Id { get; }
        void Validate();
    }
}