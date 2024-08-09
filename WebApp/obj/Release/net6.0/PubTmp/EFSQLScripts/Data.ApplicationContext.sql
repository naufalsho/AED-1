IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE TABLE [AccMenuGroup] (
        [Id] int NOT NULL IDENTITY,
        [Order] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [Icon] nvarchar(50) NOT NULL,
        [IsDirectMenu] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(100) NULL,
        CONSTRAINT [PK_AccMenuGroup] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE TABLE [AccRole] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(100) NULL,
        [IsDelete] bit NOT NULL,
        [DeletedDate] datetime2 NULL,
        [DeletedBy] nvarchar(100) NULL,
        CONSTRAINT [PK_AccRole] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE TABLE [MstGeneral] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(10) NOT NULL,
        [Type] nvarchar(25) NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [ParentId] int NULL,
        [IsActive] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(100) NULL,
        [IsDelete] bit NOT NULL,
        [DeletedDate] datetime2 NULL,
        [DeletedBy] nvarchar(100) NULL,
        CONSTRAINT [PK_MstGeneral] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MstGeneral_MstGeneral_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [MstGeneral] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE TABLE [AccMenu] (
        [Id] int NOT NULL IDENTITY,
        [MenuGroupId] int NOT NULL,
        [Order] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [Controller] nvarchar(100) NULL,
        [IsActive] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(100) NULL,
        CONSTRAINT [PK_AccMenu] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AccMenu_AccMenuGroup_MenuGroupId] FOREIGN KEY ([MenuGroupId]) REFERENCES [AccMenuGroup] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE TABLE [AccUser] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [UserId] nvarchar(50) NOT NULL,
        [Password] nvarchar(1000) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(100) NULL,
        [IsDelete] bit NOT NULL,
        [DeletedDate] datetime2 NULL,
        [DeletedBy] nvarchar(100) NULL,
        CONSTRAINT [PK_AccUser] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AccUser_AccRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AccRole] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE TABLE [MstEmployee] (
        [Id] int NOT NULL IDENTITY,
        [NRP] nvarchar(25) NOT NULL,
        [Name] nvarchar(25) NOT NULL,
        [CompanyId] int NULL,
        [BranchId] int NULL,
        [DivisionId] int NULL,
        [DepartmentId] int NULL,
        [JobGroupId] int NULL,
        [JobTitleId] int NULL,
        [Status] nvarchar(25) NOT NULL,
        [OutDate] datetime2 NULL,
        [IsActive] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(100) NULL,
        CONSTRAINT [PK_MstEmployee] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MstEmployee_MstGeneral_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [MstGeneral] ([Id]),
        CONSTRAINT [FK_MstEmployee_MstGeneral_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [MstGeneral] ([Id]),
        CONSTRAINT [FK_MstEmployee_MstGeneral_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [MstGeneral] ([Id]),
        CONSTRAINT [FK_MstEmployee_MstGeneral_DivisionId] FOREIGN KEY ([DivisionId]) REFERENCES [MstGeneral] ([Id]),
        CONSTRAINT [FK_MstEmployee_MstGeneral_JobGroupId] FOREIGN KEY ([JobGroupId]) REFERENCES [MstGeneral] ([Id]),
        CONSTRAINT [FK_MstEmployee_MstGeneral_JobTitleId] FOREIGN KEY ([JobTitleId]) REFERENCES [MstGeneral] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE TABLE [AccMRoleMenu] (
        [Id] uniqueidentifier NOT NULL,
        [RoleId] int NOT NULL,
        [MenuId] int NOT NULL,
        [AllowView] bit NOT NULL,
        [AllowCreate] bit NOT NULL,
        [AllowEdit] bit NOT NULL,
        [AllowDelete] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(100) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(100) NULL,
        CONSTRAINT [PK_AccMRoleMenu] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AccMRoleMenu_AccMenu_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [AccMenu] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AccMRoleMenu_AccRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AccRole] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_AccMenu_MenuGroupId] ON [AccMenu] ([MenuGroupId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_AccMRoleMenu_MenuId] ON [AccMRoleMenu] ([MenuId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_AccMRoleMenu_RoleId] ON [AccMRoleMenu] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_AccUser_RoleId] ON [AccUser] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_MstEmployee_BranchId] ON [MstEmployee] ([BranchId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_MstEmployee_CompanyId] ON [MstEmployee] ([CompanyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_MstEmployee_DepartmentId] ON [MstEmployee] ([DepartmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_MstEmployee_DivisionId] ON [MstEmployee] ([DivisionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_MstEmployee_JobGroupId] ON [MstEmployee] ([JobGroupId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_MstEmployee_JobTitleId] ON [MstEmployee] ([JobTitleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    CREATE INDEX [IX_MstGeneral_ParentId] ON [MstGeneral] ([ParentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430134609_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230430134609_InitialMigration', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430143909_InitialViewMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230430143909_InitialViewMigration', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230604060050_UpdateMstGeneralAddAbbr')
BEGIN
    ALTER TABLE [MstGeneral] ADD [Abbreviation] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230604060050_UpdateMstGeneralAddAbbr')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230604060050_UpdateMstGeneralAddAbbr', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230604134017_UpdateMstGeneralAddOrder')
BEGIN
    ALTER TABLE [MstGeneral] ADD [Order] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230604134017_UpdateMstGeneralAddOrder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230604134017_UpdateMstGeneralAddOrder', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230712124236_FixDecimalPrecision')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230712124236_FixDecimalPrecision', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231013135605_UpdateMstGeneralAddColor')
BEGIN
    ALTER TABLE [MstGeneral] ADD [ChartColor] nvarchar(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231013135605_UpdateMstGeneralAddColor')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231013135605_UpdateMstGeneralAddColor', N'6.0.13');
END;
GO

COMMIT;
GO

