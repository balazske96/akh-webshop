using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence
{
    public interface IHasId
    {
        public Guid Id { get; set; }
    }
}