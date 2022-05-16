﻿// <auto-generated />
using System;
using Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chat.Migrations
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chat.Models.ChatGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChatGroupName")
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Member")
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("ChatGroup");
                });

            modelBuilder.Entity("Chat.Models.FilesInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UoloadDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("FileInfo");
                });

            modelBuilder.Entity("Chat.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FriendId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("userInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userInfoId");

                    b.ToTable("Friend");
                });

            modelBuilder.Entity("Chat.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HeadImg")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserPwd")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Chat.Models.FilesInfo", b =>
                {
                    b.HasOne("Chat.Models.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Chat.Models.Friend", b =>
                {
                    b.HasOne("Chat.Models.UserInfo", "userInfo")
                        .WithMany()
                        .HasForeignKey("userInfoId");

                    b.Navigation("userInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
