using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseCart> PurchaseCarts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        //public DbSet<Cart> Carts { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Purchase - Supplier
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Purchases)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseItem - Purchase
            modelBuilder.Entity<PurchaseItem>()
                .HasOne(p => p.Purchase)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(p => p.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseItem - Medicine
            modelBuilder.Entity<PurchaseItem>()
                .HasOne(p => p.Medicine)
                .WithMany(m => m.PurchaseItems)
                .HasForeignKey(p => p.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            // SaleItem - Sale
            modelBuilder.Entity<SaleItem>()
                .HasOne(s => s.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(s => s.SaleId)
                .OnDelete(DeleteBehavior.Restrict);

            // SaleItem - Medicine
            modelBuilder.Entity<SaleItem>()
                .HasOne(s => s.Medicine)
                .WithMany(m => m.SaleItems)
                .HasForeignKey(s => s.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            // SaleItem - PurchaseItem
            modelBuilder.Entity<SaleItem>()
                .HasOne(s => s.PurchaseItem)
                .WithMany(m => m.SaleItems)
                .HasForeignKey(s => s.PurchaseItemId)
                .OnDelete(DeleteBehavior.Restrict);



            //SaleItem-Customer
            modelBuilder.Entity<SaleItem>()
                .HasOne(s => s.Customer)
                .WithMany(m => m.SaleItems)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // SaleItem - Customer
            modelBuilder.Entity<SaleItem>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.SaleItems)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sale - Customer
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Sale)
                .WithMany(s => s.Transactions) // <== Add a List<Transaction> to Sale model
                .HasForeignKey(t => t.SaleId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Cart>()
            .HasOne(c => c.Medicine)
            .WithMany(m => m.Carts) 
            .HasForeignKey(c => c.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);


            // PurchaseCart - Medicine
            modelBuilder.Entity<PurchaseCart>()
                .HasOne(p => p.Medicine)
                .WithMany(m => m.PurchaseCarts)
                .HasForeignKey(p => p.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseCart - Supplier
            modelBuilder.Entity<PurchaseCart>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.PurchaseCarts)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
