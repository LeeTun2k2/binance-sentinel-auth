﻿namespace BinanceSential.Auth.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
