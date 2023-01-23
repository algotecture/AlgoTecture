﻿namespace AlgoTecture.Libraries.Space.Interfaces
{
    public interface ISpaceGetter
    {
        Task<Domain.Models.RepositoryModels.Space> GetByCoordinates(double latitude, double longitude);
    }
}