using Shool.IRepository;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shool.Repository
{
    public class RepositoryBase<TEntity> : SimpleClient<TEntity>, IRepositoryBase<TEntity> where TEntity : class, new()
    {
        public RepositoryBase(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
        {
            base.Context = DbScoped.SugarScope;
            //base.Context.DbMaintenance.CreateDatabase();

            //我们也可以批量处理
            Type[] types = Assembly
                    .LoadFrom(@"..\Shool.Entity\bin\Debug\net5.0\Shool.Entity.dll")//如果 .dll报错，可以换成 xxx.exe 有些生成的是exe 
                    .GetTypes().Where(it => it.FullName.Contains("Models"))//命名空间过滤，当然你也可以写其他条件过滤
                    .ToArray();//断点调试一下是不是需要的Type，不是需要的在进行过滤

            base.Context.CodeFirst.InitTables(types);
        }

        #region 添加

        public async Task<bool> AddAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task<bool> AddListAsync(List<TEntity> entities)
        {
            return await base.InsertRangeAsync(entities);
        }

        public async Task<int> AddReturnIdentityAsync(TEntity entity)
        {
            return await base.InsertReturnIdentityAsync(entity);
        }
        #endregion

        #region 修改

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        public async Task<bool> EditAsync(List<TEntity> entities)
        {
            return await base.UpdateRangeAsync(entities);
        }
        #endregion

        #region 删除

        public async Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.DeleteAsync(func);
        }

        public async Task<bool> RemoveListAsync(dynamic[] entitys)
        {
            // 使用事物
            // 删除学生信息时，同时也删除成绩信息
            var check = false;
            try
            {
                base.AsTenant().BeginTran();

                check = await base.DeleteByIdsAsync(entitys);

                base.AsTenant().CommitTran();
            }
            catch
            {
                base.AsTenant().RollbackTran();
            }

            return check;
        }

        #endregion

        #region 查询
        
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        #endregion

        #region 分页
        
        public async Task<List<TEntity>> SelectPageListAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> totalCount, Expression<Func<TEntity, object>> orderFunc=null, OrderByType order=OrderByType.Asc)
        {
            return await base.GetPageListAsync(
                func,
                new PageModel { PageIndex = pageIndex, PageSize = pageSize, TotalCount = totalCount },
                orderFunc,
                order);
        }

        public async Task<List<TEntity>> SelectPageListAsync(int pageIndex, int pageSize, RefAsync<int> totalCount, Expression<Func<TEntity, bool>> func)
        {
            return await base.GetPageListAsync(
                func,
                new PageModel {PageIndex=pageIndex,PageSize=pageSize,TotalCount=totalCount });
        }

        #endregion
    }
}
