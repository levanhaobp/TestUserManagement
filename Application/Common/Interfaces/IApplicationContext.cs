using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationContext
    {
        Task<int> SaveChangesAsync();
        DbSet<User> Users { get; set; }
    }
}
