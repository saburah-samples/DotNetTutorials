
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/19/2015 02:29:57
-- Generated from EDMX file: D:\Projects\DotNetTutorials\Duv.Blogging\Duv.Blogging.Domain\Blogging.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BloggingDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PersonEntries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_PersonEntries];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonBlogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Blogs] DROP CONSTRAINT [FK_PersonBlogs];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries_Post] DROP CONSTRAINT [FK_BlogPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_PostComments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries_Comment] DROP CONSTRAINT [FK_PostComments];
GO
IF OBJECT_ID(N'[dbo].[FK_Post_inherits_Entry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries_Post] DROP CONSTRAINT [FK_Post_inherits_Entry];
GO
IF OBJECT_ID(N'[dbo].[FK_Comment_inherits_Entry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries_Comment] DROP CONSTRAINT [FK_Comment_inherits_Entry];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Entries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entries];
GO
IF OBJECT_ID(N'[dbo].[Blogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Blogs];
GO
IF OBJECT_ID(N'[dbo].[Entries_Post]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entries_Post];
GO
IF OBJECT_ID(N'[dbo].[Entries_Comment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entries_Comment];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Entries'
CREATE TABLE [dbo].[Entries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [Modified] datetime  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [Author_Id] int  NOT NULL
);
GO

-- Creating table 'Blogs'
CREATE TABLE [dbo].[Blogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Owner_Id] int  NOT NULL
);
GO

-- Creating table 'Entries_Post'
CREATE TABLE [dbo].[Entries_Post] (
    [PermaLink] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [Blog_Id] int  NOT NULL
);
GO

-- Creating table 'Entries_Comment'
CREATE TABLE [dbo].[Entries_Comment] (
    [Id] int  NOT NULL,
    [Post_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [PK_Entries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Blogs'
ALTER TABLE [dbo].[Blogs]
ADD CONSTRAINT [PK_Blogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entries_Post'
ALTER TABLE [dbo].[Entries_Post]
ADD CONSTRAINT [PK_Entries_Post]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entries_Comment'
ALTER TABLE [dbo].[Entries_Comment]
ADD CONSTRAINT [PK_Entries_Comment]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Author_Id] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [FK_PersonEntries]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEntries'
CREATE INDEX [IX_FK_PersonEntries]
ON [dbo].[Entries]
    ([Author_Id]);
GO

-- Creating foreign key on [Owner_Id] in table 'Blogs'
ALTER TABLE [dbo].[Blogs]
ADD CONSTRAINT [FK_PersonBlogs]
    FOREIGN KEY ([Owner_Id])
    REFERENCES [dbo].[Persons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonBlogs'
CREATE INDEX [IX_FK_PersonBlogs]
ON [dbo].[Blogs]
    ([Owner_Id]);
GO

-- Creating foreign key on [Blog_Id] in table 'Entries_Post'
ALTER TABLE [dbo].[Entries_Post]
ADD CONSTRAINT [FK_BlogPosts]
    FOREIGN KEY ([Blog_Id])
    REFERENCES [dbo].[Blogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogPosts'
CREATE INDEX [IX_FK_BlogPosts]
ON [dbo].[Entries_Post]
    ([Blog_Id]);
GO

-- Creating foreign key on [Post_Id] in table 'Entries_Comment'
ALTER TABLE [dbo].[Entries_Comment]
ADD CONSTRAINT [FK_PostComments]
    FOREIGN KEY ([Post_Id])
    REFERENCES [dbo].[Entries_Post]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComments'
CREATE INDEX [IX_FK_PostComments]
ON [dbo].[Entries_Comment]
    ([Post_Id]);
GO

-- Creating foreign key on [Id] in table 'Entries_Post'
ALTER TABLE [dbo].[Entries_Post]
ADD CONSTRAINT [FK_Post_inherits_Entry]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Entries]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Entries_Comment'
ALTER TABLE [dbo].[Entries_Comment]
ADD CONSTRAINT [FK_Comment_inherits_Entry]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Entries]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------