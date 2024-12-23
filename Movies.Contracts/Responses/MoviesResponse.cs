﻿namespace Movies.Contracts.Requests;

public class MoviesResponse
{
    public required IEnumerable<MovieResponse> Items { get; init; } = Enumerable.Empty<MovieResponse>();
}