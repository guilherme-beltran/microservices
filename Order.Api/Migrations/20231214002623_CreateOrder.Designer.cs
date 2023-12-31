﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orders.Api.Core.Data;

#nullable disable

namespace Orders.Api.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20231214002623_CreateOrder")]
    partial class CreateOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Orders.Api.Orders.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderId"));

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint")
                        .HasColumnName("CustomerId");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint")
                        .HasColumnName("ProductId");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Quantity");

                    b.HasKey("OrderId");

                    b.ToTable("Order", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
