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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018062216_initialCreate')
BEGIN
    CREATE TABLE [MedCategory] (
        [CategoryId] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MedCategory] PRIMARY KEY ([CategoryId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018062216_initialCreate')
BEGIN
    CREATE TABLE [MedItems] (
        [ItemId] int NOT NULL IDENTITY,
        [CategoryId] int NOT NULL,
        [ItemName] nvarchar(max) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [Seller] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MedItems] PRIMARY KEY ([ItemId]),
        CONSTRAINT [FK_MedItems_MedCategory_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [MedCategory] ([CategoryId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018062216_initialCreate')
BEGIN
    CREATE TABLE [MedCart] (
        [CartId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ItemId] int NOT NULL,
        [Quantity] int NOT NULL,
        CONSTRAINT [PK_MedCart] PRIMARY KEY ([CartId]),
        CONSTRAINT [FK_MedCart_MedItems_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [MedItems] ([ItemId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018062216_initialCreate')
BEGIN
    CREATE INDEX [IX_MedCart_ItemId] ON [MedCart] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018062216_initialCreate')
BEGIN
    CREATE INDEX [IX_MedItems_CategoryId] ON [MedItems] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018062216_initialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231018062216_initialCreate', N'6.0.23');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018064737_initialCreate2')
BEGIN
    CREATE TABLE [UserControl] (
        [UserId] int NOT NULL IDENTITY,
        [UserName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Access] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_UserControl] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018064737_initialCreate2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231018064737_initialCreate2', N'6.0.23');
END;
GO

COMMIT;
GO

