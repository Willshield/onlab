using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectTimeAssistant.Services.DataBase;

namespace ProjectTimeAssistant.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170419013740_MyThirdMigration")]
    partial class MyThirdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("ProjectTimeAssistant.Models.Issue", b =>
                {
                    b.Property<int>("IssueID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("Dirty");

                    b.Property<string>("Priority");

                    b.Property<int>("ProjectID");

                    b.Property<string>("Subject");

                    b.Property<string>("Tracker");

                    b.Property<DateTime>("Updated");

                    b.HasKey("IssueID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("ProjectTimeAssistant.Models.Profile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("ID");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ProjectTimeAssistant.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ProjectID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectTimeAssistant.Models.WorkTime", b =>
                {
                    b.Property<int>("WorkTimeID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FinishTime");

                    b.Property<int>("IssueID");

                    b.Property<DateTime?>("StartTime");

                    b.HasKey("WorkTimeID");

                    b.HasIndex("IssueID");

                    b.ToTable("WorkTimes");
                });

            modelBuilder.Entity("ProjectTimeAssistant.Models.Issue", b =>
                {
                    b.HasOne("ProjectTimeAssistant.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectTimeAssistant.Models.WorkTime", b =>
                {
                    b.HasOne("ProjectTimeAssistant.Models.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
