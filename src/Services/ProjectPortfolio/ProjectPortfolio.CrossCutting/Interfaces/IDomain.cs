using System;

namespace ProjectPortfolio.CrossCutting.Interfaces
{
    public interface IDomain
    {
        Guid Id { get; }
        void Validate();
    }
}