using AutoMapper;
using Domain.Repo;
using EvolutionBack.Models;
using MediatR;
using System.Security.Authentication;

namespace EvolutionBack.Commands;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserViewModel>
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;

    public UserLoginCommandHandler(IUserRepo userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public Task<UserViewModel> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepo.Login(request.Login, request.Password);

        if (user is null)
        {
            throw new AuthenticationException("Login or password invalid!");
        }

        return Task.FromResult(_mapper.Map<UserViewModel>(user));
    }
}
