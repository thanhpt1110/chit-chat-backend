﻿using ChitChat.Domain.Entities;
using ChitChat.Domain.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IUserRepository
    {
        IQueryable<UserApplication> GetQuery();

        Task<UserApplication> GetFirstAsync(Expression<Func<UserApplication, bool>> predicate);

        Task<UserApplication> GetFirstOrDefaultAsync(Expression<Func<UserApplication, bool>> predicate);
        Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate);

        Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate, IEnumerable<Expression<Func<UserApplication, BaseEntity>>> includes);
        Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate, Func<IQueryable<UserApplication>, IIncludableQueryable<UserApplication, object>> includeQuery);

        Task<List<UserApplication>> GetAllAsync(Expression<Func<UserApplication, bool>> predicate, Func<IQueryable<UserApplication>, IOrderedQueryable<UserApplication>> sort);


    }
}