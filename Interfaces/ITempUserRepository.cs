﻿using MongoDB.Bson;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Interfaces
{
    public interface ITempUserRepository
    {
        Task<TempUser?> GetByIdAsync(ObjectId id);
        Task<TempUser?> GetByEmailAsync(string email);
        Task<TempUser?> GetByUsernameAsync(string username);
        Task CreateAsync(TempUser user);
        Task DeleteAsync(ObjectId id);
    }
}
