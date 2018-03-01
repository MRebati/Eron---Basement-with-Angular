using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Eron.Core.ManagementSettings;
using Eron.DataAccess.Contract.UnitOfWorks;

namespace Eron.DataAccess.EntityFramework.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {
        bool disposed = false;

        private TransactionScope _transaction;
        private TransactionOptions _transactionOptions;
        protected DbContext Context;

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        
        public virtual void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return Context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }

        /// <summary>
        /// creates new instance of transaction scope.
        /// </summary>
        /// <returns></returns>
        public virtual TransactionScope Begin()
        {
            _transactionOptions.IsolationLevel = IsolationLevel.RepeatableRead;
            _transactionOptions.Timeout = ApplicationSettings.TransactionTimeout;
            _transaction = new TransactionScope(TransactionScopeOption.Required,_transactionOptions,TransactionScopeAsyncFlowOption.Enabled);
            
            return _transaction;
        }

        /// <summary>
        /// indicates that all operations in transaction 
        /// within scope are completed successfully.
        /// </summary>
        /// <returns></returns>
        public virtual Task CompleteAsync()
        {
            return Task.Run(() => Complete());
        }

        /// <summary>
        /// indicates that all operations in transaction 
        /// within scope are completed successfully.
        /// </summary>
        /// <returns></returns>
        public virtual void Complete()
        {
            _transaction.Complete();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _transaction?.Dispose();
                Context?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}