﻿using Dapper;

namespace Movies.Application.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        
        // main table
        await connection.ExecuteAsync("""
            create table if not exists movies (
                id UUID primary key,
                slug TEXT not null,
                title TEXT not null,
                yearofrelease integer not null
            );
            """);
        
        // slug index
        await connection.ExecuteAsync("""
              create unique index concurrently if not exists movies_slug_idx
              on movies
              using btree(slug);
              """);
        
        // genre table
        await connection.ExecuteAsync("""
              create table if not exists genres (
                  movieId UUID references movies(id),
                  name TEXT not null
              )
              """);
    }
}