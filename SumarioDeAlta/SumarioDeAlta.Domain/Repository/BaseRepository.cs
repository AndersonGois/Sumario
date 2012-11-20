using System.Collections.Generic;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SumarioDeAlta.Domain.Entities;
using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Repository
{
    public abstract class BaseRepository
    {
        public const string NHibernateSessionKey = "nhibernate.session.key";

        public static ISessionFactory FACTORY = CreateSessionFactory();
        private static ISession _session;

        public static ISession Session
        {
            get { return _session ?? (_session = GetCurrentSession()); }
            set { _session = value; }
        }

        private static readonly object syncObj = 1;

        #region Métodos Genericos para acesso ao DB

        public BaseRepository(ISession session)
        {
            Session = session;
        }

        public BaseRepository()
        {

        }

        public virtual void Salvar(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();
            Session.SaveOrUpdate(root);

            transaction.Commit();
        }

        public virtual void Deletar(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();
            Session.Delete(root);
            transaction.Commit();
            CloseTransaction(transaction);
        }

        public virtual IList<T> Todos<T>()
        {
            return Session.CreateCriteria(typeof(T)).List<T>();
        }

        public T Obter<T>(int id)
        {
            return Session.Get<T>(id);
        }

        #endregion

        #region Métodos de Sessão e Transação

        public static void CloseTransaction(ITransaction transaction)
        {
            transaction.Dispose();
        }

        public static ISession GetCurrentSession()
        {
            ISession currentSession;

            lock (syncObj)
                currentSession = FACTORY.OpenSession();

            return currentSession;
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c
                .FromAppSetting("Conexao")
                )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<Paciente>()).BuildSessionFactory();
        }

        #endregion
    }
}
