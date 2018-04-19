using Inventory.LunarMed.Web.Business.Interfaces;
using Inventory.LunarMed.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Inventory.LunarMed.Web.Business.Repository.Base
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        /// <summary>
        /// Gets or sets the ShipDbContext the repository is using as a persistence store.
        /// </summary>
        protected ApplicationDbContext DbContext { get; set; }

        /// <summary>
        /// Gets or sets the DbSet containing the entities the repository is being used to interact with.
        /// </summary>
        protected DbSet<T> DbSet { get; set; }

        /// <summary>
        /// The contructor requires an open DataContext to work with
        /// </summary>
        /// <param name="context">An open DataContext</param>
        public GenericRepository(ApplicationDbContext context)
        {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<T>();
        }

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public virtual T Get(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        /// <summary>
        /// Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>An ICollection of every object in the database</returns>
        public virtual ICollection<T> GetAll()
        {
            DbContext.Configuration.LazyLoadingEnabled = true;
            return DbContext.Set<T>().ToList();
        }

        /// <summary>
        /// Returns a single item from the repository matching the provided criteria.
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <param name="navigationProperties">Additional navigation properties to be included in the results.</param>
        /// <returns>Matching item, or null if no items match.</returns>
        public virtual T Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties)
        {
            // Set up query.
            IQueryable<T> query = this.DbSet;
            query.AsNoTracking();

            // Add eager loading of navigation properties if specified.
            if (navigationProperties != null)
            {
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    query = query.Include<T, object>(navigationProperty);
                }
            }

            // Return query results.
            return query.SingleOrDefault<T>(filter);
        }

        /// <summary>
        /// Returns a list of items from the repository matching the provided criteria.
        /// </summary>
        /// <param name="filter">Filter criteria.</param>
        /// <param name="navigationProperties">Additional navigation properties to be included in the results.</param>
        /// <returns>List of matching items.</returns>
        public virtual List<T> List(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            // Set up query.
            IQueryable<T> query = this.DbSet;
            query.AsNoTracking();

            // Add filter to query if specified.
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Add eager loading of navigation properties if specified.
            if (navigationProperties != null)
            {
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    query = query.Include<T, object>(navigationProperty);
                }
            }

            // Return query results.
            return query.ToList<T>();
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="t">The object to insert</param>
        /// <returns>The bool if the operation is successful or not</returns>
        public virtual T Add(T entity)
        {
            this.DbSet.Add(entity);
            this.DbContext.Entry(entity).State = EntityState.Added;
            DbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>returns true if the operation is Successful and false if not</returns>
        public virtual bool AddAll(IEnumerable<T> tList)
        {
            try
            {
                DbContext.Set<T>().AddRange(tList);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="entity">The updated object to apply to the database</param>
        /// <returns>The resulting updated object</returns>
        public virtual T Update(T entity)
        {
            this.DbSet.Attach(entity);
            this.DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="entity">The object that will be deleted in the database.</param>
        public virtual bool Delete(T entity)
        {
            try
            {
                if (this.DbContext.Entry(entity).State == EntityState.Detached)
                {
                    this.DbSet.Attach(entity);
                }
                this.DbSet.Remove(entity);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes a list of objects from the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="tList">The list of bojects that will be deleted</param>
        public virtual bool DeleteAll(IEnumerable<T> tList)
        {
            try
            {
                DbContext.Set<T>().RemoveRange(tList);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a single object does exists in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="entity">The object that will be searched.</param>
        public virtual bool IsExists(T entity)
        {
            return DbContext.Set<T>().Local.Any(e => e == entity);
        }

        /// <summary>
        /// Dispose resources.
        /// </summary>
        public void Dispose()
        {
            if (this.DbContext != null)
            {
                this.DbContext.Dispose();
                this.DbContext = null;
            }
        }
    }
}