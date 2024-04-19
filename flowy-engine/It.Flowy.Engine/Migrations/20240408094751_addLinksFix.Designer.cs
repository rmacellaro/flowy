﻿// <auto-generated />
using System;
using It.Flowy.Engine.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace It.Flowy.Engine.Migrations
{
    [DbContext(typeof(FlowyEngineContext))]
    [Migration("20240408094751_addLinksFix")]
    partial class addLinksFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Distribution", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("IdProcess")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdProcess");

                    b.ToTable("Distributions", "Modelling");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Link", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("IdSourceNode")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdTargetNode")
                        .HasColumnType("bigint");

                    b.Property<string>("Key")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdSourceNode");

                    b.ToTable("Links", "Modelling");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.LinkConfig", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("IdLink")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdLink");

                    b.ToTable("LinkConfigs", "Modelling");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Node", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<long?>("IdDistribution")
                        .HasColumnType("bigint");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdDistribution");

                    b.ToTable("Nodes", "Modelling");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.NodeConfig", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("IdNode")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdNode");

                    b.ToTable("NodeConfigs", "Modelling");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Process", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Processes", "Modelling");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Data", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("IdInstance")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdInstance");

                    b.ToTable("Datas", "Processing");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Instance", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("IdDistribution")
                        .HasColumnType("bigint");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdDistribution");

                    b.ToTable("Instances", "Processing");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Track", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Data")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("IdWire")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdWire");

                    b.ToTable("Tracks", "Processing");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Wire", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("IdInstance")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdNode")
                        .HasColumnType("bigint");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdInstance");

                    b.HasIndex("IdNode");

                    b.ToTable("Wires", "Processing");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Distribution", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Modelling.Process", "Process")
                        .WithMany("Distributions")
                        .HasForeignKey("IdProcess");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Link", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Modelling.Node", "SourceNode")
                        .WithMany("Links")
                        .HasForeignKey("IdSourceNode");

                    b.Navigation("SourceNode");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.LinkConfig", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Modelling.Link", "Link")
                        .WithMany("Configs")
                        .HasForeignKey("IdLink");

                    b.Navigation("Link");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Node", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Modelling.Distribution", "Distribution")
                        .WithMany("Nodes")
                        .HasForeignKey("IdDistribution");

                    b.Navigation("Distribution");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.NodeConfig", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Modelling.Node", "Node")
                        .WithMany("Configs")
                        .HasForeignKey("IdNode");

                    b.Navigation("Node");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Data", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Processing.Instance", "Instance")
                        .WithMany("Datas")
                        .HasForeignKey("IdInstance");

                    b.Navigation("Instance");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Instance", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Modelling.Distribution", "Distribution")
                        .WithMany()
                        .HasForeignKey("IdDistribution");

                    b.Navigation("Distribution");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Track", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Processing.Wire", "Wire")
                        .WithMany()
                        .HasForeignKey("IdWire");

                    b.Navigation("Wire");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Wire", b =>
                {
                    b.HasOne("It.Flowy.Engine.Models.Processing.Instance", "Instance")
                        .WithMany("Wires")
                        .HasForeignKey("IdInstance");

                    b.HasOne("It.Flowy.Engine.Models.Modelling.Node", "Node")
                        .WithMany()
                        .HasForeignKey("IdNode");

                    b.Navigation("Instance");

                    b.Navigation("Node");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Distribution", b =>
                {
                    b.Navigation("Nodes");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Link", b =>
                {
                    b.Navigation("Configs");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Node", b =>
                {
                    b.Navigation("Configs");

                    b.Navigation("Links");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Modelling.Process", b =>
                {
                    b.Navigation("Distributions");
                });

            modelBuilder.Entity("It.Flowy.Engine.Models.Processing.Instance", b =>
                {
                    b.Navigation("Datas");

                    b.Navigation("Wires");
                });
#pragma warning restore 612, 618
        }
    }
}
