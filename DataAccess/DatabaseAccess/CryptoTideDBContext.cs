using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;

namespace DataAccess.DatabaseAccess
{
    public partial class CryptoTideDBContext : DbContext
    {
        private readonly string ConnectionString;
        public CryptoTideDBContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("RDS-SQL-SERVER");
        }

        public CryptoTideDBContext(DbContextOptions<CryptoTideDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coins> Coins { get; set; }
        public virtual DbSet<Values> Values { get; set; }
        public virtual DbSet<HourlyTrend> HourlyTrends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coins>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            //modelBuilder.Entity<DailyTrend>(entity =>
            //{
            //    entity.Property(e => e.Date).HasColumnType("date");

            //    entity.Property(e => e.Symbol)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);
            //});

            modelBuilder.Entity<HourlyTrend>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Values>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.LiquidityAsk).HasColumnName("Liquidity.Ask");

                entity.Property(e => e.LiquidityBid).HasColumnName("Liquidity.Bid");

                entity.Property(e => e.LiquidityRatio).HasColumnName("Liquidity.Ratio");

                entity.Property(e => e.Performance1m).HasColumnName("Performance.1m");

                entity.Property(e => e.Performance1w).HasColumnName("Performance.1w");

                entity.Property(e => e.Performance1y).HasColumnName("Performance.1y");

                entity.Property(e => e.Performance24h).HasColumnName("Performance.24h");

                entity.Property(e => e.Performance6m).HasColumnName("Performance.6m");

                entity.Property(e => e.PerformanceYtd).HasColumnName("Performance.ytd");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.Values)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Values_Coins");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
