using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chat.IRepository;
using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity:class,new()
    {
        private readonly DbSet<TEntity> _context ;
        private readonly ChatContext _chatContext1;

        public BaseRepository(ChatContext chatContext) {
            _chatContext1 = chatContext;
            _context = chatContext.Set<TEntity>();
        }

        public async Task<bool> DelectAsync(IEnumerable<TEntity> entities)
        {
            _chatContext1.Set<TEntity>().RemoveRange(entities);
            _context.RemoveRange(entities);
            return await _chatContext1.SaveChangesAsync()==entities.Count();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            return await _chatContext1.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            _context.Update(entity);
            return await _chatContext1.SaveChangesAsync()>0;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            if (await _chatContext1.SaveChangesAsync()>0)
            {
                return entity;
            }
            return null;
        }

        public async Task<bool> InsertListAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
            return await _chatContext1.SaveChangesAsync() == entities.Count();
        }

        public async Task<IEnumerable<TEntity>> QuseryAsync()
        {
            return await _context.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> QuseryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _context.Where(func).ToListAsync();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _context.SingleOrDefaultAsync<TEntity>(func);
        }
    }
}
