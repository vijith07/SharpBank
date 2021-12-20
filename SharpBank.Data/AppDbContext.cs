using Microsoft.EntityFrameworkCore;
using SharpBank.Models;
using SharpBank.Models.Enums;

namespace SharpBank.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bank>()
                .HasMany<Account>(b => b.Accounts)
                .WithOne(a => a.Bank);

            modelBuilder.Entity<Bank>().HasMany<Currency>(b => b.Currencies);
            
            modelBuilder.Entity<Bank>().HasOne(b => b.DefaultCurrency);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceAccount)
                .WithMany(sa => sa.DebitTransactions)
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.DestinationAccount)
                .WithMany(sa => sa.CreditTransactions)
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            //Seeding data
            Currency currency = new Currency
            {
                Id = Guid.NewGuid(),
                Name = "Desi Rupee",
                Code = "INR",
                ExchangeRate = 1
            };
            modelBuilder.Entity<Currency>().HasData(currency);
            Bank b1 = new Bank
            {
                Id = Guid.NewGuid(),
                Name = "New Bank",
                DefaultCurrencyId = currency.Id,
                CreatedBy = "admin",
                UpdatedBy = "admin",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                RTGSToOther = 0.05m,
                RTGSToSame = 0.0m,
                IMPSToOther = 0.07m,
                IMPSToSame = 0.03m

            };
            modelBuilder.Entity<Bank>().HasData(b1);
            Account account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "John Hoe",
                BankId = b1.Id,
                Balance = 20m,
                Password = "1234",
                Gender = Gender.Female,
                Status = Status.Active,
                Type = AccountType.Customer

            };
            Account account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                BankId = b1.Id,
                Balance = 201m,
                Password = "1234",
                Gender = Gender.Female,
                Status = Status.Active,
                Type = AccountType.Customer

            };
            modelBuilder.Entity<Account>().HasData(account1, account2);
            Transaction t1 = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = 10m,
                SourceAccountId = account1.Id,
                DestinationAccountId = account2.Id,
                TransactionCharges = 0.1m,
                NetAmount = 10.1m,
                On = DateTime.Now
            };
            modelBuilder.Entity<Transaction>().HasData(t1);
        }
    }
}
