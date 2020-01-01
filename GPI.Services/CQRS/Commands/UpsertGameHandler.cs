﻿using GPI.Core.Models.Entities;
using GPI.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPI.Services.CQRS.Commands
{
    public class UpsertGameRequest : IRequest<Game>
    {
        public UpsertGameRequest(Game game)
        {
            Game = game;
        }

        public Game Game { get; }
    }
    public class UpsertGameHandler : IRequestHandler<UpsertGameRequest, Game>
    {
        private readonly IRepository<Game> _gameRepository;

        public UpsertGameHandler(IRepository<Game> gameRepository)
        {
            this._gameRepository = gameRepository;
        }

        public async Task<Game> Handle(UpsertGameRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            if (request.Game.Id == Guid.Empty)
            {
                await _gameRepository.AddAsync(request.Game);
            }
            else
            {
                var game = _gameRepository.GetByIdAsync(request.Game.Id);

                //game.
            }

            return request.Game;
        }
    }
}
