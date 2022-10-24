﻿using Microsoft.EntityFrameworkCore;
using TechSupport.BusinessLogic.Exceptions;
using TechSupport.BusinessLogic.Interfaces;
using TechSupport.BusinessLogic.Mapping;
using TechSupport.BusinessLogic.Models.UserModels;
using TechSupport.DataAccess.Context;
using Domain = TechSupport.DataAccess.Models;

namespace TechSupport.BusinessLogic.Services;

internal class UserService : IUserService
{
    private readonly TechSupportContext _context;

    public UserService(TechSupportContext context)
    {
        _context = context;
    }

    public async Task Create(CreateUserRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => IsUserExist(user, request));

        if (user is not null)
        {
            throw new DuplicateDataException("Такой пользователь уже существует");
        }

        user = request.ToDomain();
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int userId)
    {
        var user = await GetUser(userId);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Models.UserModels.User>> GetUsers()
    {
        var users = await _context.Users.AsNoTracking().ToListAsync();

        return users.Select(x => x.ToBl()).ToList();
    }

    public async Task<Models.UserModels.User> GetUserById(int userId)
    {
        var user = await GetUser(userId);

        return user.ToBl();
    }

    public async Task Update(Models.UserModels.User user, string passwordHash)
    {
        var existingUser = await GetUser(user.Id);

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Birthday = user.Birthday;
        existingUser.Phone = user.Phone;
        existingUser.Login = user.Login;
        existingUser.Email = user.Email;
        existingUser.UpdatedOn = DateTime.Now;
        existingUser.Type = user.UserType.ToDomain();
        existingUser.PasswordHash = passwordHash;

        await _context.SaveChangesAsync();
    }

    private static bool IsUserExist(Domain.User user, CreateUserRequest request)
    {
        return user.FirstName == request.FirstName &&
            user.LastName == request.LastName &&
            user.Email == request.Email &&
            user.Phone == request.Phone &&
            user.Birthday == request.Birthday;
    }

    private async Task<Domain.User> GetUser(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user is null)
        {
            throw new NotFoundException("Пользователь не найден.");
        }

        return user;
    }
}