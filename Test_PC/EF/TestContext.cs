namespace Test_PC.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class TestContext : DbContext
    {
        // Контекст настроен для использования строки подключения "Test" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "Test_PC.Test" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "Test" 
        // в файле конфигурации приложения.
        static TestContext()
        {
            //устанавливаем инициализатор
            Database.SetInitializer<TestContext>(new TestContestInitializer());
        }
        public TestContext()
            : base("name=Test")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // настройка полей с помощью Fluent API
            modelBuilder.Entity<Users>().ToTable("User")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Requests>().ToTable("Requests")
                .HasKey(x => x.Id);

            modelBuilder.Entity<RequestToUser>().ToTable("RequestToUser")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Users>().HasMany(p => p.RequestToUsers).WithRequired(x => x.Users).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Requests>().HasMany(p => p.RequestToUsers).WithRequired(x => x.Requests).HasForeignKey(x => x.RequestId);

        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<RequestToUser> RequestToUser { get; set; }
        public virtual DbSet<ServiceSetting> ServiceSettings { get; set; }

    }

    class TestContestInitializer : DropCreateDatabaseIfModelChanges<TestContext>
    {
        //заполняем бд необходимыми данными
        protected override void Seed(TestContext db)
        {
            db.ServiceSettings.Add(new ServiceSetting {BaseAddress = "https://5d80b2ae99f8a20014cf977a.mockapi.io/api/v1/Users" });            
            db.SaveChanges();
        }
    }
}