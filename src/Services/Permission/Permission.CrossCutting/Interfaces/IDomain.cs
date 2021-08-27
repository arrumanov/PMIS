using System;

namespace Permission.CrossCutting.Interfaces
{
    public interface IDomain
    {
        Guid Id { get; }
        void Validate();
    }
}