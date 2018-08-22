using System;

namespace PlayTogether.Domain
{
    public interface ISimpleEntity
    {
        Guid Id { get; }
    }
}